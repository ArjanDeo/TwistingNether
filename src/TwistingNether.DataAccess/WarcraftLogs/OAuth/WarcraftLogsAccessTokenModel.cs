namespace TwistingNether.DataAccess.WarcraftLogs.OAuth
{
    public class WarcraftLogsAccessTokenModel
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public DateTime acquired_at { get; set; } = DateTime.UtcNow;
    }
}
