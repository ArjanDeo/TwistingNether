using LazyCache;
using Pathoschild.Http.Client;
using HtmlAgilityPack;
using TwistingNether.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.OAuth;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.BattleNet.WoW.News;
using TwistingNether.DataAccess.BattleNet.WoW.Token;
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
            character.Name = character.Name.ToLower();
            character.Realm = character.Realm.ToLower();
            character.Region = character.Region.ToLower();
            character.Realm = character.Realm.Replace(" ", "-").Replace("\'", "");

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
        public async Task<WoWCharacterRaidsModel> GetCharacterRaidsAsync(CharacterRequestModel character)
        {
            await GetNewBattleNetAccessTokenAsync();

            IResponse response = await _client
                .GetAsync($"https://{character.Region}.api.blizzard.com/profile/wow/character/{character.Realm}/{character.Name}/encounters/raids?namespace=profile-{character.Region}&locale=en_US")
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token);
            return await response.As<WoWCharacterRaidsModel>();
        }

        public async Task<WowTokenModel> GetTokenPriceAsync()
        {
            return await _cache.GetOrAddAsync("WowTokenPrice", async () =>
            {
                await GetNewBattleNetAccessTokenAsync();

                return await _client.GetAsync("https://us.api.blizzard.com/data/wow/token/index")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "dynamic-us" },
                        {"locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WowTokenModel>();
            }, TimeSpan.FromMinutes(20));
        }

        public async Task<List<WowNewsModel>> GetWowNews()
        {
            const string WowNewsUrl = "https://worldofwarcraft.com/en-us/news";

            var web = new HtmlWeb();
            var doc = web.Load(WowNewsUrl);
            var featuredNewsGrid = doc.DocumentNode.SelectSingleNode(
                "//div[contains(@class, 'Grid') and contains(@class, 'SyncHeight') and contains(@class, 'gutter-small') and contains(@class, 'gutter-all') and contains(@class, 'gutter-negative')]"
            );

            var newsItems = featuredNewsGrid.SelectNodes(
                ".//div[contains(@class, 'ArticleTile') and contains(@class, 'ArticleTile--gutter')]"
            ).Take(5).ToList();

            List<WowNewsModel> newsPosts = [];
            for (int i = 0; i < newsItems.Count; i++)
            {
                string backgroundURL = "https://" + newsItems[i]
                    .SelectSingleNode(".//div[contains(@class, 'Tile-bg')]")
                    .GetAttributeValue("style", "")
                    .Split("//")[1]
                    .Split("&")[0];
                string title = newsItems[i].SelectSingleNode(".//div[contains(@class, 'ArticleTile-title')]").InnerText;
                string subtitle = newsItems[i].SelectSingleNode(".//div[contains(@class, 'ArticleTile-subtitle')]").InnerText;
                HtmlNode? link = newsItems[i].SelectSingleNode(".//a[contains(@class, 'ArticleTile-link')]");
                newsPosts.Add(new WowNewsModel
                {
                    Image = backgroundURL,
                    Title = title,
                    Subtitle = subtitle,
                    Link = link != null ? link.GetAttributeValue("href", "No Link Found") : "No Link Found"
                });
            }

            return newsPosts;
        }
        public async Task<List<OverwatchNewsModel>> GetOverwatchNews()
        {
            const string OverwatchNewsUrl = "https://overwatch.blizzard.com/en-us/news";

            var web = new HtmlWeb();
            var doc = web.Load(OverwatchNewsUrl);
            var featuredNewsGrid = doc.DocumentNode.SelectSingleNode(
                "//div[contains(@class, 'news-header')]"
            );

            var newsItems = featuredNewsGrid.SelectNodes(
                ".//a[contains(@slot, 'gallery-items')]"
            ); // OW News only has 4 compared to WoW's 5

            List<OverwatchNewsModel> newsPosts = [];
            for (int i = 0; i < newsItems.Count; i++)
            {
                string backgroundURL = newsItems[i]
                    .SelectSingleNode(".//blz-image[contains(@slot, 'media')]")
                    .GetAttributeValue("src", "");

                string title = newsItems[i].SelectSingleNode(".//h3[contains(@slot, 'heading')]").InnerText;
                string link = OverwatchNewsUrl + newsItems[i].GetAttributeValue("href", "No Link Found").Substring(5);
                newsPosts.Add(new OverwatchNewsModel
                {
                    Image = backgroundURL,
                    Title = title,
                    Link = link
                });
            }

            return newsPosts;
        }
    }
}
