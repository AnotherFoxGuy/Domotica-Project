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
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            GreenhouseList.ItemsSource = IPScanner.Instance.GetGreenhouses();
        }

        private void ScanGreenhouses(object sender, EventArgs e)
        {
            GreenhouseList.ItemsSource = IPScanner.Instance.GetGreenhouses();
        }

        private void GreenhouseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SelectedGreenhouse = e.Item as Greenhouse;
            var cp = new ChartPage(SelectedGreenhouse);
            Navigation.PushAsync(cp);
        }
    }
}
