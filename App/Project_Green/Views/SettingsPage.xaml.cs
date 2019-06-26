using Project_Green.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Green.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        DatabaseManager databaseManager = new DatabaseManager();
        public SettingsPage(Greenhouse greenhouse)
        {    
            InitializeComponent();
            ImagePicker.ItemsSource = new List<string> { "GreenhouseDefault", "GreenHouse1", "GreenHouse2", "GreenHouse3", "GreenHouse4", "GreenHouse5" };
            Imagedisplay.Source = greenhouse.Greenhouse_Image;
            ImagePicker.SelectedItem = "GreenhouseDefault";
            //pak image van database van deze arduino 
        }

        private void Humidity_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            HumidityLable.Text = $"Auto Watering : { Convert.ToSingle(HumiditySlider.Value).ToString()}";
        }

        private void TempratureSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            TempratureLable.Text = $"Trigger fans on Temprature : {Convert.ToSingle(TempratureSlider.Value).ToString()} ";
        }

        private void Watering_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Fans_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void ImagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Imagedisplay.Source = $"/Images/{ImagePicker.SelectedItem}.png";
        }

        private void SaveSettings_Clicked(object sender, EventArgs e)
        {
            string x = $"/Images/{ImagePicker.SelectedItem}.png";
            databaseManager.UpdateGreenhouse(12, Namechanger.Text, x);
        }
        
    }
}