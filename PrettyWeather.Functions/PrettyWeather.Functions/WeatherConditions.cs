namespace PrettyWeather.Functions
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class WeatherConditions
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("currently")]
        public Currently Currently { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public partial class Currently
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("nearestStormDistance")]
        public long NearestStormDistance { get; set; }

        [JsonProperty("nearestStormBearing")]
        public long NearestStormBearing { get; set; }

        [JsonProperty("precipIntensity")]
        public long PrecipIntensity { get; set; }

        [JsonProperty("precipProbability")]
        public long PrecipProbability { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonProperty("apparentTemperature")]
        public double ApparentTemperature { get; set; }

        [JsonProperty("dewPoint")]
        public double DewPoint { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("windSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("windGust")]
        public double WindGust { get; set; }

        [JsonProperty("windBearing")]
        public long WindBearing { get; set; }

        [JsonProperty("cloudCover")]
        public double CloudCover { get; set; }

        [JsonProperty("uvIndex")]
        public long UvIndex { get; set; }

        [JsonProperty("visibility")]
        public double Visibility { get; set; }

        [JsonProperty("ozone")]
        public long Ozone { get; set; }
    }
}

