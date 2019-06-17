using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloApp.Views
{
    public partial class MainPage : ContentPage
    {
        DatabaseManager databaseManager = new DatabaseManager();
        public MainPage()
        {
            InitializeComponent();
            MovieList.ItemsSource = databaseManager.GetAllMovies();
            SeriesList.ItemsSource = databaseManager.GetAllSeries();
        }
    }
}
