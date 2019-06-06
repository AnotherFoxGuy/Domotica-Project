using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public MainPage()
        {
            InitializeComponent();
        }

        public void ToggleSwitch(string ip, string state, int switchNr)
        {
            Uri uri = new Uri($"http://{ip}/transmitter{state}?params={switchNr}");
            HttpResponseMessage _ =  _client.PutAsync(uri, _content).Result;
        }

        private void Switch1_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch1.IsToggled ? "On" : "Off", 0);
        }

        private void Switch2_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch2.IsToggled ? "On" : "Off", 1);
        }

        private void Switch3_Toggled(object sender, ToggledEventArgs e)
        {
            ToggleSwitch(ipfield.Text, Switch3.IsToggled ? "On" : "Off", 2);
        }
    }
}
