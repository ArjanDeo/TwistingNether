using HtmlAgilityPack;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistingNether.DataAccess.BattleNet.WoW.News;

namespace TwistingNether.Core.Services
{
    public class GeneralService(FluentClient client) : IGeneralService
    {
        private readonly FluentClient _client = client;
        public async Task<List<WowNewsModel>?> GetNews(int? limit)
        {
            HtmlDocument doc = new();
            limit = limit == null ? 5 : limit; // If no limit was given set it to 5.

            var newsPage = await _client.GetAsync("https://news.blizzard.com/en-gb/world-of-warcraft").AsString();

            doc.LoadHtml(newsPage);

            var ol = doc.DocumentNode.SelectSingleNode("//ol[@class='ArticleList']");

            HtmlNodeCollection liNodes = ol.SelectNodes(".//li");
            List<WowNewsModel> newsPosts = [];

            if (liNodes is not null)
            {
                for (int node = 0; node < limit; node++)
                {
                    WowNewsModel news = new();
                    // string title = node.SelectSingleNode(".//h2/a")?.InnerText.Trim();                    
                    string backgroundImageStyle = liNodes[node].SelectSingleNode(".//div[@class='ArticleListItem-image']")?.Attributes["style"]?.Value;
                    news.Image = backgroundImageStyle;
                    int startIndex = news.Image.IndexOf("url(") + 4;
                    int endIndex = news.Image.LastIndexOf(")");
                    string extractedUrl = news.Image[startIndex..endIndex];
                    string link = liNodes[node].SelectSingleNode(".//h2/a")?.Attributes["href"]?.Value;

                    link = link.Replace("world-of-warcraft", "news");
                    link = "https://worldofwarcraft.blizzard.com" + link;

                    news.Image = "https:" + extractedUrl;
                    news.Title = liNodes[node].SelectSingleNode(".//h2/a")?.InnerText.Trim();
                    news.Link = link;
                    news.Description = liNodes[node].SelectSingleNode(".//div[@class='ArticleListItem-description']")?.InnerText.Trim();
                    newsPosts.Add(news);
                }
                return newsPosts;
            }
            return null;
        }
    }
}
