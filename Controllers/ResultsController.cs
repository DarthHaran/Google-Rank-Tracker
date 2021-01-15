using GRT.Data;
using GRT.Entities;
using GRT.GoogleSearchAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public ResultsController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetAllResults()
        {
            return await _context.Results.Include(x => x.Keyword).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResultById(int id)
        {
            return await _context.Results.Include(x => x.Keyword).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("int/{keywordId:int}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetKeywordsByProjectId(int keywordId)
        {
            return await _context.Results.Include(x => x.Keyword).Where(x => x.KeywordId == keywordId).ToListAsync();
        }

        [HttpGet("keyword/{id:int}")]
        public async Task<ActionResult> AddResult(int id)
        {
            string cx = _config.GetValue<string>("cx");
            string apiKey = _config.GetValue<string>("apiKey");
            DateTime date = DateTime.Now;
            Keyword keyword = _context.Keywords.Include(x => x.Project).FirstOrDefault(x => x.Id == id);
            string query = keyword.KeywordName;
            string domain = keyword.Project.Domain;
            int position = GoogleCustomSearch.SearchWithGoogle(query, domain, cx, apiKey);
            Result newResult = new Result();
            newResult.Date = date;
            newResult.KeywordId = id;
            newResult.Position = position;

            await _context.Results.AddAsync(newResult);
            _context.SaveChanges();
            return Ok(newResult);
        }
    }
}
