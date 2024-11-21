namespace TwistingNether.DataAccess.BattleNet.OAuth
{
    public class BattleNetAccessTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string sub { get; set; }
        public DateTime acquired_at { get; set; }
    }
}
