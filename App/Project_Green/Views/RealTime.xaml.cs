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
            LiveData1.Text = "Timer Started";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                try
                {
                    i++;
                    FillData();
                    return true; // return true to repeat counting, false to stop timer
                }
                catch (Exception e)
                {
                    LiveData1.Text = e.Message;
                    return false;
                }
            });
        }
        public void FillData()
        {
            LiveData1.Text = rest.Analog(1).Return_value.ToString();
            LiveData2.Text = rest.Analog(2).Return_value.ToString();
            LiveData3.Text = rest.Analog(3).Return_value.ToString();
            LiveData4.Text = rest.Analog(4).Return_value.ToString();
        }
    }
  
}