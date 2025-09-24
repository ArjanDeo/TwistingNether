using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services.Character
{
    public interface ICharacterService
    {
        Task<CharacterModel> GetCharacter(CharacterRequestModel character);
    }
}
