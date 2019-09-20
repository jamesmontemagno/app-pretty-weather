using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrettyWeather.Messaging;
using PrettyWeather.Model;
using PrettyWeather.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter.Data;
using System.Linq;

namespace PrettyWeather.ViewModel
{
    public class SavedCitiesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool initialized = false;

        public ObservableCollection<CityInfo> SavedCities { get; set; }

        public SavedCitiesViewModel()
        {
            AddCityCommand = new Command(async () => await ExecuteAddCityCommand());
            SelectCityCommand = new Command(async () => await ExecuteSelectCityCommand());

            SavedCities = new ObservableCollection<CityInfo>();

            MessagingCenter.Subscribe<SearchCitySelectedMessage>(this, SearchCitySelectedMessage.Message,
                (msg) =>
                {
                    SavedCities.Add(msg.SelectedCity);

                    // Add the city to Cosmos
                    Data.CreateAsync<CityInfo>($"{msg.SelectedCity.CityName}-{msg.SelectedCity.State}", msg.SelectedCity, DefaultPartitions.UserDocuments);
                });
        }

        public async Task GetSavedCities()
        {
            if (!initialized)
            {
                var savedCitiesFromCosmos = await Data.ListAsync<CityInfo>(DefaultPartitions.UserDocuments);

                foreach (var city in savedCitiesFromCosmos.CurrentPage.Items)
                {
                    SavedCities.Add(city.DeserializedValue);
                }

                initialized = true;
            }
        }

        private async Task ExecuteAddCityCommand()
        {
            await Shell.Current.Navigation.PushModalAsync(new AddCitySearchPage());
        }

        private async Task ExecuteSelectCityCommand()
        {
            var msg = new DisplayCitySelectedMessage { SelectedCity = SelectedCity };

            MessagingCenter.Send(msg, DisplayCitySelectedMessage.Message);

            await Shell.Current.Navigation.PopModalAsync();
        }


        public ICommand AddCityCommand { get; }
        public ICommand SelectCityCommand { get; }

        CityInfo selectedCity;
        public CityInfo SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCity)));
            }
        }


    }
}
