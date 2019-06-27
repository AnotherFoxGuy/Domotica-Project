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
        int i;
        ArdunoRestClient rest = new ArdunoRestClient {};
        Greenhouse greenhouse;
        public RealTime(Greenhouse greenhouse)
        {
            InitializeComponent();
            this.greenhouse = greenhouse;
            SetTimer();

        }
        private void SetTimer()
        {
            Test.Text = "Timer Started";
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                try
                {
                    i++;
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