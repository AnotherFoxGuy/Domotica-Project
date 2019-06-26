using System;
using System.Collections.Generic;
using System.Text;
using Project_Green.Models;
using SQLite;

namespace Project_Green.Models
{
    public class Sensor
    {
        [Unique, PrimaryKey, NotNull]
        public int Greenhouse_ID { get; set; }

        [Unique]
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Moisture { get; set; }
        public int LightLevel { get; set; }
        public int WaterLevel { get; set; }
    }
}
