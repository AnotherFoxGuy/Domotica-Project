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
        readonly SQLiteConnection Connection = DependencyService.Get<IDBInterface>().CreateConnection();
        public DatabaseManager()
        {
        }

        public List<Greenhouse> GetGreenhouses()
        {
            return Connection.Query<Greenhouse>("SELECT * FROM Greenhouse");
        }

        public List<Sensor> GetSensorData(string date, int greenhouse_id)
        {
            return Connection.Query<Sensor>($"SELECT AVG(Temperature, Humidity, WaterLevel, LightLevel, Moisture), Date FROM Sensor WHERE Data = {date} AND Greenhouse_ID = {greenhouse_id} GROUP BY Date");
        }

        public void AddGreenhouse(int ID, string Name, string Image)
        {
            Connection.Insert(new Greenhouse { Greenhouse_ID = ID, Greenhouse_Name = Name, Greenhouse_Image = Image });
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
    }
}
