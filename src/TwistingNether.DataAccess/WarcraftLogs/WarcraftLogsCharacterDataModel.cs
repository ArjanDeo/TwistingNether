namespace TwistingNether.DataAccess.WarcraftLogs
{
    public class AllStar
    {
        public int partition { get; set; }
        public string spec { get; set; }
        public double points { get; set; }
        public int possiblePoints { get; set; }
        public int rank { get; set; }
        public int regionRank { get; set; }
        public int serverRank { get; set; }
        public double rankPercent { get; set; }
        public int total { get; set; }
    }

    public class Character
    {
        public int canonicalID { get; set; }
        public int classID { get; set; }
        public EncounterRankings encounterRankings { get; set; }
        public ZoneRankings zoneRankings { get; set; }
        public bool hidden { get; set; }
    }

    public class CharacterData
    {
        public Character character { get; set; }
    }

    public class Data
    {
        public CharacterData characterData { get; set; }
    }

    public class Encounter
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class EncounterRankings
    {
        public double bestAmount { get; set; }
        public double medianPerformance { get; set; }
        public double averagePerformance { get; set; }
        public int totalKills { get; set; }
        public int fastestKill { get; set; }
        public int difficulty { get; set; }
        public string metric { get; set; }
        public int partition { get; set; }
        public int zone { get; set; }
        public List<Rank> ranks { get; set; }
    }

    public class Guild
    {
        public object id { get; set; }
        public object name { get; set; }
        public object faction { get; set; }
    }

    public class Rank
    {
        public bool lockedIn { get; set; }
        public double rankPercent { get; set; }
        public double historicalPercent { get; set; }
        public double todayPercent { get; set; }
        public int rankTotalParses { get; set; }
        public int historicalTotalParses { get; set; }
        public int todayTotalParses { get; set; }
        public Guild guild { get; set; }
        public Report report { get; set; }
        public int duration { get; set; }
        public long startTime { get; set; }
        public double amount { get; set; }
        public int bracketData { get; set; }
        public string spec { get; set; }
        public string bestSpec { get; set; }
        public int @class { get; set; }
        public int faction { get; set; }
    }

    public class Ranking
    {
        public Encounter encounter { get; set; }
        public double? rankPercent { get; set; }
        public double? medianPercent { get; set; }
        public bool? lockedIn { get; set; }
        public int? totalKills { get; set; }
        public int? fastestKill { get; set; }
        public AllStar? allStars { get; set; }
        public string? spec { get; set; }
        public string? bestSpec { get; set; }
        public double? bestAmount { get; set; }
    }

    public class Report
    {
        public string code { get; set; }
        public long startTime { get; set; }
        public int fightID { get; set; }
    }

    public class WarcraftLogsCharacterDataModel
    {
        public Data data { get; set; }
    }

    public class ZoneRankings
    {
        public double? bestPerformanceAverage { get; set; }
        public double? medianPerformanceAverage { get; set; }
        public int? difficulty { get; set; }
        public string? metric { get; set; }
        public int? partition { get; set; }
        public int? zone { get; set; }
        public int? size { get; set; }
        public List<AllStar>? allStars { get; set; }
        public List<Ranking>? rankings { get; set; }
    }


}
