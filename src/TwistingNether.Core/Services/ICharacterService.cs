using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services
{
    public interface ICharacterService
    {
        Task<CharacterModel> GetCharacter(string name, string realm, string region);
        Task<object?> PingCharacter (string name, string realm, string region);
        Task<List<Quest>> GetCharacterCompletedQuests(string name, string realm, string region);
    }
}
