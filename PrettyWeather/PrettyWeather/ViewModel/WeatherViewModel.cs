using System;
using System.Collections.Generic;
using System.Text;

namespace PrettyWeather.ViewModel
{
    public class WeatherViewModel
    {
        public int Temp { get; set;  }
        public WeatherViewModel()
        {
        }
        public WeatherViewModel(int temp)
        {
            Temp = temp;
        }
    }
}
