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
	public partial class RegisterPage : ContentPage
	{
        DatabaseManager databaseManager = new DatabaseManager();
        public RegisterPage()
		{
			InitializeComponent();
		}

        private void Create_Clicked(object sender, EventArgs e)
        {
            if (databaseManager.DoesUserExist(Username.Text, Password.Text))
                DisplayAlert("Allready exist", "Please login or create a new account", "OK");

            else
            {
                databaseManager.AddUser(Username.Text, Password.Text);
                Application.Current.MainPage = new LoginPage();
            }              
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}