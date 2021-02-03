using GRT.Entities;
using System.Collections.Generic;

namespace GRT.Extensions
{
    public static class SearchProviderExtensions
    {
        public static int GetPosition(List<SearchEntry> searchEntries, Keyword keyword)
        {
            string domain = keyword.Project.Domain;
            var index = searchEntries.FindIndex(x => x.Url.Contains(domain));
            index = index == 0 ? searchEntries.Count : index + 1;

            return index;
        }
    }
}
