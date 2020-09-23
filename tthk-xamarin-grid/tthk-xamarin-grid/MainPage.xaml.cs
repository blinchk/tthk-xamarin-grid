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
            Grid grid = new Grid() {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength(1,GridUnitType.Star) }
                }
            };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    boxView = new BoxView { Color = Color.FromHex("#0099FF") };
                    grid.Children.Add(boxView, i, j);
                }
            }

            var tap = new TapGestureRecognizer();
            tap.Tapped += BoxTapped;
            Content = grid;
        }

        private void BoxTapped(object sender, EventArgs e)
        {
        }
    }
}
