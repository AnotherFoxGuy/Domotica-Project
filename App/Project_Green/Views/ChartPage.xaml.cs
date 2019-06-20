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
     
        List<ChartEntry> entriesH = new List<ChartEntry>();
        List<ChartEntry> entriesT = new List<ChartEntry>();
        List<ChartEntry> entriesS = new List<ChartEntry>();
        StatMeister pieter = new StatMeister();
        public ChartPage()
        {
            
            InitializeComponent();
            List<string> dates = new List<string> { "Week", "Month", "Year" };
            DateSelecter.ItemsSource = dates;
            FillHumidityChart();
            FillTempratureChart();
            FillSoilMoisterChart();

        }
       
        private void FillHumidityChart()
        {

            HumidityChart.Chart = new LineChart()
            {
                Entries = entriesH,
                LineMode = LineMode.Straight,
                LineSize = 8,
                LabelTextSize = 50,
                PointMode = PointMode.Circle,
                PointSize = 25,
                MaxValue = 100,
                MinValue = 0
            };

            //maak methode aan om data vissen uit databas
            List<int> rows = new List<int>();
            foreach(var row in rows)
            {
                int x = 20;
                int i = pieter.Limitcheck(100  ,0, x);
                string color = pieter.ColorRep(x, 100, 0);
                entriesH.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = "maandag", ValueLabel = x.ToString() });
            }
            //max 100%
            //min 0%
        }
        private void FillTempratureChart()
        {
            TempratureChart.Chart = new LineChart()
            {
                Entries = entriesT,
                LineMode = LineMode.Straight,
                LineSize = 8,
                LabelTextSize = 50,
                PointMode = PointMode.Circle,
                PointSize = 25,
                MaxValue = 50,
                MinValue = -50
            };
            int x = 20;
            int i = pieter.Limitcheck(50, -50, x);
            string color = pieter.ColorRep(x, 50, -50);
            entriesT.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = "maandag", ValueLabel = x.ToString() });

            //50 *C
            //-50 *C
        }
        private void FillSoilMoisterChart()
        {
            MoisterChart.Chart = new LineChart()
            {
                Entries = entriesS,
                LineMode = LineMode.Straight,
                LineSize = 8,
                LabelTextSize = 50,
                PointMode = PointMode.Circle,
                PointSize = 25,
                MaxValue = 50,
                MinValue = -50
            };
            int x = 20;
            int i = pieter.Limitcheck(200, 0, x);
            string color = pieter.ColorRep(x, 200, 0);   
            entriesS.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = "maandag", ValueLabel = x.ToString() });
            //200
            //0
        }
        private int EntryCount()
        {
            int AmountOfTimes =0;
            string date = DateSelecter.SelectedItem.ToString();
            Dictionary<string, int> TimeDates = new Dictionary<string, int>();
            TimeDates.Add("Week", 7);
            TimeDates.Add("Month", 12);
            TimeDates.Add("Year", 24);

            switch (date)
            {
                case "Week":
                    AmountOfTimes = TimeDates["Week"];
                    break;
                case "Month":
                    AmountOfTimes = TimeDates["Month"];
                    break;
                case "Year":
                    AmountOfTimes = TimeDates["Year"];
                    break;
                default:
                    AmountOfTimes = TimeDates["Week"];
                    break;
            }
            return AmountOfTimes;
        }
       
    }
}