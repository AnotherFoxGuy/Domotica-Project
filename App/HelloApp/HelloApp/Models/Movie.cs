using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HelloApp.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement, Unique, NotNull]
        public int MovieID { get; set; }

        [Unique, NotNull]
        public string Title { get; set; }

        public string Description { get; set; }

        [NotNull]
        public int Year { get; set; }
    }
}
