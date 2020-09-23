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
        const int GRID_COLUMNS_ROWS_NUM = 5; // Grid will be 5x5
        const string CELL_SKYBLUE_COLOR = "#0099FF";
        public MainPage()
        {
            Grid grid = new Grid();

            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                for (int j = 0; j < GRID_COLUMNS_ROWS_NUM; j++)
                {
                    boxView = new BoxView { Color = Color.FromHex(CELL_SKYBLUE_COLOR) };
                    grid.Children.Add(boxView, i, j);
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += BoxViewTapped;
                    boxView.GestureRecognizers.Add(tap);
                }
            }
            Content = grid;
        }

        public Dictionary<BoxView, int> clickedBoxViews = new Dictionary<BoxView, int> { };
        private void BoxViewTapped(object sender, EventArgs e)
        {
            BoxView boxView = sender as BoxView;
            if (clickedBoxViews.ContainsKey(boxView))
            {
                switch (clickedBoxViews[boxView])
                {
                    case 0:
                        boxView.Color = Color.FromHex(CELL_SKYBLUE_COLOR);
                        clickedBoxViews[boxView]++;
                        break;
                    case 1:
                        boxView.Color = Color.ForestGreen;
                        clickedBoxViews[boxView]++;
                        break;
                    case 2:
                        boxView.Color = Color.Tomato;
                        clickedBoxViews[boxView] = 0;
                        break;
                }
            }
            else
            {
                clickedBoxViews.Add(boxView, 1);
                boxView.Color = Color.ForestGreen;
            }
                   
        }
    }
}
