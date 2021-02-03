using GRT.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Services
{
    public class GoogleService
    {
        public static int returnPosition(List<SearchEntry> list, Keyword keyword)
        {
            string domain = keyword.Project.Domain;
            int postion = 0;

            foreach (var item in list)
            {
                postion += 1;

                if (item.Url.Contains(domain))
                {
                    break;
                }
            }

            return postion;
        }

        public static List<SearchEntry> ScrapeGoogle(Keyword keyword)
        {
            var url = string.Format("https://www.{0}/search?&q={1}&gl={2}&hl={3}&num=100&uule=w+CAIQICI{4}", keyword.GoogleHost, keyword.KeywordName, 
                keyword.Country, keyword.Language, keyword.CityBase64);
            var searchEntries = new List<SearchEntry>();
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var searchResults = doc.DocumentNode.SelectNodes("//div[contains(@class, 'g')]//div[contains(@class, 'tF2Cxc')]");

            foreach (var result in searchResults)
            {
                var titleUrlDiv = result.SelectSingleNode(".//div[contains(@class, 'yuRUbf')]")
                    .Descendants("a")
                    .FirstOrDefault();

                var contentDiv = result.SelectSingleNode(".//div[contains(@class, 'IsZvec')]");

                searchEntries.Add(new SearchEntry
                {
                    Title = titleUrlDiv.InnerText,
                    Url = titleUrlDiv.Attributes["href"].Value,
                    Content = contentDiv.InnerHtml
                });
            };

            return searchEntries;
        }
    }
}
