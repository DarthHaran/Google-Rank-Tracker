using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GRT.Extensions;
using GRT.Interfaces;

namespace GRT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;
        private readonly IKeywordRepository _keywordRepository;
        private readonly ISearchProviderService _searchProvider;

        public ResultsController(IResultRepository resultRepository,
            IKeywordRepository keywordRepository,
            ISearchProviderService searchProvider)
        {
            _resultRepository = resultRepository;
            _keywordRepository = keywordRepository;
            _searchProvider = searchProvider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetLastResult(int id)
        {
            return Ok(await _resultRepository.GetLastResult(id));
        }

        [HttpGet("keyword/{keywordId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetByKeywordId(int keywordId)
        {
            return Ok(await _resultRepository.GetAllResults(keywordId));
        }

        [HttpPost("keyword/{keywordId}")]
        public async Task<ActionResult> Add(int keywordId)
        {
            var keyword = await _keywordRepository.GetKeyword(keywordId);
            var position = _searchProvider.GetResultsWithSelenium(keyword).GetPosition(keyword);

            var newResult = new Result
            {
                Date = DateTime.Now,
                KeywordId = keywordId,
                Position = position,
            };

            await _resultRepository.AddResult(newResult);
            return Ok(newResult);
        }
    }
}
