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

            var newsPage = await _client.GetAsync("https://worldofwarcraft.blizzard.com/en-gb/news").AsString();

            doc.LoadHtml(newsPage);

            var div = doc.DocumentNode.SelectSingleNode("//div[@class='List List--vertical List--separatorAll List--full']");

            HtmlNodeCollection divNodes = div.SelectNodes(".//div");
            List<WowNewsModel> newsPosts = [];

            if (divNodes is not null)
            {
                for (int node = 0; node < limit; node++)
                {
                    WowNewsModel news = new();
                    // string title = node.SelectSingleNode(".//h2/a")?.InnerText.Trim();                    
                    string backgroundImageUrl = divNodes[node].SelectSingleNode(".//div[@class='NewsBlog-image']")?.Attributes["data-src"]?.Value;
                     string link = divNodes[node].SelectSingleNode(".//a[@class='Link NewsBlog-link']")?.Attributes["href"]?.Value;
                    link = "https://worldofwarcraft.blizzard.com" + link;

                    news.Image = "https:" + backgroundImageUrl;
                    news.Title = divNodes[node].SelectSingleNode(".//div[@class='NewsBlog-title']")?.InnerText.Trim();
                    news.Link = link;
                    news.Description = divNodes[node].SelectSingleNode(".//div[@class='NewsBlog-desc color-beige-medium font-size-xSmall']")?.InnerText.Trim();
                    newsPosts.Add(news);
                }
                return newsPosts;
            }
            return null;
        }
    }
}
