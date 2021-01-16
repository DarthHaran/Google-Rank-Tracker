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
    }
}
