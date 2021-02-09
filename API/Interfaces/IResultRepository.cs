using GRT.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Interfaces
{
    public interface IResultRepository
    {
        Task<Result> GetLastResult(int keywordId);
        Task<IEnumerable<Result>> GetAllResults(int keywordId);
        Task AddResult(Result result);
    }
}
