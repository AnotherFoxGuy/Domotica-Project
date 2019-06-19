﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Project_Green.Models
{
    public class Greenhouse
    {
        [Unique, PrimaryKey, NotNull]
        public int Greenhouse_ID { get; set; }

        [Unique]
        public string Greenhouse_Name { get; set; }

        [NotNull]
        public string Greenhouse_Image { get; set; }

        [Unique, NotNull]
        public string Greenhouse_IP { get; set; }
    }
}
