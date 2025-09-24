using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.WarcraftLogs;

namespace TwistingNether.Core.Services.WarcraftLogs
{
    public interface IWarcraftLogsService
    {
        Task<ZoneRankings> GetLatestRaidPerformanceAsync(CharacterRequestModel character, int? difficulty);
    }
}
