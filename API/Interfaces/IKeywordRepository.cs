using GRT.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Interfaces
{
    public interface IKeywordRepository
    {
        Task<Keyword> GetKeyword(int id);
        Task<IEnumerable<Keyword>> GetKeywordsOfProject(int projectId);
        Task AddKeyword(Keyword keyword);
        Task DeleteKeyword(Keyword keyword);
    }
}
