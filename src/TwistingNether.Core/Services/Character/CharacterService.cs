using LazyCache;
using Microsoft.EntityFrameworkCore;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services.BattleNet;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Raid;
using TwistingNether.DataAccess.WarcraftLogs;

namespace TwistingNether.Core.Services.Character
{
    public class CharacterService(Common common, FluentClient client, IAppCache cache, IBattleNetService battleNetService) : ICharacterService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;
        private readonly IBattleNetService _battleNetService = battleNetService;
        public async Task<CharacterModel> GetCharacter(CharacterRequestModel character)
        {
            return await _cache.GetOrAddAsync($"character-{character.Region}-{character.Realm}-{character.Name}", async () =>
            {
                character.Name = character.Name.ToLower();
                character.Realm = character.Realm.ToLower();
                character.Region = character.Region.ToLower();
                character.Realm = character.Realm.Replace(" ", "-").Replace("\'", "");

                var raiderIOCharacterDataResponse = _client
                                 .GetAsync("https://raider.io/api/v1/characters/profile")
                                 .WithOptions(ignoreHttpErrors: true)
                                 .WithArgument("region", character.Region)
                                 .WithArgument("name", character.Name)
                                 .WithArgument("realm", character.Realm)
                                 .WithArgument("fields", "raid_progression,mythic_plus_weekly_highest_level_runs,mythic_plus_scores_by_season:current,guild,gear,mythic_plus_highest_level_runs,mythic_plus_best_runs,raid_achievement_curve:manaforge-omega");
                var raiderIOCharacterDataTask = raiderIOCharacterDataResponse.As<RaiderIOCharacterDataModel>();
                var characterMediaListTask = _battleNetService.GetCharacterMediaAsync(character);
                var characterEquipmentTask = _battleNetService.GetCharacterEquipmentAsync(character);
                var raidBossesKilledTask = GetCharacterWeeklyBossesKilled(character);
                var raidPerformanceTask = GetLatestRaidPerformance(character, 0);

                await Task.WhenAll(
                    raiderIOCharacterDataTask,
                    characterMediaListTask,
                    characterEquipmentTask,
                    raidBossesKilledTask,
                    raidPerformanceTask
                );

                var raiderIOCharacterData = raiderIOCharacterDataTask.Result;
                var characterMediaList = characterMediaListTask.Result;
                var characterEquipment = characterEquipmentTask.Result;
                var raidBossesKilled = raidBossesKilledTask.Result;
                var raidPerformance = raidPerformanceTask.Result;

                var classColor = await _common.GetClassColor(raiderIOCharacterData.char_class);


                return new CharacterModel
                {
                    RaiderIOCharacterData = raiderIOCharacterData,
                    classColor = classColor,
                    CharacterMedia = characterMediaList,
                    RaidBossesKilledThisWeek = raidBossesKilled,
                    CharacterEquipment = characterEquipment,
                    RaidPerformance = raidPerformance

                };
            }, TimeSpan.FromMinutes(60));
            
        }
        // Returns a list of current tier raid bosses killed this week by the specified character.
        private async Task<List<RaidEncounter>> GetCharacterWeeklyBossesKilled(CharacterRequestModel character)
        {
            character.Realm = character.Realm.ToLower();
            character.Name = character.Name.ToLower();
            character.Region = character.Region.ToLower();

            await _common.GetNewBattleNetAccessToken();

            WoWCharacterRaidsModel data = await _client
                .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/encounters/raids?namespace=profile-{character.Region}&locale=en_US")
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WoWCharacterRaidsModel>();


            var responseEncounters = data.expansions
                .SelectMany(e => e.instances)
                .SelectMany(i => i.modes);

            List<string> validRaidBosses = ["Plexus Sentinel", "Loom'ithar", "Soulbinder Naazindhri", "Forgeweaver Araz", "The Soul Hunters", "Fractillus", "Nexus-King Salhadaar", "Dimensius"];

            var lastKilledTimestamps = responseEncounters
            .SelectMany(encounter => encounter.progress.encounters.Select(boss => new
            {
                BossName = boss.encounter.name,
                Difficulty = encounter.difficulty.name,
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(boss.last_kill_timestamp).UtcDateTime
            }))
            .Where(x => validRaidBosses.Contains(x.BossName))
            .GroupBy(x => x.BossName)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(x => x.Timestamp).First()
            );

            return [.. lastKilledTimestamps
             .Where(entry => entry.Value.Timestamp > GetLastReset())
             .Select(entry => new RaidEncounter
             {
                 Boss = entry.Key,
                 Difficulty = entry.Value.Difficulty
             })];

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
