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

        [HttpPost]
        public async Task<ActionResult<Project>> Post(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpPut]
        public async Task<ActionResult<Project>> Put (Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            try
            {
                _context.Entry(project).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
