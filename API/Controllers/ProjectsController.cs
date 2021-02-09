using GRT.Entities;
using GRT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _repository;

        public ProjectsController(IProjectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            return Ok(await _repository.GetProjectsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            return Ok(await _repository.GetProjectAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Add(Project project)
        {
            await _repository.AddProject(project);
            return Ok(project);
        }

        [HttpPut]
        public async Task<ActionResult<Project>> Update (Project project)
        {
            await _repository.UpdateProject(project);
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var project = await _repository.GetProjectAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.DeleteProject(project);
                return Ok();
            }
        }
    }
}
