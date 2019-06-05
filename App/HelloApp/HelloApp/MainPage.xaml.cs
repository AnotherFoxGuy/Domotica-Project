using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Mqtt;

namespace HelloApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        IMqttClient _client;

        public MainPage()
        {
            InitializeComponent();
            var configuration = new MqttConfiguration();

            var kaas = Task.Run(async () => await MqttClient.CreateAsync("vps693792.ovh.net", configuration));
            kaas.Wait();
            _client = kaas.Result;

            Task.Run(async () => await _client.ConnectAsync(new MqttClientCredentials("Kaas-App", "mqtt-test", "mqtt-test"))).Wait();
        }

        private void Switch1_Toggled(object sender, ToggledEventArgs e)
        {
            var message = new MqttApplicationMessage("Switch/Toggled", Encoding.UTF8.GetBytes("1"));
            Task.Run(async () => await _client.PublishAsync(message, MqttQualityOfService.AtMostOnce)).Wait();
        }

        private void Switch2_Toggled(object sender, ToggledEventArgs e)
        {
            var message = new MqttApplicationMessage("Switch/Toggled", Encoding.UTF8.GetBytes("2"));
            Task.Run(async () => await _client.PublishAsync(message, MqttQualityOfService.AtMostOnce)).Wait();
        }

        private void Switch3_Toggled(object sender, ToggledEventArgs e)
        {
            var message = new MqttApplicationMessage("Switch/Toggled", Encoding.UTF8.GetBytes("3"));
            Task.Run(async () => await _client.PublishAsync(message, MqttQualityOfService.AtMostOnce)).Wait();
        }
    }
}
