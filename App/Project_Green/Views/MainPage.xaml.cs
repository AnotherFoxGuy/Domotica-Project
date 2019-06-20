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
        DatabaseManager databaseManager = new DatabaseManager();
        IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
        Regex regex = new Regex("^.+(?=\\.\\d+$)");
        //List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
        public MainPage()
        {
            InitializeComponent();
            GreenhouseList.ItemsSource = databaseManager.GetGreenhouses();
            IP_Address.Text = regex.Match(addresses[0].ToString()).ToString();
            //AddChartEntries(150, 300, 50);
            //Testchart.Chart = new BarChart() { Entries = entries };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            databaseManager.AddGreenhouse(12345, "Test123", "Images/GreenhouseDefault.png", "192.168.1.5");
            GreenhouseList.ItemsSource = databaseManager.GetGreenhouses();
        }

        //public void AddChartEntries(float test1, float test2, float test3)
        //{
        //    entries.Add(new Microcharts.Entry(test1) { Color = SKColor.Parse("#FF1493"), Label = "Test", ValueLabel = test1.ToString() });
        //    entries.Add(new Microcharts.Entry(test2) { Color = SKColor.Parse("#00BFFF"), Label = "Test", ValueLabel = test2.ToString() });
        //    entries.Add(new Microcharts.Entry(test3) { Color = SKColor.Parse("#00CED1"), Label = "Test", ValueLabel = test3.ToString() });
        //}

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ChartPage();
        }
    }
}
