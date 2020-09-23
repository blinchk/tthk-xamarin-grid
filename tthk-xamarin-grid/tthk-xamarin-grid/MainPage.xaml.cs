using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tthk_xamarin_grid
{
    public partial class MainPage : ContentPage
    {
        BoxView boxView;
        public MainPage()
        {
            Grid grid = new Grid();

            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    boxView = new BoxView { Color = Color.FromHex("#0099FF") };
                    grid.Children.Add(boxView, i, j);
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += Tap_Tapped;
                    boxView.GestureRecognizers.Add(tap);
                }
            }
            Content = grid;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            BoxView boxView = sender as BoxView;
            boxView.Color = Color.Tomato;
        }
    }
}
