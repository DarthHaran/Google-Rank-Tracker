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
    public class KeywordRepository : IKeywordRepository
    {
        private readonly DataContext _context;

        public KeywordRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddKeyword(Keyword keyword)
        {
            _context.Keywords.Add(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKeyword(Keyword keyword)
        {
            _context.Keywords.Remove(keyword);
            await _context.SaveChangesAsync();
        }

        public async Task<Keyword> GetKeyword(int id)
        {
            return await _context.Keywords.Include(x => x.Project).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Keyword>> GetKeywordsOfProject(int projectId)
        {
            return await _context.Keywords.Include(x => x.Project).Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
