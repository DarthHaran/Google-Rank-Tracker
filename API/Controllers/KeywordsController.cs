using GRT.Data;
using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRT.Interfaces;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeywordsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ISearchProviderService _searchProvider;

        public KeywordsController(DataContext context, ISearchProviderService searchProvider)
        {
            _context = context;
            _searchProvider = searchProvider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Keyword>> Get(int id)
        {
            return await _context.Keywords.Include(x => x.Project).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keyword>>> GetByProjectId(int projectId)
        {
            return await _context.Keywords.Include(x => x.Project).Where(x => x.ProjectId == projectId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Keyword>> Add(Keyword keyword)
        {
            keyword.CityBase64 = _searchProvider.GetEncodedName(keyword.City);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Keywords.Add(keyword);
            await _context.SaveChangesAsync();
            return Ok(keyword);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var keyword = await _context.Keywords.FirstOrDefaultAsync(x => x.Id == id);

            if (keyword == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Keywords.Remove(keyword);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
