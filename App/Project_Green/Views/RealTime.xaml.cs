using ArdunoRest;
using Project_Green.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Green.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RealTime : ContentPage
    {  
        Greenhouse greenhouse;
        ArdunoRestClient rest = new ArdunoRestClient {};
        /// <summary>
        /// RealTime Constructor
        /// </summary>
        /// <param name="greenhouse"></param>
        public RealTime(Greenhouse greenhouse)
        {
            InitializeComponent();
            this.greenhouse = greenhouse;
            SetTimer();

        }
        /// <summary>
        /// sets a timer for every 10 seconds get the data from the arduino
        /// </summary>
        private void SetTimer()
        {
            Test.Text = "Timer Started";
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                try
                {

                    FillData();
                    Test.Text = "Verbonden met Arduino";
                    return true; // return true to repeat counting, false to stop timer
                }
                catch (Exception e)
                {
                    Test.Text = e.Message;
                    return false;
                }
            });
        }
        /// <summary>
        /// fill the data
        /// </summary>
        public void FillData()
        {
            LiveTemprature.Text = $"Temprature : {rest.Temperature().Return_value.ToString()}";
            LiveHumidity.Text = $"Humidity : {rest.Humidity().Return_value.ToString()} ";
            LiveLightIntensity.Text = $"Light intesety : {rest.Analog(1).Return_value.ToString()} " ;
            LiveWaterLevel.Text = $"Water Level : {rest.Analog(2).Return_value.ToString()}";
            LiveSoilMoister.Text = $"Soil Moister : {rest.Analog(3).Return_value.ToString()}";
        }
    }
  
}