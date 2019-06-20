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
using Project_Green.Views;

namespace Project_Green
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IPScanner ipsc;

        public MainPage()
        {
            InitializeComponent();
            ipsc = new IPScanner();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            GreenhouseList.ItemsSource = ipsc.GetGreenhouses();
        }
    }
}
