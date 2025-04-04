using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pathoschild.Http.Client;
using TwistingNether.DataAccess;
using TwistingNether.DataAccess.BattleNet.OAuth;
using TwistingNether.DataAccess.Configuration;

namespace TwistingNether.Core
{
    public class Common(FluentClient client/*ApplicationDbContext context*/)
    {
        private readonly FluentClient _client = client;
        //private readonly ApplicationDbContext _context = context;

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
        public class CharacterClass
        {
            public int Id { get; set; }
            public string ClassName { get; set; }
            public string ColorCode { get; set; }
        }

        public async Task<string> GetClassColor(string char_class)
        {
            var characterClasses = new List<CharacterClass>
        {
            new CharacterClass { Id = 1, ClassName = "Warrior", ColorCode = "#C69B6D" },
            new CharacterClass { Id = 2, ClassName = "Hunter", ColorCode = "#AAD372" },
            new CharacterClass { Id = 3, ClassName = "Mage", ColorCode = "#3FC7EB" },
            new CharacterClass { Id = 4, ClassName = "Rogue", ColorCode = "#FFF468" },
            new CharacterClass { Id = 5, ClassName = "Priest", ColorCode = "#FFFFFF" },
            new CharacterClass { Id = 6, ClassName = "Warlock", ColorCode = "#8788EE" },
            new CharacterClass { Id = 7, ClassName = "Paladin", ColorCode = "#F48CBA" },
            new CharacterClass { Id = 8, ClassName = "Druid", ColorCode = "#FF7C0A" },
            new CharacterClass { Id = 9, ClassName = "Shaman", ColorCode = "#0070DD" },
            new CharacterClass { Id = 10, ClassName = "Monk", ColorCode = "#00FF98" },
            new CharacterClass { Id = 11, ClassName = "Demon Hunter", ColorCode = "#A330C9" },
            new CharacterClass { Id = 12, ClassName = "Death Knight", ColorCode = "#C41E3A" },
            new CharacterClass { Id = 13, ClassName = "Evoker", ColorCode = "#33937F" }
        };
            var color = characterClasses
                                     .Where(c => c.ClassName == char_class)
                                     .Select(c => c.ColorCode).FirstOrDefault();

            return color ?? "black"; // Return "black" if no match found
        }
    }

}
