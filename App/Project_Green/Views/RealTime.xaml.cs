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
        readonly ArdunoRestClient rest;
        readonly Greenhouse greenhouse;

        public RealTime(Greenhouse greenhouse)
        {
            InitializeComponent();
            this.greenhouse = greenhouse;
            rest = new ArdunoRestClient { BaseUrl = $"http://{greenhouse.Greenhouse_IP}/" };
            SetTimer();

        }
        private void SetTimer()
        {
            Test.Text = "Timer Started";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                try
                {
                    LiveTemprature.Text = $"Temprature : {rest.Temperature().Temperature.ToString()}";
                    LiveHumidity.Text = $"Humidity : {rest.Humidity().Humidity.ToString()} ";
                    LiveLightIntensity.Text = $"Light intesety : {rest.Analog(0).Return_value.ToString()} ";
                    LiveWaterLevel.Text = $"Water Level : {rest.Analog(1).Return_value.ToString()}";
                    LiveSoilMoister.Text = $"Soil Moister : {rest.Analog(2).Return_value.ToString()}";
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
    }

}