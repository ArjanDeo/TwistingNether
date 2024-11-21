using TwistingNether.DataAccess.RaiderIO;

namespace TwistingNether.DataAccess.TwistingNether.Character
{
    public class CharacterModel
    {
        public RaiderIOCharacterDataModel? RaiderIOCharacterData { get; set; }
        public List<int>? DungeonVaultSlots { get; set; }
       // public List<RaidEncounter>? RaidBossesKilledThisWeek { get; set; }
        public string? classColor { get; set; }
        public List<CharacterMediaModel>? CharacterMedia { get; set; }
    }
}
