using ArdunoRest;
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
        Greenhouse greenhouse;
        ArdunoRestClient rest = new ArdunoRestClient { };
        public SettingsPage(Greenhouse greenhouse)
        {    
            InitializeComponent();
            this.greenhouse = greenhouse;
            UseSettings();
           
            
            //pak image van database van deze arduino 
        }
        public void UseSettings()
        {
            ImagePicker.ItemsSource = new List<string> { "GreenHouse1", "GreenHouse2", "GreenHouse3", "GreenHouse4", "GreenHouse5", "GreenhouseDefault" };
            Imagedisplay.Source = greenhouse.Greenhouse_Image;
            Namechanger.Text = greenhouse.Greenhouse_Name;
            //TempratureSlider = greenhouse.Greenhouse_TempSlider; heb data base voor nodig 
            //HumiditySlider = greenhouse.Greenhouse_HumiSlider;
            Fixbar();
        }
        
        private void TempratureSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            TempratureLable.Text = $"Trigger fans on Temprature : {Convert.ToSingle(TempratureSlider.Value).ToString()} ";
        }

        private void ImagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Imagedisplay.Source = $"/Images/{ImagePicker.SelectedItem}.png";
        }

        private void SaveSettings_Clicked(object sender, EventArgs e)
        {
            string x = $"/Images/{ImagePicker.SelectedItem}.png";
            float TempSliderValue = Convert.ToSingle(TempratureSlider.Value);
            float SoilmoisterValue = Convert.ToSingle(SoilmoisterSlider.Value);
            // DatabaseManager.Instance.UpdateGreenhouse(greenhouse.Greenhouse_ID, Namechanger.Text, x , TempSliderValue , HumiSliderValue);
            DatabaseManager.Instance.UpdateGreenhouse(greenhouse.Greenhouse_ID, Namechanger.Text, x);
        }
        private void Fixbar()
        {
            for (int i = 0; i < ImagePicker.ItemsSource.Count; i++)
            {
                if (greenhouse.Greenhouse_Image.ToString().Contains(i.ToString()))
                {
                    ImagePicker.SelectedItem = ImagePicker.ItemsSource[i - 1];
                    break;
                }
                if (i == ImagePicker.ItemsSource.Count)
                {
                    ImagePicker.SelectedItem = "GreenhouseDefault";
                }
            }
        }

        private void FanToggle_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Watering_Clicked(object sender, EventArgs e)
        {
            rest.DigitalGet(5, 1);
            TimeSpan.FromMilliseconds(1000);
            rest.DigitalGet(5, 0);
        }

        private void SoilmoisterSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            string x = $"/Images/{ImagePicker.SelectedItem}.png";
            //DatabaseManager.Instance.UpdateGreenhouse(12, Namechanger.Text, x);
        }
    }
}