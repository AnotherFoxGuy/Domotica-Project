using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Project_Green.Models;
using System.Net;
using System.Text.RegularExpressions;
using Microcharts;
using SkiaSharp;

namespace Project_Green
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GreenhouseList.ItemsSource = new IPScanner().GetGreenhouses();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            GreenhouseList.ItemsSource = new IPScanner().GetGreenhouses();
        }
    }
}
