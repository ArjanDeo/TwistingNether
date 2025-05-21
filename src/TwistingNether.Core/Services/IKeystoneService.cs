using TwistingNether.DataAccess.BattleNet.WoW.Keystone.Affixes.Media;

namespace TwistingNether.Core.Services
{
    public interface IKeystoneService
    {
        Task<WoWAffixMediaDto> GetAffixMedia(int id);
        Task<object> MythicScoreCalculation(int targetScore, string region, string realm, string name);
    }
}
