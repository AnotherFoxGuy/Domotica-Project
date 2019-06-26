using System;
using System.ComponentModel;
using Xamarin.Forms;
using Project_Green.Models;
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

        private void GreenhouseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SelectedGreenhouse = e.Item as Greenhouse;
            var cp = new ChartPage(SelectedGreenhouse);
            Navigation.PushAsync(cp);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GreenhouseList.ItemsSource = pieter.GetGreenhouses();
        }
    }
}
