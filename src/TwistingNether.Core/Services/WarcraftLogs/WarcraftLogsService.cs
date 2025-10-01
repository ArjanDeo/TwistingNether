using LazyCache;
using Pathoschild.Http.Client;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.WarcraftLogs;
using TwistingNether.DataAccess.WarcraftLogs.OAuth;

namespace TwistingNether.Core.Services.WarcraftLogs
{
    public class WarcraftLogsService(FluentClient client, IAppCache cache) : IWarcraftLogsService
    {
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;

        private async Task<bool> GetNewWarcraftLogsAccessToken()
        {
            // If there is an access token already, and it has not expired yet.
            if (AppConstants.WarcraftLogsAccessToken != null &&
                AppConstants.WarcraftLogsAccessToken.acquired_at.AddSeconds(AppConstants.WarcraftLogsAccessToken.expires_in).AddMinutes(-5) > DateTime.UtcNow)
            {
                return true;
            }

            Dictionary<string, string> AccessTokenPayload = new()
            {
                ["grant_type"] = "client_credentials"
            };

            try
            {
                var Response = await _client
                    .PostAsync("https://www.warcraftlogs.com/oauth/token")
                    .WithBody(AccessTokenPayload)
                    .WithBasicAuthentication(Settings.WarcraftLogsClientId, Settings.WarcraftLogsClientSecret)
                    .As<WarcraftLogsAccessTokenModel>();

                if (Response?.access_token != null)
                {
                    AppConstants.WarcraftLogsAccessToken = Response;
                    AppConstants.WarcraftLogsAccessToken.acquired_at = DateTime.UtcNow;
                    return true;
                }

                return false;
            }
            catch (ApiException ex)
            {
                var responseText = await ex.Response.AsString();
                Console.WriteLine($"Error fetching oauth token from battle.net: {responseText}");
                return false;
            }
        }
        public async Task<ZoneRankings> GetLatestRaidPerformanceAsync(CharacterRequestModel character, int? difficulty)
        {
            return await _cache.GetOrAddAsync($"GetLatestRaidPerformance-{character.Region}-{character.Realm}-{character.Name}-{difficulty}", async () =>
            {
                await GetNewWarcraftLogsAccessToken();
                try
                {
                    const string query = """                     
                    query CharacterData($name: String!, $serverSlug: String!, $serverRegion: String!, $difficulty: Int) 
                    { characterData 
                        { character(name: $name, serverSlug: $serverSlug, serverRegion: $serverRegion) 
                            { canonicalID classID hidden zoneRankings(difficulty: $difficulty) } 
                        } 
                    }
                    """;

                    Dictionary<string, object> payload = new()
                    {
                        ["query"] = query,
                        ["variables"] = new
                        {
                            name = character.Name,
                            serverSlug = character.Realm,
                            serverRegion = character.Region,
                            difficulty = difficulty != null ? difficulty : 0
                        }
                    };
                    IResponse response = await _client
                    .PostAsync("https://www.warcraftlogs.com/api/v2/client")
                    .WithBearerAuthentication(AppConstants.WarcraftLogsAccessToken.access_token)
                    .WithBody(payload);
                    var stringResponse = await response.AsString();
                    var formattedResponse = await response.As<WarcraftLogsCharacterDataModel>();

                    return formattedResponse.data.characterData.character.zoneRankings;
                }
                catch (ApiException ex)
                {
                    throw new Exception($"There was an issue trying to get the character's raid performance. {await ex.Response.AsString()}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }, TimeSpan.FromMinutes(60));
        }
    }
}
