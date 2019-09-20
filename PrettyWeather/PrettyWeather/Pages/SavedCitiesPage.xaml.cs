using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettyWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedCitiesPage : ContentPage
    {
        public SavedCitiesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as SavedCitiesViewModel;

            vm?.GetSavedCities();
        }
    }
}