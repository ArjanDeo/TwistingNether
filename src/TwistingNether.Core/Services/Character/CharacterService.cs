using LazyCache;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services.BattleNet;
using TwistingNether.Core.Services.WarcraftLogs;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Raid;

namespace TwistingNether.Core.Services.Character
{
    public class CharacterService(Common common, FluentClient client, IAppCache cache, IBattleNetService battleNetService, IWarcraftLogsService warcraftLogsService) : ICharacterService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;
        private readonly IBattleNetService _battleNetService = battleNetService;
        private readonly IWarcraftLogsService _warcraftLogsService = warcraftLogsService;
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
                                 .WithArgument("region", character.Region)
                                 .WithArgument("name", character.Name)
                                 .WithArgument("realm", character.Realm)
                                 .WithArgument("fields", "raid_progression,mythic_plus_weekly_highest_level_runs,mythic_plus_scores_by_season:current,guild,gear,mythic_plus_highest_level_runs,mythic_plus_best_runs,raid_achievement_curve:manaforge-omega");
                var raiderIOCharacterDataTask = raiderIOCharacterDataResponse.As<RaiderIOCharacterDataModel>();
                var characterMediaListTask = _battleNetService.GetCharacterMediaAsync(character);
                var characterEquipmentTask = _battleNetService.GetCharacterEquipmentAsync(character);
                var raidBossesKilledTask = GetCharacterWeeklyBossesKilledAsync(character);
                var raidPerformanceTask = _warcraftLogsService.GetLatestRaidPerformanceAsync(character, 0);

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
        public async Task<List<RaidEncounter>> GetCharacterWeeklyBossesKilledAsync(CharacterRequestModel character)
        {
            character.Realm = character.Realm.ToLower();
            character.Name = character.Name.ToLower();
            character.Region = character.Region.ToLower();

            List<string> validRaidBosses =
            [
            "Plexus Sentinel",
            "Loom'ithar",
            "Soulbinder Naazindhri",
            "Forgeweaver Araz",
            "The Soul Hunters",
            "Fractillus",
            "Nexus-King Salhadaar",
            "Dimensius"
            ];

            var raidData = await _battleNetService.GetCharacterRaidsAsync(character);

            var responseEncounters = raidData.expansions
                .SelectMany(e => e.instances)
                .SelectMany(i => i.modes);


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
             .Where(entry => entry.Value.Timestamp > _common.GetLastReset())
             .Select(entry => new RaidEncounter
             {
                 Boss = entry.Key,
                 Difficulty = entry.Value.Difficulty
             })];

        }
        public async Task<BaseCharacterModel> GetBaseCharacterAsync(CharacterRequestModel character)
        {
            return await _cache.GetOrAddAsync($"raiderio-{character.Region}-{character.Realm}-{character.Name}", async () =>
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
                var characterEquipmentTask = _battleNetService.GetCharacterEquipmentAsync(character);



                await Task.WhenAll(
                    raiderIOCharacterDataTask,
                    characterEquipmentTask
                );

                var raiderIOCharacterData = raiderIOCharacterDataTask.Result;
                var characterEquipment = characterEquipmentTask.Result;

                var classColor = await _common.GetClassColor(raiderIOCharacterData.char_class);

                return new BaseCharacterModel
                {
                    CharacterData = raiderIOCharacterData,
                    CharacterEquipment = characterEquipment,
                    ClassColor = classColor
                };
            }, TimeSpan.FromMinutes(60));
            
        }
        public async Task<List<CharacterMediaModel>> GetCharacterMediaAsync(CharacterRequestModel character)
        {
            return await _cache.GetOrAddAsync($"characterMedia-{character.Region}-{character.Realm}-{character.Name}", async () =>
            {
                var characterMediaList = await _battleNetService.GetCharacterMediaAsync(character);
                return characterMediaList;
            }, TimeSpan.FromHours(6));
            
        }

    }
}
