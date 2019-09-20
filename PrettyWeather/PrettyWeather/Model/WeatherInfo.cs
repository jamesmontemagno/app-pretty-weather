using System;
using System.Collections.Generic;
using System.Text;

namespace PrettyWeather.Model
{
    public class WeatherInfo
    {
        public string Icon { get; set; }
        public string Summary { get; set; }
        public int Temperature { get; set; }
        public int Pressure { get; set; }
        public int UvIndex { get; set; }
        public int WindSpeed { get; set; }
        public int WindBearing { get; set; }
        public int Humidity { get; set; }
        public int PrecipProbability { get; set; }
    }
}
