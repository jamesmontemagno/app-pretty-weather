using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PrettyWeather.Model
{
    public class CityInfo
    {
        [JsonProperty("city")]
        public string CityName { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
