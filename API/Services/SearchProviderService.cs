using GRT.Entities;
using GRT.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRT.Services
{
    public class SearchProviderService : ISearchProviderService
    {
        public string GetEncodedName(string text)
        {
            string[] chars = { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
                "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
                "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2",
                "3", "4", "5", "6", "7", "8", "9", "-", "_", "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "M", "T", "L"};

            string character = chars[text.Length - 1];

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            string location = Convert.ToBase64String(plainTextBytes);

            string uule = character + location;

            return uule;
        }

        public List<SearchEntry> GetResults(Keyword keyword)
        {
            var url = string.Format("https://www.{0}/search?&q={1}&gws_rd=cr&gl={2}&hl={3}&num=100&uule=w+CAIQICI{4}", keyword.GoogleHost, keyword.KeywordName,
                keyword.Country, keyword.Language, keyword.CityBase64);

            var web = new HtmlWeb();
            var searchEntries = new List<SearchEntry>();


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
