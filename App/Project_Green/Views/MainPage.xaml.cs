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
        /// <summary>
        /// Constructor of MainPage
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            GreenhouseList.ItemsSource = IPScanner.Instance.GetGreenhouses();
            GreenhouseList.ItemsSource = DatabaseManager.Instance.GetGreenhouses(); // haal deze weg in uit in eind product
        }
        /// <summary>
        /// Scans for greenhouses 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanGreenhouses(object sender, EventArgs e)
        {
            GreenhouseList.ItemsSource = IPScanner.Instance.GetGreenhouses();
        }
        /// <summary>
        /// When Greenhouse is selected Go to settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GreenhouseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SelectedGreenhouse = e.Item as Greenhouse;
            var cp = new ChartPage(SelectedGreenhouse);
            Navigation.PushAsync(cp);
        }
        /// <summary>
        /// When you come back to the page scan for green houses 
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GreenhouseList.ItemsSource = IPScanner.Instance.GetGreenhouses();
        }
    }
}
