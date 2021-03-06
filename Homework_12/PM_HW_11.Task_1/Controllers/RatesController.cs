using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Authentication;
using DepsWebApp.Middlewares;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Change currencies
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rates">list of rates</param>
        /// <param name="logger">logger</param>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Changes amount of one currency into another
        /// </summary>
        /// <param name="srcCurrency">Source currency</param>
        /// <param name="dstCurrency">Destination currency</param>
        /// <param name="amount">Amount to convert</param>
        /// <returns>Converted currency</returns>
        [Authorize]
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        [TypeFilter(typeof(CustomExceptionFilter))]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            if (!(HttpContext.User.Identity is AccountIdentity identity))
                return Unauthorized();
            
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogInformation($"User encoded as: {identity.Base64String} requested value change.");

            var exchange = await _rates.
                ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (exchange.HasValue) return exchange.Value.DestinationAmount;
            
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
            return BadRequest("Invalid currency code");
        }
    }
}
