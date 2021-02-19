using GRT.Entities;
using GRT.Interfaces;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRT.Services
{
    public class SearchProviderService : ISearchProviderService
    {
        private readonly HtmlWeb web;

        public SearchProviderService()
        {
            web = new HtmlWeb();
        }

        public List<SearchEntry> GetResults(Keyword keyword)
        {
            var url = string.Format("https://www.{0}/search?&q={1}&gws_rd=cr&gl={2}&hl={3}&num=100&uule={4}", keyword.GoogleHost, keyword.KeywordName,
                keyword.Country, keyword.Language, GetUule(keyword.City));

            var doc = web.Load(url);

            var searchResults = doc.DocumentNode.SelectNodes("//div[contains(@class, 'g')]//div[contains(@class, 'tF2Cxc')]");

            return searchResults.Select(result =>
            {
                var titleUrlDiv = result.SelectSingleNode(".//div[contains(@class, 'yuRUbf')]")
                    .Descendants("a")
                    .FirstOrDefault();

                var contentDiv = result.SelectSingleNode(".//div[contains(@class, 'IsZvec')]");

                return new SearchEntry
                {
                    Title = titleUrlDiv.InnerText,
                    Url = titleUrlDiv.Attributes["href"].Value,
                    Content = contentDiv.InnerHtml
                };
            }).ToList();
        }

        public List<SearchEntry> GetResultsWithSelenium(Keyword keyword)
        {
            var url = string.Format("https://www.{0}/search?&q={1}&gws_rd=cr&gl={2}&hl={3}&num=100&uule={4}", keyword.GoogleHost, keyword.KeywordName,
                keyword.Country, keyword.Language, GetUule(keyword.City));
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            

            using (IWebDriver driver = new ChromeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);
                var results = driver.FindElements(By.ClassName("g"));

                return results.Select(result =>
                {
                    var j = result.FindElement(By.TagName("a"));

                    return new SearchEntry
                    {
                        Title = null,
                        Url = j.GetAttribute("href"),
                        Content = null
                    };
                }).ToList();
            }
        }

        private string GetUule(string text)
        {
            string[] chars = { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
                "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
                "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2",
                "3", "4", "5", "6", "7", "8", "9", "-", "_", "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "M", "T", "L"};

            string character = chars[text.Length - 1];

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            string location = Convert.ToBase64String(plainTextBytes);

            return "w+CAIQICI" + character + location;
        }
    }
}
