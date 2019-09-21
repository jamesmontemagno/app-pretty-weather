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
        readonly string iOSAppCenter = "6d9ef07b-2066-4dc1-ad60-972e53896c42";
        readonly string androidAppCenter = "c61a8375-ba75-4b0a-85ae-f1cb6b2b4af3";

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
