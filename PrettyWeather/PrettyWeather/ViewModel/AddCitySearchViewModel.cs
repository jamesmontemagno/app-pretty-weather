using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PrettyWeather.Messaging;
using PrettyWeather.Model;
using Xamarin.Forms;

namespace PrettyWeather.ViewModel
{
    public class AddCitySearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        List<CityInfo> SearchableCities;

        public AddCitySearchViewModel()
        {
            SearchCitiesCommand = new Command(async () => await ExecuteSearchCitiesCommand());
            CitySelectedCommand = new Command(async () => await ExecuteCitySelectedCommand());

            CityResults = new ObservableCollection<CityInfo>();

            PopulateSearchableCities();
        }

        private void PopulateSearchableCities()
        {
            SearchableCities = new List<CityInfo>();

            SearchableCities.Add(new CityInfo { CityName = "Seattle", State = "WA", Latitude = 47.62396, Longitude = -122.31882 });
            SearchableCities.Add(new CityInfo { CityName = "Bellevue", State = "WA", Latitude = 47.61002, Longitude = -122.187 });
            SearchableCities.Add(new CityInfo { CityName = "Redmond", State = "WA", Latitude = 47.67858, Longitude = -122.13158 });
            SearchableCities.Add(new CityInfo { CityName = "Bellingham", State = "WA", Latitude = 48.75235, Longitude = -122.47122 });
            SearchableCities.Add(new CityInfo { CityName = "Vancouver", State = "BC", Latitude = 49.26038, Longitude = -123.11336 });
            SearchableCities.Add(new CityInfo { CityName = "Madison", State = "WI", Latitude = 43.07295, Longitude = -89.38669 });
            SearchableCities.Add(new CityInfo { CityName = "Green Bay", State = "WI", Latitude = 44.513, Longitude = -88.01001 });
            SearchableCities.Add(new CityInfo { CityName = "Miami", State = "FL", Latitude = 25.77481, Longitude = -80.19773 });
            SearchableCities.Add(new CityInfo { CityName = "Los Angeles", State = "CA", Latitude = 34.05349, Longitude = -118.24532 });
            SearchableCities.Add(new CityInfo { CityName = "Honolulu", State = "HI", Latitude = 21.30485, Longitude = -157.85776 });
            SearchableCities.Add(new CityInfo { CityName = "Denver", State = "CO", Latitude = 39.73715, Longitude = -104.989174 });
        }

        private async Task ExecuteCitySelectedCommand()
        {
            var message = new SearchCitySelectedMessage { SelectedCity = SelectedCity };

            MessagingCenter.Send(message, SearchCitySelectedMessage.Message);

            await Shell.Current.Navigation.PopModalAsync();
        }

        private async Task ExecuteSearchCitiesCommand()
        {
            IsRefreshing = true;

            await Task.Delay(250);

            if (CurrentSearchTerm.ToUpper() == "signout".ToUpper())
            {
                Microsoft.AppCenter.Auth.Auth.SignOut();
                IsRefreshing = false;
                await Shell.Current.Navigation.PopModalAsync();
                return;
            }

            var matchingCities = SearchableCities.FindAll(ci => ci.CityName.ToUpper().Contains(CurrentSearchTerm.ToUpper()));

            CityResults.Clear();

            foreach (var item in matchingCities)
            {
                CityResults.Add(item);
            }

            IsRefreshing = false;
        }

        string currentSearchTerm;
        public string CurrentSearchTerm
        {
            get => currentSearchTerm;
            set
            {
                currentSearchTerm = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSearchTerm)));
            }
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }

        CityInfo selectedCity;
        public CityInfo SelectedCity {
            get => selectedCity;
            set
            {
                if (selectedCity != value)
                {
                    selectedCity = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCity)));
                }
            }
        }

        public ObservableCollection<CityInfo> CityResults { get; set; }

        public ICommand SearchCitiesCommand { get; }
        public ICommand CitySelectedCommand { get; }
    }
}
