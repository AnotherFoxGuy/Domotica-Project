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
	public partial class FavoritePage : ContentPage
	{
        DatabaseManager databaseManager = new DatabaseManager();
		public FavoritePage()
		{
			InitializeComponent();
            string currentUser = databaseManager.GetUser().Username;
            MovieList.ItemsSource = databaseManager.GetAllMoviesByUser(currentUser);
            SeriesList.ItemsSource = databaseManager.GetAllSeriesByUser(currentUser);
        }
	}
}