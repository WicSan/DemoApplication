using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(WeatherForecastContext context, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastDto> Get()
        {
            return _context.WeatherForecasts.Select(w => new WeatherForecastDto
            {
                Date = w.Date,
                Summary = w.Summary,
                TemperatureC = w.TemperatureC,
                TemperatureF = w.TemperatureF
            });
        }
    }
}
