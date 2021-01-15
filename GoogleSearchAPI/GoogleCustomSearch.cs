using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace GRT.GoogleSearchAPI
{
    public static class GoogleCustomSearch
    {
        public static int SearchWithGoogle(string query, string domain, string cx, string apiKey)
        {
            WebClient webClient = new WebClient();

            string gl = "rs";
            string hl = "sr";
            string googlehost = "google.rs";

            bool found = false;
            int start = 1;
            int tempPosition = 0;

            while (found == false && start < 100)
            {
                string result = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&gl={3}&start={4}&hl={5}&googlehost={6}alt=json", apiKey, cx, query, gl, start, hl, googlehost));
                JObject obj = JObject.Parse(result);
                var token = (JArray)obj.SelectToken("items");

                foreach (var item in token)
                {
                    string json = JsonConvert.SerializeObject(item.SelectToken("displayLink"));
                    tempPosition += 1;

                    if (json.Contains(domain))
                    {
                        found = true;
                        break;
                    }
                }
                start += 10;
            }
            return tempPosition;
        }
    }
}
