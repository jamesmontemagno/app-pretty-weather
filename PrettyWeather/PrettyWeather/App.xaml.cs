using Microsoft.AppCenter;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Data;
using PrettyWeather.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather
{
    public partial class App : Application
    {
        readonly string iOSAppCenter = "";
        readonly string androidAppCenter = "";

        public App()
        {
            InitializeComponent();

            MainPage = new AppShellPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start($"ios={iOSAppCenter};android={androidAppCenter};",
                typeof(Auth), typeof(Data));

            Routing.RegisterRoute("saved-cities", typeof(SavedCitiesPage));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
