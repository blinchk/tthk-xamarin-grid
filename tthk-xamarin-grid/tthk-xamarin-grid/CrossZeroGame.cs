using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;

namespace tthk_xamarin_grid
{
	public class CrossZeroGame : ContentPage
    {
        Image box;
        Button resetGameButton;
        Label currentTurn, winStatus;
        Grid playground;
        Dictionary<Image, int> crossZeros;
        Dictionary<Image, int[]> boxPosition;
        public bool turn;
        const int GRID_COLUMNS_ROWS_NUM = 3; // Grid will be 5x5
        private const bool Cross = true;
        private const bool Zero = false;

        public CrossZeroGame()
        {
            crossZeros = new Dictionary<Image, int>();

            playground = new Grid() {
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
                    boxPosition.Add(box, new int[2] { i, j });
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += CrossOrZeroTapped;
                    box.GestureRecognizers.Add(tap);
                }
            }

            currentTurn = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontSize = 30,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            winStatus = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontSize = 30,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
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
                Children = { currentTurn, winStatus, playground, buttonsLayout }
            };

            Content = stackLayout;
            resetGameButton.Clicked += ResetButtonClicked;
            GetRandomTurn();
        }

        private int CheckForWin()
        {
            int[] inputArray = crossZeros.Values.ToArray();
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (i < 3)
                {
                    valuesForCheck[0, i] = inputArray[i];
                }
                else if (i < 6)
                {
                    valuesForCheck[1, i - 3] = inputArray[i];
                }
                else
                {
                    valuesForCheck[2, i - 6] = inputArray[i];
                }
            }
            if (valuesForCheck[0, 0] == valuesForCheck[1, 1] && valuesForCheck[1, 1] == valuesForCheck[2, 2])
            {
                return valuesForCheck[0, 0];
            }
            if (valuesForCheck[2, 0] == valuesForCheck[1, 1] && valuesForCheck[1, 1] == valuesForCheck[0, 2])
            {
                return valuesForCheck[2, 0];
            }
            for (int i = 0; i < 3; i++)
            {
                if (valuesForCheck[i, 0] == valuesForCheck[i, 1] && valuesForCheck[i, 1] == valuesForCheck[i, 2])
                {
                    return valuesForCheck[i, 0];
                }
                else if (valuesForCheck[0, i] == valuesForCheck[1, i] && valuesForCheck[1, i] == valuesForCheck[2, i])
                {
                    return valuesForCheck[0, i];
                }
            }
            return 0;
        }

        private void CheckForBlankBoxes()
        {
            if (!crossZeros.ContainsValue(0))
            {
                int win = CheckForWin();
                if (win == 1)
                {
                    winStatus.Text = "Cross player won.";
                }
                else if (win == 2)
                {
                    winStatus.Text = "Zero player won.";
                }
                else
                {
                    winStatus.Text = "Draw.";
                }
            }
        }

        private void GetRandomTurn()
        {
            Random random = new Random();
            turn = Convert.ToBoolean(random.Next(2));
            currentTurn.Text = turn ? "X player turn" : "O player turn";
        }

        private void ResetButtonClicked(object sender, EventArgs e)
        {
            ResetImages();
        }

        private void ResetImages()
        {
            foreach (Image image in playground.Children)
            {
                image.Source = "";
            }
            crossZeros = new Dictionary<Image, int>();
            GetRandomTurn();
        }

        private void UpdateTurn(Image box)
        {
            if (crossZeros[box] == 1)
            {
                box.Source = "cross.png";
                turn = Zero;
                currentTurn.Text = "O player turn";
            }
            else
            {
                box.Source = "zero.png";
                turn = Cross;
                currentTurn.Text = "X player turn";
            }
            CheckForBlankBoxes();
        }

        private void CrossOrZeroTapped(object sender, EventArgs e)
        {
            Image box = sender as Image;
            if (turn == Cross && !crossZeros.ContainsKey(box))
            {
                crossZeros[box] = 1;
                UpdateTurn(box);
            }
            else if (turn == Zero && !crossZeros.ContainsKey(box))
            {
                crossZeros[box] = 2;
                UpdateTurn(box);
            }
        }
    }
}