using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Project_Green.Models;

namespace Project_Green
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DatabaseManager databaseManager = new DatabaseManager();
        public MainPage()
        {
            InitializeComponent();
            GreenhouseList.ItemsSource = databaseManager.GetGreenhouses();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            databaseManager.AddGreenhouse(12345, "Test123", "Images/GreenhouseDefault.png", "192.168.1.5");
            GreenhouseList.ItemsSource = databaseManager.GetGreenhouses();
        }
    }
}
