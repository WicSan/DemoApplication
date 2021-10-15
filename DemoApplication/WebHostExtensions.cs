using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoApplication
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<WeatherForecastContext>();

            context!.Database.EnsureCreated();

            if (context.WeatherForecasts.Any())
            {
                return host;
            }

            // now that the database is up to date. Let's seed
            new WeatherForecastSeeder(context).SeedData();

            return host;
        }
    }
}
