using CUSTOM.Database.Api.Services;
using CUSTOM.SharedDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CUSTOM.Database.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDbProcessController : ControllerBase
    {
        private readonly IDbService _dbService;

        public SalesDbProcessController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost(nameof(AddDbSales))]
        [ProducesResponseType(typeof(AddSalesRes), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddSalesRes), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDbSales(AddSalesDbReq req)
        {
            var result = await _dbService.AddSales(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost(nameof(SalesDblist))]
        [ProducesResponseType(typeof(SalesListRes), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SalesListRes), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SalesDblist(SalesListReq req)
        {
            var result = await _dbService.SalesDbList(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}