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
        DatabaseManager Henk = new DatabaseManager();
        public SettingsPage()
        {
           
            InitializeComponent();
        }

        private void GotoGraphsPage(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ChartPage();
          
        }

        private void Humidity_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            HumidityLable.Text = "Auto Watering :"  + Convert.ToSingle(HumiditySlider.Value).ToString();
        }

        private void Watering_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Fans_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void TempratureSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            TempratureLable.Text = "Trigger fans on Temprature:" + Convert.ToSingle(TempratureSlider.Value).ToString();
        }
    }
}