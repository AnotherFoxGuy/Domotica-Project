using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HelloApp.MenuItems;

namespace HelloApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        DatabaseManager databaseManager = new DatabaseManager();
        public LoginPage()
		{
			InitializeComponent();
            databaseManager.UserLogout();
            NavigationPage.SetHasNavigationBar(this, false);
            NoAccountLabel.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => OnLabelClicked()),
            });
		}

        private void Login_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Username.Text) && !String.IsNullOrEmpty(Password.Text))
            {
                if (databaseManager.DoesUserExist(Username.Text, Password.Text))
                {
                    databaseManager.UserLogin(Username.Text);
                    Application.Current.MainPage = new MasterPage();
                }
                else
                    DisplayAlert("Wrong username or password", "Please try again", "OK");
            }
            else
                DisplayAlert("Empty field", "Username & password cannot be empty!", "OK");
        }

        private void OnLabelClicked()
        {
            Application.Current.MainPage = new RegisterPage();
        }
    }
}