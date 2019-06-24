using System;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;

namespace Project_Green
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //var test = new ArdunoRest.ArdunoRestClient();
            //test.BaseUrl = "192992553";


            //var res = test.Id();
            //var x = res.Name;
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
