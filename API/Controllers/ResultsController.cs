using GRT.Data;
using GRT.Entities;
using GRT.GoogleSearchAPI;
using GRT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IGoogleCustomSearch _googleCustomSearch;

        public ResultsController(DataContext context, IGoogleCustomSearch googleCustomSearch)
        {
            _context = context;
            _googleCustomSearch = googleCustomSearch;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> Get(int id)
        {
            return await _context.Results.Include(x => x.Keyword).OrderBy(x => x.Id).LastOrDefaultAsync(x => x.KeywordId == id);
        }

        [HttpGet("keyword/{keywordId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetByKeywordId(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).Where(x => x.KeywordId == keywordId).ToListAsync();
        }

        [HttpPost("keyword/{keywordId}")]
        public async Task<ActionResult> AddByKeywordId(int keywordId)
        {
            var keyword = _context.Keywords.Include(x => x.Project).FirstOrDefault(x => x.Id == keywordId);
            var results = GoogleService.ScrapeGoogle(keyword);
            var position = GoogleService.returnPosition(results, keyword);

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
