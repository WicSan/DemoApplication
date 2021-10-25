using Xunit;

namespace DemoApplication.Tests
{
    public class WeatherForecastTest
    {
        [Theory]
        [InlineData(32, 89)]
        [InlineData(0, 32)]
        [InlineData(-23, -9)]
        public void TestTemperatureConversion(int temperatureC, int expectedTemperatureF)
        {
            var forecast = new WeatherForecast
            {
                TemperatureC = temperatureC,
            };

            Assert.Equal(expectedTemperatureF ,forecast.TemperatureF);
        }
    }
}
