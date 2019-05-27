using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private List<string> _names;
        
        
        public MainPage()
        {
            InitializeComponent();
            _names = new List<string>();
        }


        private void AddButton_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NameEntry.Text))
                _names.Add(NameEntry.Text);

        }

        private void ShowButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListPage(_names));
        }
    }
}
