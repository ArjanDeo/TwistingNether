using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.News;
using TwistingNether.DataAccess.BattleNet.WoW.Token;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services.BattleNet
{
    public interface IBattleNetService
    {
        Task<List<CharacterMediaModel>> GetCharacterMediaAsync(CharacterRequestModel character);
        Task<WoWCharacterEquipmentModel> GetCharacterEquipmentAsync(CharacterRequestModel character);
        Task<WoWCharacterRaidsModel> GetCharacterRaidsAsync(CharacterRequestModel character);
        Task<WowTokenModel> GetTokenPriceAsync();
        Task<string> GetItemMediaAsync(string id);
        // Returns top 5 posts
        Task<List<WowNewsModel>> GetNews();
    }
}
