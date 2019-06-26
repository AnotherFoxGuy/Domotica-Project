using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Project_Green.Models;
using System.Net;
using System.Text.RegularExpressions;
using Microcharts;
using SkiaSharp;
using Project_Green.Views;

namespace Project_Green
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IPScanner ipsc;
        DatabaseManager pieter = new DatabaseManager();
        
        public MainPage()
        {
            InitializeComponent();
            ipsc = new IPScanner();
            NavigationPage.SetHasNavigationBar(this, true);
            GreenhouseList.ItemsSource = pieter.GetGreenhouses();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            GreenhouseList.ItemsSource = ipsc.GetGreenhouses();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new ChartPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GreenhouseList.ItemsSource = pieter.GetGreenhouses();

        }
    }
}
