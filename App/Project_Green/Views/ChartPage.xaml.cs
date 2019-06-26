using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Green.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Green.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        StatMeister pieter = new StatMeister();
        Greenhouse selectedGreenhouse;
        public ChartPage(Greenhouse greenhouse)
        {
            InitializeComponent();
            selectedGreenhouse = greenhouse;
            DateSelecter.ItemsSource = new List<string> { "Week", "Month", "Year" };
            fillCharts();
        }

        public void fillCharts()
        {
            int dateselecter = pieter.EntryCount((string)DateSelecter.SelectedItem);
            HumidityChart.Chart = pieter.MakeLineChart(dateselecter);
            TempratureChart.Chart = pieter.FillTempratureChart(dateselecter);
            MoisterChart.Chart = pieter.FillSoilMoisterChart(dateselecter);
        }
       
        private void DateChanged(object sender, EventArgs e)
        {
            fillCharts();
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(selectedGreenhouse));
        }

        private async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}