using GRT.Data;
using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeywordsController : ControllerBase
    {
        private readonly DataContext _context;

        public KeywordsController(DataContext context)
        {
            _context = context;
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
        public async Task<ActionResult<Keyword>> Post(Keyword keyword)
        {
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
