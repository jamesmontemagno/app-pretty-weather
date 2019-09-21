using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Auth;
using PrettyWeather.Messaging;
using PrettyWeather.Pages;
using Xamarin.Forms;

using PrettyWeather.Services;
using PrettyWeather.Model;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrettyWeather.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        readonly string latitudePrefKey = "savedlatitude";
        readonly string longitudePrefKey = "savedlongitude";
        readonly string cityNamePrefKey = "savedCityName";
        readonly string stateNamePrefKey = "savedStateName";

        bool initialized = false;

        #region Properties

        int temp;
        public int Temp {
            get => temp;
            set => SetProperty(ref temp, value);
        }

        string summary;
        public string Summary
        {
            get => summary;
            set => SetProperty(ref summary, value);
        }

        string icon;
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        public ObservableCollection<DataItem> WeatherProperties { get; set; }

        double pressure;
        public double Pressure
        {
            get => pressure;
            set => SetProperty(ref pressure, value);
        }

        long uvIndex;
        public long UvIndex { get=> uvIndex; set => SetProperty(ref uvIndex, value); }

        double windSpeed;
        public double Windspeed { get=> windSpeed; set=>SetProperty(ref windSpeed, value); }

        long windBearing;
        public long WindBearing { get => windBearing; set => SetProperty(ref windBearing, value); }

        double humidity;
        public double Humidity { get => humidity; set => SetProperty(ref humidity, value); }

        long precipProbability;
        public long PrecipProbability { get => precipProbability; set => SetProperty(ref precipProbability, value); }

        string cityName;
        public string CityName { get => cityName; set => SetProperty(ref cityName, value); }

        string currentDate;
        public string CurrentDate { get => currentDate; set => SetProperty(ref currentDate, value); }

        #endregion

        public WeatherViewModel()
        {
            ShowSavedCitiesCommand = new Command(async () => await ExecuteShowSavedCitiesCommand());
            WeatherProperties = new ObservableCollection<DataItem>()
            {
                new DataItem { Name = "UV Index", Value = string.Empty },
                new DataItem { Name = "Wind Speed", Value = string.Empty },
                new DataItem { Name = "Wind Direction", Value = string.Empty },
                new DataItem { Name = "Humidity", Value = string.Empty },
                new DataItem { Name = "Precip Probability", Value = string.Empty },
                new DataItem { Name = "Pressure", Value = string.Empty }
            };

            // subscribe to any new selected city changed messages
            MessagingCenter.Subscribe<DisplayCitySelectedMessage>(this, DisplayCitySelectedMessage.Message,
                async (msg) =>
                {
                    await LoadWeatherInfo(msg.SelectedCity); 
                });
        }

        public async Task InitializeWeather()
        {
            if (!initialized)
            {
                var latitude = Preferences.Get(latitudePrefKey, 47.60357);
                var longitude = Preferences.Get(longitudePrefKey, -122.32945);
                var cityName = Preferences.Get(cityNamePrefKey, "Seattle");
                var stateName = Preferences.Get(stateNamePrefKey, "WA");

                var cityInfo = new CityInfo { CityName = cityName, State = stateName, Latitude = latitude, Longitude = longitude };
                await LoadWeatherInfo(cityInfo);

                initialized = true;
            }

        }

        public WeatherViewModel(int temp) : this()
        {
            //Temp = temp;
        }

        async Task LoadWeatherInfo(CityInfo cityInfo)
        {
            var weatherService = new DataService();

            var weatherInfo = await weatherService.GetWeatherInfo(cityInfo.Latitude, cityInfo.Longitude);

            if (weatherInfo != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    Temp = weatherInfo.Temperature;
                    Summary = weatherInfo.Summary.ToUpper();
                    Icon = weatherInfo.Icon;

                    CurrentDate = $"{NumberToMonth(DateTime.Now.Month).ToUpper()} {DateTime.Now.Day}, {DateTime.Now.Year}";

                    WeatherProperties.First(di => di.Name == "Pressure").Value = $"{weatherInfo.Pressure} mb";
                    WeatherProperties.First(di => di.Name == "UV Index").Value = weatherInfo.UvIndex.ToString();
                    WeatherProperties.First(di => di.Name == "Wind Speed").Value = $"{weatherInfo.WindSpeed} mph";
                    WeatherProperties.First(di => di.Name == "Wind Direction").Value = WindBearingToCardinalDirection(weatherInfo.WindBearing);
                    WeatherProperties.First(di => di.Name == "Humidity").Value = $"{weatherInfo.Humidity}%";
                    WeatherProperties.First(di => di.Name == "Precip Probability").Value = $"{weatherInfo.PrecipProbability}%";

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeatherProperties)));

                    CityName = cityInfo.CityName.ToUpper();

                // save city info back to preferences
                Preferences.Set(latitudePrefKey, cityInfo.Latitude);
                    Preferences.Set(longitudePrefKey, cityInfo.Longitude);
                    Preferences.Set(cityNamePrefKey, cityInfo.CityName);
                    Preferences.Set(stateNamePrefKey, cityInfo.State);
                });
            }
        }

        string WindBearingToCardinalDirection(int degrees)
        {
            if (degrees < 23)
                return "N";

            if (degrees >= 23 && degrees < 68)
                return "NE";

            if (degrees >= 68 && degrees < 113)
                return "E";

            if (degrees >= 113 && degrees < 158)
                return "SE";

            if (degrees >= 158 && degrees < 203)
                return "S";

            if (degrees >= 203 && degrees < 248)
                return "SW";

            if (degrees >= 248 && degrees < 293)
                return "W";

            if (degrees >= 293 && degrees < 337)
                return "NW";

            return "N";
        }
        
        string NumberToMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                default:
                    return "December";
            }
        }
        public ICommand ShowSavedCitiesCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        async Task ExecuteShowSavedCitiesCommand()
        {
            try
            {
                // first make sure we're logged in
                var userInfo = await Auth.SignInAsync();

                var citiesPage = new SavedCitiesPage();

                await Shell.Current.Navigation.PushModalAsync(citiesPage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    
        void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string memberName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

    }
}
