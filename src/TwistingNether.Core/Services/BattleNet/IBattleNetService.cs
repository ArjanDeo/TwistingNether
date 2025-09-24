using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services.BattleNet
{
    public interface IBattleNetService
    {
        Task<List<CharacterMediaModel>> GetCharacterMediaAsync(CharacterRequestModel character);
        Task<WoWCharacterEquipmentModel> GetCharacterEquipmentAsync(CharacterRequestModel character);
        Task<string> GetItemMediaAsync(string id);
    }
}
