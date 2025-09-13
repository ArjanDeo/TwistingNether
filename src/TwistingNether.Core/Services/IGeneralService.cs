using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.BattleNet.WoW.News;
using TwistingNether.DataAccess.BattleNet.WoW.Token;

namespace TwistingNether.Core.Services
{
    public interface IGeneralService
    {
        Task<List<WowNewsModel>?> GetNews(int? limit);
        Task<WowTokenModel> GetTokenPrice();
        Task<WowItemMediaModel> GetItemMedia(string itemId);

    }
}
