using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartemProject
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("Insert SyncFusion License Here (removed to prevent stealing lol)");
            DependencyService.Register<ApiAdapter>();

            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());
            //(MainPage as NavigationPage).BarTextColor = Color.White;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
