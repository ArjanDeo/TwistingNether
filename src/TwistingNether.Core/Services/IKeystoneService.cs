using TwistingNether.DataAccess.BattleNet.WoW.Keystone.Affixes.Media;

namespace TwistingNether.Core.Services
{
    public interface IKeystoneService
    {
        Task<WoWAffixMediaDto> GetAffixMedia(int id);
    }
}
