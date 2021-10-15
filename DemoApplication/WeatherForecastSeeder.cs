using System;
using System.Linq;

namespace DemoApplication
{
    public class WeatherForecastSeeder
    {
        private readonly WeatherForecastContext _context;

        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastSeeder(WeatherForecastContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            var rng = new Random();
            var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

            foreach (var weatherForecast in weatherForecasts)
            {
                _context.WeatherForecasts.Add(weatherForecast);
            }
            
            _context.SaveChanges();
        }
    }
}
