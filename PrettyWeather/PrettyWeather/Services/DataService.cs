using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AppCenter.Data;

using PrettyWeather.Model;
using Microsoft.AppCenter;

namespace PrettyWeather.Services
{
    public class DataService
    {
        static HttpClient httpClient = new HttpClient();

        readonly string functionsUrl = "https://prettyweather.azurewebsites.net/api/GetCurrentConditions";
        public async Task< List<CityInfo>> SearchForCities(string searchText)
        {
            var cityInfo = new List<CityInfo>
            {
                new CityInfo{ CityName="Seattle", State="WA"},
                new CityInfo{ CityName="Madison", State="WI"}
            };

            return await Task.FromResult(cityInfo);
        }

        public async Task<WeatherInfo> GetWeatherInfo(double latitude, double longitude)
        {
            try
            {
                var infoJson = await httpClient.GetStringAsync($"{functionsUrl}?lat={latitude}&long={longitude}");

                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(infoJson);

                return weatherInfo;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }

    }
}