using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AppCenter.Data;
using Newtonsoft.Json;
using PrettyWeather.Model;


namespace PrettyWeather.Services
{
    public class DataService
    {
        static HttpClient httpClient = new HttpClient();

        readonly string functionsUrl = "https://prettyweather.azurewebsites.net/api/GetCurrentConditions";

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

        public async Task<IEnumerable<CityInfo>> GetSavedCities()
        {
            var allSavedCities = new List<CityInfo>();

            var savedCitiesFromCosmos = await Data.ListAsync<CityInfo>(DefaultPartitions.UserDocuments);

            allSavedCities.AddRange(savedCitiesFromCosmos.CurrentPage.Items.Select(ci => ci.DeserializedValue));

            while (savedCitiesFromCosmos.HasNextPage)
            {
                await savedCitiesFromCosmos.GetNextPageAsync();

                allSavedCities.AddRange(savedCitiesFromCosmos.CurrentPage.Items.Select(ci => ci.DeserializedValue));
            }

            return allSavedCities;
        }

        public async Task SaveCity(CityInfo city)
        {
            try
            {
                await Data.CreateAsync<CityInfo>($"{city.CityName.Replace(" ",string.Empty)}-{city.State}", city,
                    DefaultPartitions.UserDocuments);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }

    }
}