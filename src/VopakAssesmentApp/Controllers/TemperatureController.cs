using System;
using System.Collections.Generic;
using System.Linq;
using VopakAssesmentApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VopakAssesmentApp.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VopakAssesmentApp.Controllers
{
    [ApiController]
    [Route("temperature")]
    [Produces("application/json")]
    public class TemperatureController : ControllerBase
    {
        private readonly IOpenWeatherApiCaller _openWeatherApiCaller;

        public TemperatureController(IOpenWeatherApiCaller openWeatherApiCaller)
        {
            _openWeatherApiCaller = openWeatherApiCaller;
        }

        [HttpGet("currenttemp")]
        [ProducesResponseType(typeof(Temperature), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentTemperature([FromQuery] string city)
        {
            if(string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Invalid city name!");
            }

            var response = await _openWeatherApiCaller.GetTemperatureByCity(city.ToLower());

            return Ok(new Temperature { TemperatureInCelcius = response });
        }
    }
}
