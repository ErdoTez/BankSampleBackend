using CUSTOM.Services.SalesProcess.Services;
using CUSTOM.SharedDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace CUSTOM.BankSample.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpPost(nameof(AddSales))]
        [ProducesResponseType(typeof(AddSalesRes), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddSalesRes), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSales(AddSalesReq req)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //Console Log
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(AddSales), JsonSerializer.Serialize(req));

            var result = await _salesService.AddSales(req);

            stopWatch.Stop();
            _logger.LogInformation($"Executing {nameof(AddSales)} with response: {JsonSerializer.Serialize(result)} time: {stopWatch.Elapsed}");
            if (result.ResultCode == ResultCode.Success) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost(nameof(SalesList))]
        [ProducesResponseType(typeof(SalesListRes), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SalesListRes), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SalesList(SalesListReq req)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(SalesList), JsonSerializer.Serialize(req));

            var result = await _salesService.SalesList(req);

            stopWatch.Stop();
            _logger.LogInformation($"Executing {nameof(SalesList)} with response: {JsonSerializer.Serialize(result)} time: {stopWatch.Elapsed}");
            if (result.ResultCode == ResultCode.Success) return Ok(result);
            else return BadRequest(result);
        }
    }
}