using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Project_Green.Models
{
    public class Greenhouse
    {
        [Unique, PrimaryKey, NotNull]
        public string ID { get; set; }

        [Unique]
        public string Name { get; set; }

        [Unique, NotNull]
        public string IP { get; set; }
    }
}
