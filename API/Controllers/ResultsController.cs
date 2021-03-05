using GRT.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GRT.Extensions;
using GRT.Interfaces;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using CsvHelper;
using System.Globalization;
using System.Text;

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

        [HttpGet("report/{id}")]
        public async Task<FileResult> GetMonthlyReport(int id)
        {
            IEnumerable<Keyword> keywords = await _keywordRepository.GetKeywordsOfProject(id);
            List<ReportRow> rows = new List<ReportRow>();

            foreach (var k in keywords)
            {
                var lastResult = await _resultRepository.GetLastResult(k.Id);
                var lastMonthsResult = await _resultRepository.GetLastMonthsResult(k.Id);
                var row = new ReportRow
                {
                    KeywordName = k.KeywordName,
                    LastPosition = lastResult.Position,
                    LastMonthsPosition = lastMonthsResult.Position
                };

                rows.Add(row);
            }

            using (var writer = new StreamWriter(@"C:\Users\Haran\source\repos\CSV_console\report.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(rows);
                }
            }

            var reader = new StreamReader(@"C:\Users\Haran\source\repos\CSV_console\report.csv");
            byte[] file = Encoding.UTF8.GetBytes(reader.ReadToEnd().ToString());
            reader.Close();

            return File(file, "text/csv", "report.csv");
        }

        [HttpGet("report-current/{id}")]
        public async Task<FileResult> GetCurrentPositionsReport(int id)
        {
            IEnumerable<Keyword> keywords = await _keywordRepository.GetKeywordsOfProject(id);
            List<Tuple<string, int>> rows = new List<Tuple<string, int>>();

            foreach (var k in keywords)
            {
                var lastResult = await _resultRepository.GetLastResult(k.Id);
                Tuple<string, int> row = new Tuple<string, int> (k.KeywordName, lastResult.Position);

                rows.Add(row);
            }

            using (var writer = new StreamWriter(@"C:\Users\Haran\source\repos\CSV_console\report.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(rows);
                }
            }

            var reader = new StreamReader(@"C:\Users\Haran\source\repos\CSV_console\report.csv");
            byte[] file = Encoding.UTF8.GetBytes(reader.ReadToEnd().ToString());
            reader.Close();

            return File(file, "text/csv", "report.csv");
        }
    }
}
