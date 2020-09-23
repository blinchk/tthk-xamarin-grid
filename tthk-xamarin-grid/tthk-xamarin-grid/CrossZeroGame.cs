using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

using Xamarin.Forms;

namespace tthk_xamarin_grid
{
	public class CrossZeroGame : ContentPage
    {
        Image box;
        Button resetGameButton;
        Label currentTurn;
        public bool turn;
        const int GRID_COLUMNS_ROWS_NUM = 3; // Grid will be 5x5
        public CrossZeroGame()
        {
            Random random = new Random();
            turn = Convert.ToBoolean(random.Next(2));
            Grid playground = new Grid() {
                HeightRequest = 375
            };
            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                playground.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                playground.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                for (int j = 0; j < GRID_COLUMNS_ROWS_NUM; j++)
                {
                    box = new Image { HeightRequest = 125,
                        BackgroundColor = Color.FromHex("#0099FF")
                    };
                    playground.Children.Add(box, i, j);
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += CrossOrZeroTapped;
                    box.GestureRecognizers.Add(tap);
                }
            }

            currentTurn = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            resetGameButton = new Button()
            {
                Text = "Reset Game",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout buttonsLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { resetGameButton }
            };

            StackLayout stackLayout = new StackLayout()
            {
                Children = { currentTurn, playground, buttonsLayout }
            };

            Content = stackLayout;
            currentTurn.Text = turn ? "X player turn" : "O player turn";
        }

        private void UpdateTurn()
        {
            if (turn = true)
            {
                turn = false;
                currentTurn.Text = "X player turn";
            }
            else
            {
                turn = true;
                currentTurn.Text = "O player turn";
            }
        }

        private void CrossOrZeroTapped(object sender, EventArgs e)
        {
            Image box = sender as Image;
            if (turn)
            {
                box.Source = "cross.png";
                UpdateTurn();
            }
            else
            {
                turn = true;
                box.Source = "zero.png";
            }
        }
    }
}