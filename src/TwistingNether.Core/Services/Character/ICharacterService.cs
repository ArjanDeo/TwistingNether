using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Raid;

namespace TwistingNether.Core.Services.Character
{
    public interface ICharacterService
    {
        Task<BaseCharacterModel> GetBaseCharacterAsync(CharacterRequestModel character);
        Task<WoWCharacterStatisticsModel> GetCharacterStatisticsAsync(CharacterRequestModel character);

        Task<List<CharacterMediaModel>> GetCharacterMediaAsync(CharacterRequestModel character);
        Task<List<RaidEncounter>> GetCharacterWeeklyBossesKilledAsync(CharacterRequestModel character);
    }
}
