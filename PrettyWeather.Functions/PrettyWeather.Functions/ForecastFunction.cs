using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace PrettyWeather.Functions
{
    public static class ForecastFunction
    {
        static HttpClient client = new HttpClient();

        [FunctionName("GetCurrentConditions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            double latitude = double.Parse(req.Query["lat"]);
            double longitude = double.Parse(req.Query["long"]);

            var key = Environment.GetEnvironmentVariable("WeatherKey");
            var baseUrl = Environment.GetEnvironmentVariable("WeatherUrl");
            var excludeParams = Environment.GetEnvironmentVariable("WeatherParams");

            // Make a request to the Darksky API
            var weatherJson = await client.GetStringAsync($"{baseUrl}/{key}/{latitude},{longitude}?{excludeParams}");

            var weatherData = JsonConvert.DeserializeObject<WeatherConditions>(weatherJson);

            return new OkObjectResult(new 
            { 
                weatherData.Currently.Icon, 
                weatherData.Currently.Summary, 
                Temperature = (int)Math.Round(weatherData.Currently.Temperature,0),
                Pressure = (int)Math.Round(weatherData.Currently.Pressure,0),
                UvIndex = (int)weatherData.Currently.UvIndex,
                WindSpeed = (int)Math.Round(weatherData.Currently.WindSpeed,0),
                WindBearing = (int)weatherData.Currently.WindBearing,
                Humidity = (int)Math.Round(weatherData.Currently.Humidity * 100, 0),
                PrecipProbability = (int)weatherData.Currently.PrecipProbability
            });

        }
    }
}
