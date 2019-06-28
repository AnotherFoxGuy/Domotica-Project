using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Green.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Green.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        ChartData chartData = new ChartData();
        Greenhouse selectedGreenhouse;
        /// <summary>
        /// Constructor of ChartPage
        /// </summary>
        /// <param name="greenhouse"> Object of Greenhouse</param>
        public ChartPage(Greenhouse greenhouse)
        {
            InitializeComponent();
            selectedGreenhouse = greenhouse;
            ChartTimeTable.ItemsSource = new List<string> { "Day", "Week", "Month", "Year" };
            ChartWeekNumber.ItemsSource = Enumerable.Range(1, 52).ToList();
            ChartMonthName.ItemsSource = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Noveber", "December" };
        }

        /// <summary>
        /// Fills the Charts
        /// </summary>
        /// <param name="timeTable"> Gives the Selected Time window (Day,Week,Month,Year)</param>
        /// <param name="dateWeekMonth">Gives the Selected Month</param>
        public void fillCharts(string timeTable, int dateWeekMonth)
        {
            //HumidityChart.Chart = chartData.GetChartDataBy(timeTable, dateWeekMonth, "Humidity", selectedGreenhouse.Greenhouse_ID);
            //TempratureChart.Chart = chartData.GetChartDataBy(timeTable, dateWeekMonth, "Temperature", selectedGreenhouse.Greenhouse_ID);
            //MoisterChart.Chart = chartData.GetChartDataBy(timeTable, dateWeekMonth, "Moisture", selectedGreenhouse.Greenhouse_ID);
        }
        /// <summary>
        /// Go to Settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(selectedGreenhouse));
        }
        /// <summary>
        /// Go to the MainPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        /// <summary>
        /// Checks what Timewindow you want 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartTimeTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            switch (picker.SelectedItem.ToString())
            {
                case "Day":
                    ChartDatePicker.IsVisible = true;
                    ChartWeekNumber.IsVisible = false;
                    ChartMonthName.IsVisible = false;
                    break;

                case "Week":
                    ChartDatePicker.IsVisible = false;
                    ChartWeekNumber.IsVisible = true;
                    ChartMonthName.IsVisible = false;
                    break;

                case "Month":
                    ChartDatePicker.IsVisible = false;
                    ChartWeekNumber.IsVisible = false;
                    ChartMonthName.IsVisible = true;
                    break;

                default:
                    ChartDatePicker.IsVisible = false;
                    ChartWeekNumber.IsVisible = false;
                    ChartMonthName.IsVisible = false;
                    fillCharts(picker.SelectedItem.ToString(), 2019);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            testdate.Text = e.NewDate.ToShortDateString().Replace("-", string.Empty); //28-6-2019 => 2862019
        }

        private void ChartWeekNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChartMonthName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Go To Live Data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LiveData_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RealTime(selectedGreenhouse));
        }
    }
}