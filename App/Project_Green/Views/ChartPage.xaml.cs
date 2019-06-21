using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Green.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        
        StatMeister pieter = new StatMeister();
        public ChartPage()
        {

            InitializeComponent();
            List<string> dates = new List<string> { "Week", "Month", "Year" };
            DateSelecter.ItemsSource = dates;
            fillCharts();
        }
        public void fillCharts()
        {
            int dateselecter = pieter.EntryCount(Convert.ToString(DateSelecter.SelectedItem));
            HumidityChart.Chart = pieter.MakeLineChart(dateselecter);
            TempratureChart.Chart = pieter.FillTempratureChart(dateselecter);
            MoisterChart.Chart = pieter.FillSoilMoisterChart(dateselecter);
        }
       
        private void DateChanged(object sender, EventArgs e)
        {
            fillCharts();
        }
    }
}