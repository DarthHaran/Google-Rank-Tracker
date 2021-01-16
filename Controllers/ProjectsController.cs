using GRT.Data;
using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
