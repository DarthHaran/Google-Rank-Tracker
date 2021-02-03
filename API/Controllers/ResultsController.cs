using GRT.Data;
using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRT.Extensions;
using GRT.Interfaces;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ISearchProviderService _searchProvider;

        public ResultsController(DataContext context, ISearchProviderService searchProvider)
        {
            _context = context;
            _searchProvider = searchProvider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetById(int id)
        {
            return await _context.Results.Include(x => x.Keyword).OrderBy(x => x.Id).LastOrDefaultAsync(x => x.KeywordId == id);
        }

        [HttpGet("keyword/{keywordId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetByKeywordId(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).Where(x => x.KeywordId == keywordId).ToListAsync();
        }

        [HttpPost("keyword/{keywordId}")]
        public async Task<ActionResult> Add(int keywordId)
        {
            var keyword = _context.Keywords.Include(x => x.Project).FirstOrDefault(x => x.Id == keywordId);
            var position = _searchProvider.GetResults(keyword).GetPosition(keyword);

            var newResult = new Result
            {
                Date = DateTime.Now,
                KeywordId = keywordId,
                Position = position,
            };

            await _context.Results.AddAsync(newResult);
            _context.SaveChanges();
            return Ok(newResult);
        }
    }
}
