using Microsoft.EntityFrameworkCore;
using Pathoschild.Http.Client;
using TwistingNether.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Exceptions;
using TwistingNether.DataAccess.TwistingNether.Raid;

namespace TwistingNether.Core.Services
{
    public class CharacterService(Common common, FluentClient client) : ICharacterService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        public async Task<CharacterModel> GetCharacter(CharacterRequestModel character)
        {
            character.Name = character.Name.ToLower();
            character.Realm = character.Realm.ToLower();
            character.Region = character.Region.ToLower();
            character.Realm = character.Realm.Replace(" ", "-").Replace("\'", "");
            RaiderIOCharacterDataModel raiderIOCharacterData = await _client
                             .GetAsync("https://raider.io/api/v1/characters/profile")
                             .WithArgument("region", character.Region)
                             .WithArgument("name", character.Name)
                             .WithArgument("realm", character.Realm)
                             .WithArgument("fields", "raid_progression,mythic_plus_weekly_highest_level_runs,mythic_plus_scores_by_season:current,guild,gear,mythic_plus_highest_level_runs,mythic_plus_best_runs,raid_achievement_curve:nerubar-palace,raid_achievement_curve:liberation-of-undermine")
                             .As<RaiderIOCharacterDataModel>();

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
            return new CharacterModel
            {
                RaiderIOCharacterData = raiderIOCharacterData,
                classColor = await _common.GetClassColor(raiderIOCharacterData.char_class),
                CharacterMedia = characterMediaList,
                RaidBossesKilledThisWeek = await GetCharacterRaids(character.Name, character.Realm, character.Region)
            };
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

        public async Task<object?> PingCharacter(CharacterRequestModel character)
        {
            try
            {
                RaiderIOCharacterDataModel data = await _client
                      .GetAsync("https://raider.io/api/v1/characters/profile")
                      .WithArgument("region", character.Region)
                      .WithArgument("name", character.Name)
                      .WithArgument("realm", character.Realm.Replace(" ", "-"))
                      .As<RaiderIOCharacterDataModel>();


                return new
                {
                    Name = data.name,
                    Realm = data.realm,
                    Region = data.region,
                    Class = data.char_class.Replace(" ", "")
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
