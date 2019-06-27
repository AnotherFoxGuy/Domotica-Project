using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using Project_Green.Models;
using SkiaSharp;

namespace Project_Green
{
    class ChartData
    {
        public LineChart GetChartDataBy(string timeTable, int date, string sensor, int greenhouse_Id)
        {
            int min = 0;
            int max = 100;
            List<ChartEntry> entries = new List<ChartEntry>();
            List<Sensor> sensorData = DatabaseManager.Instance.GetSensorData(timeTable, date, sensor, greenhouse_Id);
            foreach (Sensor data in sensorData)
            {
                switch (sensor)
                {
                    case "Humidity":
                        entries.Add(new ChartEntry((float)data.Humidity)
                        {
                            Color = SKColor.Parse(GetColor((int)data.Humidity)),
                            Label = timeTable,
                            ValueLabel = sensor
                        });
                        
                        break;
                    case "Temperature":
                        entries.Add(new ChartEntry((float)data.Temperature)
                        {
                            Color = SKColor.Parse(GetColor((int)data.Temperature)),
                            Label = timeTable,
                            ValueLabel = sensor
                        });
                        min = -50;
                        max = 50;
                        break;
                    case "LightLevel":
                        entries.Add(new ChartEntry(data.LightLevel)
                        {
                            Color = SKColor.Parse(GetColor(data.LightLevel / 10)),
                            Label = timeTable,
                            ValueLabel = sensor
                        });
                        max = 1024;
                        break;
                    case "Moisture":
                        entries.Add(new ChartEntry((float)data.Moisture)
                        {
                            Color = SKColor.Parse(GetColor((int)data.Moisture / 10)),
                            Label = timeTable,
                            ValueLabel = sensor
                        });
                        break;
                    case "WaterLevel":
                        entries.Add(new ChartEntry(data.WaterLevel)
                        {
                            Color = SKColor.Parse(GetColor(data.WaterLevel / 10)),
                            Label = timeTable,
                            ValueLabel = sensor
                        });
                        max = 1024;
                        break;
                }
            }

            return new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                LabelTextSize = 50,
                PointMode = PointMode.Circle,
                PointSize = 25,
                MaxValue = max,
                MinValue = min
            };
        }

        public string GetColor(int value)
        {
            if (value / 10 > 10)
                value = 10;
            else
                value /= 10;

            List<string> color = new List<string> {
                "#ed401a",
                "#d85c41",
                "#5bdc54",
                "#ba7c7c",
                "#44c438",
                "#27e016",
                "#44c438",
                "#5bdc54",
                "#d17864",
                "#d85c41",
                "#ed401a"
            };
            return color[value];
        }
    }
}
