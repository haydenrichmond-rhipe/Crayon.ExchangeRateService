using Crayon.ExchangeRates.Services;
using Crayon.ExchangeRateService.Contracts.Requests;
using Crayon.ExchangeRateService.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crayon.ExchangeRateService.Controllers
{
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(
            IExchangeRateService exchangeRateService,
            ILogger<ExchangeRateController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/exchange-rates/{sourceCurrency}/{targetCurrency}/{dates}")]
        public async Task<ActionResult<ExchangeRateResponse>> GetExchangeRateData(
            [FromRoute] ExchangeRateRequest request
            )
        {
            try
            {
                return Ok(await _exchangeRateService.GetExchangeRateData(request));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred in request to get excahnge rates.");
                return BadRequest(ex.Message);
            }
        }
    }
}
