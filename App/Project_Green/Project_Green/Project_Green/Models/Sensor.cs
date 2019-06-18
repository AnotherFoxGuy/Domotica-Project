using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Green.Models
{
    public class Sensor
    {
        public int ID { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Moisture { get; set; }
        public int LightLevel { get; set; }
        public bool WaterLevel { get; set; }
    }
}
