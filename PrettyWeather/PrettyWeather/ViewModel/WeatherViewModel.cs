using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrettyWeather.ViewModel
{
    public class WeatherViewModel : BindableObject
    {
        int temp = 0;
        public int Temp
        {
            get => temp;
            set
            {
                temp = value;
                OnPropertyChanged();
            }
        }

        public WeatherViewModel()
        {
        }
        public WeatherViewModel(int temp)
        {
            Temp = temp;
        }
    }
}
