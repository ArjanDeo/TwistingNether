using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services
{
    public interface ICharacterService
    {
        Task<CharacterModel> GetCharacter(CharacterRequestModel character);
        Task<object?> PingCharacter (CharacterRequestModel character);
        Task<List<Quest>> GetCharacterCompletedQuests(CharacterRequestModel character);
    }
}
