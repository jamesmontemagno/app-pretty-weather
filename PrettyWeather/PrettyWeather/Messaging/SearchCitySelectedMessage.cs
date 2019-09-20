using System;
using System.Collections.Generic;
using System.Text;
using PrettyWeather.Model;

namespace PrettyWeather.Messaging
{
    public class SearchCitySelectedMessage
    {
        public const string Message = "searchcityselectedmessage";

        public CityInfo SelectedCity { get; set; }
    }
}
