using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task_1.Services.Interfaces;

namespace Task_1.Controllers
{
    [ApiController]
    [Route("/primes")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PrimesController : ControllerBase
    {
        private readonly IPrimeAlgorithm _primeAlgorithm;
        private readonly ILogger<PrimesController> _logger;

        public PrimesController(
            IPrimeAlgorithm primeAlgorithm,
            ILogger<PrimesController> logger)
        {
            _primeAlgorithm = primeAlgorithm;
            _logger = logger;
        }

        [HttpGet("{number}")]
        [ProducesResponseType(typeof(string),(int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string),(int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> IsPrime(int number)
        {
            try
            {
                var result = await _primeAlgorithm.IsPrime(number);
                if(result)
                    return Ok($"{number} prime");
                return NotFound($"{number} not prime");
            }
            catch (Exception argumentOut)
            {
                _logger.LogError(argumentOut.Message);
                return NotFound(argumentOut.Message);
            }
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<int>),(int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<int>>> GetPrimesFromRange(
            [FromQuery] string from, 
            [FromQuery] string to)
        {
            try
            {
                var result = await _primeAlgorithm.GetPrimes(from, to);
                
                if (result.Primes is null)
                    return BadRequest("Wrong interval");
                
                return Ok(result.Primes);
            }
            catch (Exception argumentOutOfRange)
            {
                _logger.LogError(argumentOutOfRange.Message);
                return BadRequest(argumentOutOfRange.Message);
            }
        }
    }
}