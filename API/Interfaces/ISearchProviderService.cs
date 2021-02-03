using GRT.Entities;
using System.Collections.Generic;

namespace GRT.Interfaces
{
    public interface ISearchProviderService
    {
        List<SearchEntry> GetResults(Keyword keyword);
        string GetEncodedName(string text);
    }
}
