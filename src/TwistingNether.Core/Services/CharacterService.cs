using LazyCache;
using Microsoft.EntityFrameworkCore;
using Pathoschild.Http.Client;
using TwistingNether.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Exceptions;
using TwistingNether.DataAccess.TwistingNether.Raid;
using TwistingNether.DataAccess.WarcraftLogs;
using TwistingNether.DataAccess.WarcraftLogs.OAuth;

namespace TwistingNether.Core.Services
{
    public class CharacterService(Common common, FluentClient client, IAppCache cache) : ICharacterService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;
        public async Task<CharacterModel> GetCharacter(CharacterRequestModel character)
        {
            return await _cache.GetOrAddAsync($"character-{character.Region}-{character.Realm}-{character.Name}", async () =>
            {
                character.Name = character.Name.ToLower();
                character.Realm = character.Realm.ToLower();
                character.Region = character.Region.ToLower();
                character.Realm = character.Realm.Replace(" ", "-").Replace("\'", "");
                IResponse raiderIOCharacterDataResponse = await _client
                                 .GetAsync("https://raider.io/api/v1/characters/profile")
                                 .WithOptions(ignoreHttpErrors: true)
                                 .WithArgument("region", character.Region)
                                 .WithArgument("name", character.Name)
                                 .WithArgument("realm", character.Realm)
                                 .WithArgument("fields", "raid_progression,mythic_plus_weekly_highest_level_runs,mythic_plus_scores_by_season:current,guild,gear,mythic_plus_highest_level_runs,mythic_plus_best_runs,raid_achievement_curve:manaforge-omega");
                if (!raiderIOCharacterDataResponse.IsSuccessStatusCode)
                {
                    throw new KeyNotFoundException($"Character {character.Name} on realm {character.Realm} in region {character.Region} was not found.");
                }
                var raiderIOCharacterData = await raiderIOCharacterDataResponse.As<RaiderIOCharacterDataModel>();

                bool tokenSuccessful = await _common.GetNewBattleNetAccessToken();

                if (!tokenSuccessful)
                {
                    throw new TokenRetrievalException("Could not get a token from BattleNet.");
                }

                WoWCharacterMediaModel data = await _client
                 .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/character-media?namespace=profile-us&locale=en_US&:region=us")
                 .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                 .As<WoWCharacterMediaModel>();

                List<CharacterMediaModel> characterMediaList = [];
                foreach (var image in data.assets)
                {
                    CharacterMediaModel characterMediaModel = new()
                    {
                        Link = image.value,
                        Type = image.key
                    };

                    characterMediaList.Add(characterMediaModel);
                }

                WoWCharacterEquipmentModel characterGear = await _client
                .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/equipment?namespace=profile-us&locale=en_US&:region=us")
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WoWCharacterEquipmentModel>();

                for (int i = 0; i < characterGear.equipped_items.Count; i++)
                {
                    var item = characterGear.equipped_items[i];
                    if (item.item != null)
                        characterGear.equipped_items[i].item.iconUrl = await GetItemMedia(item.item.id.ToString());
                }

                return new CharacterModel
                {
                    RaiderIOCharacterData = raiderIOCharacterData,
                    classColor = await _common.GetClassColor(raiderIOCharacterData.char_class),
                    CharacterMedia = characterMediaList,
                    RaidBossesKilledThisWeek = await GetCharacterRaids(character.Name, character.Realm, character.Region),
                    CharacterEquipment = characterGear,
                    RaidPerformance = await GetLatestRaidPerformance(character, 0) // 0: Highest, 1: LFR, 2: Normal, 3: Heroic, 4: Mythic

                };
            }, TimeSpan.FromMinutes(60));
            
        }
        private async Task<List<RaidEncounter>> GetCharacterRaids(string name, string realm, string region)
        {
            realm = realm.ToLower();
            name = name.ToLower();
            region = region.ToLower();

            await _common.GetNewBattleNetAccessToken();

            WoWCharacterRaidsModel data = await _client
                .GetAsync($"https://{region}.api.blizzard.com/profile/wow/character/{realm}/{name}/encounters/raids?namespace=profile-us&locale=en_US")
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WoWCharacterRaidsModel>();

            Dictionary<string, (DateTime Timestamp, string Difficulty)> lastKilledTimestamps = [];

            var responseEncounters = data.expansions
                .SelectMany(e => e.instances)
                .SelectMany(i => i.modes);

            List<string> validRaidBosses = ["Plexus Sentinel", "Loom'ithar", "Soulbinder Naazindhri", "Forgeweaver Araz", "The Soul Hunters", "Fractillus", "Nexus-King Salhadaar", "Dimensius"];

            foreach (var encounter in responseEncounters)
            {
                string difficultyLevel = encounter.difficulty.name;
                foreach (var boss in encounter.progress.encounters)
                {
                    string bossName = boss.encounter.name;
                    long lastKillTimestamp = boss.last_kill_timestamp;

                    if (validRaidBosses.Contains(bossName))
                    {
                        DateTime lastKillDateTime = DateTimeOffset.FromUnixTimeMilliseconds(lastKillTimestamp).UtcDateTime;

                        if (lastKilledTimestamps.TryGetValue(bossName, out var existingEntry))
                        {
                            if (existingEntry.Timestamp < lastKillDateTime)
                            {
                                lastKilledTimestamps[bossName] = (lastKillDateTime, difficultyLevel);
                            }
                        }
                        else
                        {
                            lastKilledTimestamps[bossName] = (lastKillDateTime, difficultyLevel);
                        }
                    }
                }
            }

            List<RaidEncounter> clearedBossList = [];

            foreach (var entry in lastKilledTimestamps)
            {
                if (entry.Value.Timestamp > GetLastReset())
                {
                    clearedBossList.Add(new RaidEncounter
                    {
                        Boss = entry.Key,
                        Difficulty = entry.Value.Difficulty
                    });
                }
            }
            return clearedBossList;
        }
        private static DateTime GetLastReset()
        {
            DateTime lastTuesday = DateTime.Now.AddDays(-1);
            while (lastTuesday.DayOfWeek != DayOfWeek.Tuesday)
            {
                lastTuesday = lastTuesday.AddDays(-1);
            }
            return lastTuesday;
        }
        private async Task<string> GetItemMedia(string id)
        {
            await _common.GetNewBattleNetAccessToken();
            return await _cache.GetOrAddAsync($"item-media-{id}", async () =>
            {
                WowItemMediaModel res = await _client.GetAsync($"https://us.api.blizzard.com/data/wow/media/item/{id}")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "static-us" },
                        {"locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WowItemMediaModel>();
                return res.assets.FirstOrDefault(a => a.key == "icon")?.value ?? "";
            }, TimeSpan.MaxValue);
        }
        public async Task<List<Quest>> GetCharacterCompletedQuests(CharacterRequestModel character)
        {
            await _common.GetNewBattleNetAccessToken();
            WoWCharacterCompletedQuestsModel res = await _client.GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/quests/completed")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "profile-us" },
                        {"locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WoWCharacterCompletedQuestsModel>();
            int[] undermineQuestIds = [86775, 83151, 83096, 83176,];

            List<Quest> undermineWeeklyQuests = res.quests.Where(q => undermineQuestIds.Contains(q.id)).ToList();

            return undermineWeeklyQuests;
        }
        public async Task<bool> PingCharacter(CharacterRequestModel character)
        {
            try
            {
                IResponse response = await _client
                      .GetAsync("https://raider.io/api/v1/characters/profile")
                      .WithArgument("region", character.Region)
                      .WithArgument("name", character.Name)
                      .WithArgument("realm", character.Realm.Replace(" ", "-"));

               return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ZoneRankings> GetLatestRaidPerformance(CharacterRequestModel character, int difficulty)
        {
            await _common.GetNewWarcraftLogsAccessToken();
            return await _cache.GetOrAddAsync($"GetLatestRaidPerformance-{character.Region}-{character.Realm}-{character.Name}-{difficulty}", async () =>
            {
                try
                {
                    var query = $$"""                     
                    query CharacterData($name: String!, $serverSlug: String!, $serverRegion: String!) 
                    { characterData 
                        { character(name: $name, serverSlug: $serverSlug, serverRegion: $serverRegion) 
                            { canonicalID classID hidden zoneRankings } 
                        } 
                    }
                    """;
                    Dictionary<string, object> payload = new()
                    {
                        ["query"] = query,
                        ["variables"] = new
                        {
                            name = character.Name,
                            serverSlug = character.Realm,
                            serverRegion = character.Region
                        }
                    };
                    IResponse response = await _client
                    .PostAsync("https://www.warcraftlogs.com/api/v2/client")
                    .WithBearerAuthentication(AppConstants.WarcraftLogsAccessToken.access_token)
                    .WithBody(payload);
                    var stringResponse = await response.AsString();
                    var formattedResponse = await response.As<WarcraftLogsCharacterDataModel>();

                    return formattedResponse.data.characterData.character.zoneRankings;
                }
                catch (ApiException ex)
                {
                    throw new Exception($"There was an issue trying to get the character's raid performance. {await ex.Response.AsString()}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }, TimeSpan.FromMinutes(60));
        }

    }
}
