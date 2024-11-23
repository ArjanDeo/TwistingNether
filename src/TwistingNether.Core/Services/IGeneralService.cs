using TwistingNether.DataAccess.BattleNet.WoW.News;

namespace TwistingNether.Core.Services
{
    public interface IGeneralService
    {
        Task<List<WowNewsModel>?> GetNews(int? limit);
    }
}
