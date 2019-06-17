using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPage : ContentPage
	{
        DatabaseManager databaseManager = new DatabaseManager();
		public AddPage()
		{
			InitializeComponent();
            MovieImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => SelectImageClicked(true)),
            });
            SerieImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => SelectImageClicked(false)),
            });
        }

        private void SelectImageClicked(bool type)
        {
            if (type)
            {
                MovieImage.Opacity = 1;
                SerieImage.Opacity = 0.5;
                HideSeries.IsVisible = false;
            }
            else
            {
                MovieImage.Opacity = 0.5;
                SerieImage.Opacity = 1;
                HideSeries.IsVisible = true;
            }
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            if (!HideSeries.IsVisible)
            {
                if (String.IsNullOrEmpty(Title.Text))
                    DisplayAlert("Empty field", "Please enter a title", "OK");
                else if (String.IsNullOrEmpty(Year.Text))
                    DisplayAlert("Empty field", "Please enter a year", "OK");
                else
                {
                    databaseManager.AddMovie(Title.Text, Description.Text, int.Parse(Year.Text));
                    DisplayAlert("Succes", "Movie has been added", "OK");
                    ClearEntry();
                }
            }
            if (HideSeries.IsVisible)
            {
                if (String.IsNullOrEmpty(Title.Text))
                    DisplayAlert("Empty field", "Please enter a title", "OK");
                else if (String.IsNullOrEmpty(Year.Text))
                    DisplayAlert("Empty field", "Please enter a year", "OK");
                else if (String.IsNullOrEmpty(YearEnded.Text))
                    YearEnded.Text = "-1";
                else if (String.IsNullOrEmpty(NumberOfEpisodes.Text))
                    DisplayAlert("Empty field", "Please enter the number of episodes", "OK");
                else if (String.IsNullOrEmpty(NumberOfSeasons.Text))
                    DisplayAlert("Empty field", "Please enter the number of seasons", "OK");
                else
                {
                    databaseManager.AddSerie(Title.Text, Description.Text, int.Parse(Year.Text), int.Parse(YearEnded.Text), int.Parse(NumberOfEpisodes.Text), int.Parse(NumberOfSeasons.Text));
                    DisplayAlert("Succes", "Serie has been added", "OK");
                    ClearEntry();
                }
            }
        }

        private void ClearEntry()
        {
            Title.Text = String.Empty;
            Description.Text = String.Empty;
            Year.Text = String.Empty;
            YearEnded.Text = String.Empty;
            NumberOfEpisodes.Text = String.Empty;
            NumberOfSeasons.Text = String.Empty;
        }
    }
}