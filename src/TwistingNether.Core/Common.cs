using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pathoschild.Http.Client;
using TwistingNether.DataAccess;
using TwistingNether.DataAccess.BattleNet.OAuth;
using TwistingNether.DataAccess.Configuration;

namespace TwistingNether.Core
{
    public class Common(FluentClient client, ApplicationDbContext context)
    {
        private readonly FluentClient _client = client;
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> GetNewBattleNetAccessToken()
        {
            // If there is an access token already, and it has not expired yet.
            if (AppConstants.BattleNetAccessToken != null &&
                AppConstants.BattleNetAccessToken.acquired_at.AddSeconds(AppConstants.BattleNetAccessToken.expires_in).AddMinutes(-5) > DateTime.UtcNow)
            {
                return true;
            }

            Dictionary<string, string> AccessTokenPayload = new()
            {
                [":region"] = "us",
                ["grant_type"] = "client_credentials"
            };

            try
            {
                var Response = await _client
                    .PostAsync("https://us.battle.net/oauth/token")
                    .WithBody(p => p.FormUrlEncoded(AccessTokenPayload))
                    .WithBasicAuthentication(Settings.ClientId, Settings.ClientSecret)
                    .As<BattleNetAccessTokenModel>();

                if (Response?.access_token != null)
                {
                    AppConstants.BattleNetAccessToken = Response;
                    AppConstants.BattleNetAccessToken.acquired_at = DateTime.UtcNow;
                    return true;
                }

                return false;
            }
            catch (ApiException ex)
            {
                // Log or inspect the response text for debugging purposes
                var responseText = await ex.Response.AsString();
                Console.WriteLine($"API Error: {responseText}");
                return false;
            }
        }

        public async Task<string> GetClassColor(string char_class)
        {
            var color = await _context.tbl_Classes
                                     .Where(c => c.Name == char_class)
                                     .Select(c => c.Color)
                                     .FirstOrDefaultAsync();

            return color ?? "black"; // Return "black" if no match found
        }
    }

}
