using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.EasyDelivery.BoundedContextExemplos1.Services;
using Application.EasyDelivery.BoundedContextExemplos1.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UI.API.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IExemploAppService _exemploAppService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IExemploAppService exemploAppService)
        {
            _logger = logger;
            _exemploAppService = exemploAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
