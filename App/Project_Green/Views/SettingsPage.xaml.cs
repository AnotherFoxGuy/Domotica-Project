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
        public SettingsPage()
        {    
            InitializeComponent();
            List<string> Imagesindexes = new List<string> { "GreenhouseDefault", "GreenHouse1", "GreenHouse2", "GreenHouse3", "GreenHouse4" , "GreenHouse5" };
            ImagePicker.ItemsSource = Imagesindexes;
            ImagePicker.SelectedItem = "Default";
            Imagedisplay.Source = "/Images/GreenhouseDefault.png";
            //pak image van database van deze arduino 
        }

        private void GotoGraphsPage(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ChartPage();
          
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
    }
}