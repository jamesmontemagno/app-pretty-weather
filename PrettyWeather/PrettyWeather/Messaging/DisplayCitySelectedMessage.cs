using System;
using System.Collections.Generic;
using System.Text;
using PrettyWeather.Model;

namespace PrettyWeather.Messaging
{
    public class DisplayCitySelectedMessage
    {
        public const string Message = "displaycityselectedmessage";

        public CityInfo SelectedCity { get; set; }
    }
}
