using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Green
{
    class StatMeister
    {
        int AmountOfTimes = 0;
        
       
        public int Limitcheck(int max, int min, int value)
        {
            if (value < min) { value = min; }
            if (value > max) { value = max; }
            return value;
        }
        public string ColorRep(int value, int max , int min)
        {
            string color = "";
            long convertedvalue = convert(value,max,min);
            
            Dictionary<long, string> TimeDates = new Dictionary<long, string>();
            TimeDates.Add(0, "#FF1493");
            TimeDates.Add(1, "#FF1493");
            TimeDates.Add(2, "#FF1493");
            TimeDates.Add(3, "#FF1493");
            TimeDates.Add(4, "#FF1493");
            TimeDates.Add(5, "#FF1493");
            TimeDates.Add(6, "#FF1493");
            TimeDates.Add(7, "#FF1493");
            TimeDates.Add(8, "#FF1493");
            TimeDates.Add(9, "#FF1493");
            TimeDates.Add(10, "#FF1493");
            switch (convertedvalue)
            {
                case 0:
                    color = TimeDates[convertedvalue];
                    break;
                case 1:
                    color = TimeDates[convertedvalue];
                    break;
                case 2:
                    color = TimeDates[convertedvalue];
                    break;
                case 3:
                    color = TimeDates[convertedvalue];
                    break;
                case 4:
                    color = TimeDates[convertedvalue];
                    break;
                case 5:
                    color = TimeDates[convertedvalue];
                    break;
                case 6:
                    color = TimeDates[convertedvalue];
                    break;
                case 7:
                    color = TimeDates[convertedvalue];
                    break;
                case 8:
                    color = TimeDates[convertedvalue];
                    break;
                case 9:
                    color = TimeDates[convertedvalue];
                    break;
                case 10:
                    color = TimeDates[convertedvalue];
                    break;

            }

            return color;
        }

        public long convert (int value,int max ,int min)
        {
            return (value - min) * (10 - 0) / (max - min) + 0;
        }
        public int EntryCount(string dateselected)
        {
            Dictionary<string, int> TimeDates = new Dictionary<string, int>();
            TimeDates.Add("Week", 7);
            TimeDates.Add("Month", 12);
            TimeDates.Add("Year", 24);

            switch (dateselected)
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

        public LineChart MakeLineChart(int dateselecter)
        {
            LineChart chart;
            List<ChartEntry> entriesH = new List<ChartEntry>();
            //dummydata
            List<int> Week = new List<int> { 10, 20, 33, 40, 50, 60, 70, 8 };
            List<int> Month = new List<int> { 10, 20, 33, 40, 50, 60, 70, 8, 10, 20, 33, 40 };
            List<int> Year = new List<int> { 10, 20, 33, 40, 50, 60, 70, 8, 10, 20, 33, 40, 50, 60 , 10, 20, 33, 40, 50, 60, 70, 8, 10, 20};
            List<int> dates = new List<int>();
            switch(dateselecter)
            {
                case 7:
                    dates = Week;
                    break;
                case 12:
                    dates = Month;
                    break;
                case 24:
                    dates = Year;
                    break;

            }
      
            foreach (int value in dates)
            {
                int i = Limitcheck(100, 0, value);
                string color = ColorRep(value, 100, 0);
                entriesH.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = dateselecter.ToString(), ValueLabel = value.ToString() });
            }
            chart = new LineChart()
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
            //max 100%
            //min 0%
            return chart;
        }
        public LineChart FillTempratureChart(int dateselecter)
        {
            LineChart chart;
            List<ChartEntry> entriesT = new List<ChartEntry>();
            List<int> Week = new List<int> { 10, 20, 33, 40, -20, 9, -10 };
            List<int> Month = new List<int> { 10, 20, 33, 40, -20, 9, -10, 10, 20, 33, 40, -20 };
            List<int> Year = new List<int> { 10, 20, 33, 40, -20, 9, -10, 10, 20, 33, 40, -20, 10, 20, 33, 40, -20, 9, -10, 10, 20, 33, 40, -20, };
            List<int> dates = new List<int>();
            switch (dateselecter)
            {
                case 7:
                    dates = Week;
                    break;
                case 12:
                    dates = Month;
                    break;
                case 24:
                    dates = Year;
                    break;

            }
           
            foreach (var value in dates)
            {

                int i = Limitcheck(50, -50, value);
                string color = ColorRep(value, 50, -50);
                entriesT.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = "maandag", ValueLabel = value.ToString() });
            }
            chart = new LineChart()
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
            
            return chart;
        }
        public LineChart FillSoilMoisterChart(int dateselecter)
        {
            LineChart chart;
            List<ChartEntry> entriesS = new List<ChartEntry>();
            List<int> Week = new List<int> { 20, 33, 40, 50, 60, 70, 8 };
            List<int> Month = new List<int> { 20, 33, 40, 50, 60, 70, 8, 20, 33, 40, 50, 60 };
            List<int> Year = new List<int> { 20, 33, 40, 50, 60, 70, 8, 20, 33, 40, 50, 60 , 20, 33, 40, 50, 60, 70, 8, 20, 33, 40, 50, 60 };
            List<int> dates = new List<int>();

            switch (dateselecter)
            {
                case 7:
                    dates = Week;
                    break;
                case 12:
                    dates = Month;
                    break;
                case 24:
                    dates = Year;
                    break;

            }
            foreach (var value in dates)
            {

                int i = Limitcheck(200, 0, value);
                string color = ColorRep(value, 200, 0);
                entriesS.Add(new ChartEntry(i) { Color = SKColor.Parse(color), Label = "maandag", ValueLabel = value.ToString() });
            }
            chart = new LineChart()
            {
                Entries = entriesS,
                LineMode = LineMode.Straight,
                LineSize = 8,
                LabelTextSize = 50,
                PointMode = PointMode.Circle,
                PointSize = 25,
                MaxValue = 200,
                MinValue = 0,
            };
            //200
            //0
            return chart;
        }

    }
}
