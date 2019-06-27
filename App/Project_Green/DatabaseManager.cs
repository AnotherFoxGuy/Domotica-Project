using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Project_Green.Models;

namespace Project_Green
{
    public class DatabaseManager
    {
        readonly SQLiteConnection Connection;

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
                    querystring = $"SELECT {sensor} FROM Sensor WHERE Date = {date} AND Greenhouse_ID = {greenhouse_Id}";
                    break;
                case "Week": // date = week nummer
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Data = {date} AND Greenhouse_ID = {greenhouse_Id} GROUP BY Date";
                    break;
                case "Month": // date = maandnummer
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Data = {date} AND Greenhouse_ID = {greenhouse_Id} GROUP BY Date";
                    break;
                case "Year": // date = jaargetal
                    querystring = $"SELECT AVG({sensor}), Date FROM Sensor WHERE Data = {date} AND Greenhouse_ID = {greenhouse_Id} GROUP BY Date";
                    break;
            }
            //return Connection.Query<Sensor>(querystring);
            throw new NotImplementedException();
        }


        //check of onderstaande code nog relevant is!!
        public void AddGreenhouse(int ID, string Name, string Image , string IP )
        {
            Connection.Insert(new Greenhouse { Greenhouse_ID = ID, Greenhouse_Name = Name, Greenhouse_Image = Image , Greenhouse_IP = IP});
        }
        public void DeleteGreenhouse(int ID)
        {
            Connection.Execute($"DELETE FROM Greenhouse WHERE Greenhouse_ID = {ID} ");
        }
        public void UpdateGreenhouse(int ID , string Name , string Image )
        {
            Connection.Query<Greenhouse>($"UPDATE Greenhouse SET Greenhouse_Name = \"{Name}\" , Greenhouse_Image = \"{Image}\" WHERE Greenhouse_ID = \"{ID}\"");
        }

        // methode voor data per uur *GetPerHour
        // methode pak data per dag (met de methode GetPerHour) * PerDay
        // methode pak per week (met de methode Perday * 7) 
        // methode pak per maand


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
