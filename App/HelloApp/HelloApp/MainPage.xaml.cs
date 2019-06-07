using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        HttpClient _client = new HttpClient();
        private HttpContent _content;
        private int _delay;

        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromMilliseconds(5), () =>
            {
                _delay += 5;
                if (_delay > (1000 / interval.Value))
                {
                    if (CheckValidIpAddress(ipfield.Text))
                    {
                        temp.Text = "Temperature: " + DhtSensor(ipfield.Text, "temperature");
                        humid.Text = "Humidity: " + DhtSensor(ipfield.Text, "humidity");
                        potmeter.Text = "Potentiometer: " + Analogpin(ipfield.Text, 0);
                        _delay = 0;
                    }
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        public bool CheckValidIpAddress(string ip)
        {
            if (ip != null)
            {
                //Check user input against regex (check if IP address is not empty).
                Regex regex = new Regex("\\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}\\b");
                Match match = regex.Match(ip);
                return match.Success;
            }
            else return false;
        }

        private string DhtSensor(string ip, string type)
        {
            Uri uri = new Uri($"http://{ip}/{type}");
            HttpResponseMessage _dht = _client.PutAsync(uri, _content).Result;
            return _dht.Content.ReadAsStringAsync().Result;
        }

        private string Analogpin(string ip, int pin)
        {
            Uri uri = new Uri($"http://{ip}/analog/{pin}");
            HttpResponseMessage _analog = _client.PutAsync(uri, _content).Result;
            return _analog.Content.ReadAsStringAsync().Result;
        }

        public void ToggleSwitch(string ip, string state, int switchNr)
        {
            Uri uri = new Uri($"http://{ip}/transmitter{state}?params={switchNr}");
            HttpResponseMessage _ =  _client.PutAsync(uri, _content).Result;
        }

        private void Switch1_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch1.IsToggled ? "Off" : "On", 0);
        }

        private void Switch2_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch2.IsToggled ? "Off" : "On", 1);
        }

        private void Switch3_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch3.IsToggled ? "Off" : "On", 2);
        }
    }
}
