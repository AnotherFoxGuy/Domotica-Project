using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Green.Models
{
    public class SensorYaml
    {
        public string time { get; set; }
        public float temperature { get; set; }
        public float humidity { get; set; }
        public float lightlevel { get; set; }
        public int moisture { get; set; }
        public int waterlevel { get; set; }
    }
}
