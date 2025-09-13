using HtmlAgilityPack;
using LazyCache;
using Pathoschild.Http.Client;
using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.BattleNet.WoW.News;
using TwistingNether.DataAccess.BattleNet.WoW.Token;
using TwistingNether.DataAccess.Configuration;

namespace TwistingNether.Core.Services
{
    public class GeneralService(FluentClient client, IAppCache cache, Common common) : IGeneralService
    {
        private readonly FluentClient _client = client;
        private readonly IAppCache _cache = cache;
        private readonly Common _common = common;
        public async Task<List<WowNewsModel>?> GetNews(int? limit)
        {
            HtmlDocument doc = new();
            limit = limit == null ? 5 : limit; // If no limit was given set it to 5.

            var newsPage = await _client.GetAsync("https://worldofwarcraft.blizzard.com/en-gb/news").AsString();

            doc.LoadHtml(newsPage);

            // Get main news post
            var targetDiv = doc.DocumentNode.SelectSingleNode(
    "//div[contains(@class, 'Grid') and " +
           "contains(@class, 'SyncHeight') and " +
           "contains(@class, 'gutter-small') and " +
           "contains(@class, 'gutter-all') and " +
           "contains(@class, 'gutter-negative')]"
);
            List<WowNewsModel> newsList = [];

            int count = 0;
            for (int i = 0; count < limit; i++)
            {
                WowNewsModel newsModel = new();
                HtmlNode post;

                if (i < 5)
                {
                    if (i >= targetDiv.ChildNodes.Count) break;
                    post = targetDiv.ChildNodes[i];

                    if (!post.HasClass("ArticleTile")) continue;

                    newsModel.Title = post.SelectSingleNode(".//div[@class='ArticleTile-title']").InnerText;
                    newsModel.Link = post.SelectSingleNode(".//a[contains(@class, 'Link--external')]").GetAttributeValue("href", "");
                    newsModel.Image = ExtractBackgroundImageUrl(post.SelectSingleNode(".//div[@class='Tile-bg']").GetAttributeValue("style", ""));
                    newsModel.Description = post.SelectSingleNode(".//div[@class='ArticleTile-subtitle']").InnerText;
                }
                else
                {
                    var list = doc.DocumentNode.SelectSingleNode("//div[@class='List List--vertical List--separatorAll List--full']");
                    if (list == null || (i - 5) >= list.ChildNodes.Count) break;

                    post = list.ChildNodes[i - 5];
                    if (!post.HasClass("List-item")) continue;

                    newsModel.Title = post.SelectSingleNode(".//div[@class='NewsBlog-title']").InnerText;
                    newsModel.Link = "https://worldofwarcraft.blizzard.com" + post.SelectSingleNode(".//a[@class='Link NewsBlog-link']").GetAttributeValue("href", "");
                    newsModel.Image = "https://" + post.SelectSingleNode(".//img[@class='NewsBlog-image']").GetAttributeValue("data-src", "");
                    newsModel.Description = post.SelectSingleNode(".//p[@class='NewsBlog-desc color-beige-medium font-size-xSmall']").InnerText;
                }

                newsList.Add(newsModel);
                count++;
            }
            return newsList;
        }
        private static string ExtractBackgroundImageUrl(string input)
            =>
            "https://" + input
            .Replace("background-image:url(&quot;//", "")
            .Replace("background-image:url(&quot;https://", "")
            .Replace(");", "")
            .Replace("&quot;", "")
            .Trim();
        public async Task<WowTokenModel> GetTokenPrice()
        {
            await _common.GetNewBattleNetAccessToken();
            return await _cache.GetOrAddAsync("WowTokenPrice", async () =>
            {
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
        public async Task<WowItemMediaModel> GetItemMedia(string itemId)
        {
            await _common.GetNewBattleNetAccessToken();
            return await _cache.GetOrAddAsync($"GetItemMedia_{itemId}", async () =>
            {
                return await _client.GetAsync($"https://us.api.blizzard.com/data/wow/media/item/{itemId}")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "static-us" },
                        { "locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WowItemMediaModel>();

            }, TimeSpan.FromMinutes(60));
            
        }
    }
}