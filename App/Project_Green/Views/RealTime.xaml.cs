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
            Device.StartTimer(TimeSpan.FromSeconds(6), () =>
            {
                try
                {
                    i++;
                    FillData();
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
            LiveTemprature.Text = rest.Temperature().Return_value.ToString();
            LiveHumidity.Text = rest.Humidity().Return_value.ToString();
            LiveLightIntensity.Text = rest.Analog(1).Return_value.ToString();
            LiveWaterLevel.Text = rest.Analog(2).Return_value.ToString();
            LiveSoilMoister.Text = rest.Analog(3).Return_value.ToString();
        }
    }
  
}