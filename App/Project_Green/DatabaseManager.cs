using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Project_Green.Models;
using System.Globalization;

namespace Project_Green
{
    public class DatabaseManager
    {
        readonly SQLiteConnection Connection;
        public string CurrentIP;
        

        public List<Greenhouse> GetGreenhouses()
        {
            return Connection.Query<Greenhouse>("SELECT * FROM Greenhouse");
        }

        public List<Sensor> GetAvgSensorData(int date, string sensor, int greenhouse_Id)
        {
            return Connection.Query<Sensor>($"SELECT AVG({sensor}), Date FROM Sensor WHERE Date = {date} AND Greenhouse_ID = {greenhouse_Id} GROUP BY Date");
        }

        public List<Sensor> GetSensorData(string timeTable, int date, string sensor, int greenhouse_Id)
        {
            string querystring;
            switch (timeTable)
            {
                case "Day": // date = datum(2862019)
                    querystring = $"SELECT {sensor} FROM Sensor WHERE Date = {date} AND Greenhouse_ID = \"{greenhouse_Id}\"";
                    break;
                case "Week": // date = week nummer
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Date BETWEEN {FirstDateOfWeek(2019, date)} AND {FirstDateOfWeek(2019, date + 1)} AND Greenhouse_ID = \"{greenhouse_Id}\" GROUP BY Date";
                    break;
                case "Month": // date = maandnummer
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Date BETWEEN 01{date}2019 AND 31{date}2019 AND Greenhouse_ID = \"{greenhouse_Id}\" GROUP BY Date";
                    break;
                case "Year": // date = jaargetal
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Date LIKE 01%{date} AND Date LIKE 15%{date} AND Greenhouse_ID = \"{greenhouse_Id}\" GROUP BY Date";
                    break;
                default:
                    querystring = $"SELECT Temperature FROM Sensor WHERE Greenhouse_ID = 0";
                    break;
            }
            return Connection.Query<Sensor>(querystring);
        }

        public static int FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return Convert.ToInt32(result.AddDays(-3).ToShortDateString().Replace("-", string.Empty));
        }
        #region Singleton

        private static readonly Lazy<DatabaseManager> LazyDatabaseManager =
            new Lazy<DatabaseManager>(() => new DatabaseManager());

        public static DatabaseManager Instance => LazyDatabaseManager.Value;

        private DatabaseManager()
        {
            Connection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        #endregion
    }
}
