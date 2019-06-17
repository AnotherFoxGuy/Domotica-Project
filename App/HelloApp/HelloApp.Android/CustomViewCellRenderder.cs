using HelloApp;
using Android.Content;
using Android.Views;
using Xamarin.Forms;
using HelloApp.Droid;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderder))]
namespace HelloApp.Droid
{
    public class CustomViewCellRenderder : ViewCellRenderer
    {

        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _selected = false;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;
                if (_selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}