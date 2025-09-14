using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Raid;
using TwistingNether.DataAccess.WarcraftLogs;

namespace TwistingNether.DataAccess.TwistingNether.Character
{
    public class CharacterModel
    {
        public RaiderIOCharacterDataModel? RaiderIOCharacterData { get; set; }
        public List<RaidEncounter>? RaidBossesKilledThisWeek { get; set; }
        public List<int>? DungeonVaultSlots { get; set; }
        public List<CharacterMediaModel>? CharacterMedia { get; set; }
        public WoWCharacterEquipmentModel CharacterEquipment { get; set; }
        public ZoneRankings RaidPerformance { get; set; }
        public string? classColor { get; set; }
    }
}
