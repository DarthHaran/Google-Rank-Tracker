using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace GRT.GoogleSearchAPI
{
    public class GoogleCustomSearch : IGoogleCustomSearch
    {
        private readonly string _gl = "rs";
        private readonly string _hl = "sr";
        private readonly string _googlehost = "google.rs";
        private readonly string _cx;
        private readonly string _apiKey;

        public GoogleCustomSearch(IConfiguration config)
        {
            _cx = config.GetValue<string>("cx");
            _apiKey = config.GetValue<string>("apiKey");
        }

        public int GetPosition(string query, string domain)
        {
            var webClient = new WebClient();
            int position = 0;

            for (int start = 0; start < 100; start += 10)
            {
                string result = webClient.DownloadString(string.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&gl={3}&start={4}&hl={5}&googlehost={6}alt=json", _apiKey, _cx, query, _gl, start, _hl, _googlehost));
                var tokenItems = JObject.Parse(result).SelectToken("items");
                foreach (var item in tokenItems)
                {
                    string json = JsonConvert.SerializeObject(item.SelectToken("displayLink"));
                    position++;
                    if (json.Contains(domain))
                    {
                        return position;
                    }
                }
            }

            return position;
        }
    }
}
