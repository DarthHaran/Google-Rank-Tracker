using GRT.Entities;
using GRT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordRepository _repository;

        public KeywordsController(IKeywordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Keyword>> Get(int id)
        {
            return Ok(await _repository.GetKeyword(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keyword>>> GetByProjectId(int projectId)
        {
            return Ok(await _repository.GetKeywordsOfProject(projectId));
        }

        [HttpPost]
        public async Task<ActionResult<Keyword>> Add(Keyword keyword)
        {
            await _repository.AddKeyword(keyword);
            return Ok(keyword);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var keyword = await _repository.GetKeyword(id);

            if (keyword == null)
            {
                return BadRequest();
            }

            await _repository.DeleteKeyword(keyword);
            return Ok();
        }
    }
}
