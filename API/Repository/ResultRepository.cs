using GRT.Data;
using GRT.Entities;
using GRT.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly DataContext _context;

        public ResultRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddResult(Result result)
        {
            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Result>> GetAllResults(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).Where(x => x.KeywordId == keywordId).ToListAsync();
        }

        public async Task<Result> GetLastMonthsResult(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).OrderBy(x => x.Id).Where(x =>
                x.Date.Month < DateTime.Now.Month).LastOrDefaultAsync(x => x.KeywordId == keywordId);
        }

        public async Task<Result> GetLastResult(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).OrderBy(x => x.Id).LastOrDefaultAsync(x => x.KeywordId == keywordId);
        }
    }
}
