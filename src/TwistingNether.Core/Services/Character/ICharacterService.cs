using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.WarcraftLogs;

namespace TwistingNether.Core.Services.Character
{
    public interface ICharacterService
    {
        Task<CharacterModel> GetCharacter(CharacterRequestModel character);
        Task<bool> PingCharacter (CharacterRequestModel character);
        Task<List<Quest>> GetCharacterCompletedQuests(CharacterRequestModel character);
        Task<ZoneRankings> GetLatestRaidPerformance(CharacterRequestModel character, int difficulty);
    }
}
