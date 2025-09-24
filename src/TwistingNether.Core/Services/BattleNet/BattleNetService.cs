using LazyCache;
using Pathoschild.Http.Client;
using TwistingNether.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.OAuth;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services.BattleNet
{
    public class BattleNetService(FluentClient client, IAppCache cache) : IBattleNetService
    {
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;

        private async Task GetNewBattleNetAccessTokenAsync()
        {
            // If there is an access token already, and it has not expired yet.
            if (AppConstants.BattleNetAccessToken != null &&
                AppConstants.BattleNetAccessToken.acquired_at.AddSeconds(AppConstants.BattleNetAccessToken.expires_in).AddMinutes(-5) > DateTime.UtcNow)
            {
                return;
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
                }

            }
            catch (ApiException ex)
            {
                var responseText = await ex.Response.AsString();
                Console.WriteLine($"Error fetching oauth token from battle.net: {responseText}");
            }
            return;
        }
        public async Task<List<CharacterMediaModel>> GetCharacterMediaAsync(CharacterRequestModel character)
        {
            await GetNewBattleNetAccessTokenAsync();
            try
            {
                IResponse data = await _client
                    .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/character-media?namespace=profile-{character.Region}&locale=en_US&:region={character.Region}")
                    .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token);

                WoWCharacterMediaModel characterMedia = await data.As<WoWCharacterMediaModel>();

                return characterMedia.assets
                    .ConvertAll(asset => new CharacterMediaModel
                    {
                        Link = asset.value,
                        Type = asset.key
                    });
            }
            catch (ApiException ex)
            {
                // TODO: Implement global exception handling
                Console.WriteLine($"Error fetching character media for {character.Name} on {character.Realm} in {character.Region}: {await ex.Response.AsString()}");
                throw;
            }
        }
        public async Task<string> GetItemMediaAsync(string id)
        {
            await GetNewBattleNetAccessTokenAsync();
            return await _cache.GetOrAddAsync($"item-media-{id}", async () =>
            {
                WowItemMediaModel res = await _client.GetAsync($"https://us.api.blizzard.com/data/wow/media/item/{id}")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "static-us" },
                        {"locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WowItemMediaModel>();
                return res.assets.FirstOrDefault(a => a.key == "icon")?.value ?? "";
            }, TimeSpan.MaxValue);
        }

        public async Task<WoWCharacterEquipmentModel> GetCharacterEquipmentAsync(CharacterRequestModel character)
        {
            await GetNewBattleNetAccessTokenAsync();

            try
            {
                IResponse response = await _client
                        .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/equipment?namespace=profile-{character.Region}&locale=en_US&:region={character.Region}")
                        .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token);

                WoWCharacterEquipmentModel characterGear = await response.As<WoWCharacterEquipmentModel>();

                var iconTasks = characterGear.equipped_items
                .Where(item => item.item != null)
                .Select(async item => item.item.iconUrl = await GetItemMediaAsync(item.item.id.ToString()));

                await Task.WhenAll(iconTasks);

                return characterGear;
            }
            catch (ApiException ex)
            {
                // TODO: Implement global exception handling
                Console.WriteLine($"Error fetching character media for {character.Name} on {character.Realm} in {character.Region}: {await ex.Response.AsString()}");
                throw;
            }
        }
    }
}
