using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace tthk_xamarin_grid
{
    public class CrossZeroGame : ContentPage
    {
        Image box;
        Button resetGameButton;
        Label currentTurn, winStatus;
        Grid playground;
        Dictionary<Image, int> crossZeros;
        private Image[,] boxPosition;
        public bool turn;
        const int GRID_COLUMNS_ROWS_NUM = 3; // Grid will be 5x5
        private const bool Cross = true;
        private const bool Zero = false;

        public CrossZeroGame()
        {
            Title = "Tic-Tac-Toe Game";
            crossZeros = new Dictionary<Image, int>();

            playground = new Grid() {
                HeightRequest = 375
            };

            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                playground.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                playground.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            boxPosition = new Image[3,3];
            for (int i = 0; i < GRID_COLUMNS_ROWS_NUM; i++)
            {
                for (int j = 0; j < GRID_COLUMNS_ROWS_NUM; j++)
                {
                    box = new Image { HeightRequest = 125,
                        BackgroundColor = Color.FromHex("#0099FF")
                    };
                    playground.Children.Add(box, i, j);
                    boxPosition[i, j] = box;
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
            GetFirstPlayer();
        }

        public async void GetFirstPlayer()
        {
            string firstPlayer = await DisplayActionSheet("Who is first player?", "Cancel", null, "Cross", "Zero");
            if (firstPlayer == "Cross")
            {
                turn = true;
            }
            else
            {
                turn = false;
            }
            currentTurn.Text = turn ? "X player turn" : "O player turn";
        }

        private int GetWinner()
        {
            if (turn == Zero)
            {
                return 1;
            }
            return 2;
        }
        
        private int CheckForWin()
        {
            if (boxPosition[0,0].Source != null && 
                boxPosition[1,1].Source != null && 
                boxPosition[2,2].Source != null &&
                crossZeros[boxPosition[0,0]] == crossZeros[boxPosition[1,1]] && 
                crossZeros[boxPosition[1,1]] == crossZeros[boxPosition[2,2]])
            {
                return GetWinner();
            }
            if (boxPosition[0,2].Source != null && boxPosition[1,1].Source != null &&  boxPosition[2,0].Source != null &&
                crossZeros[boxPosition[0,2]] == crossZeros[boxPosition[1,1]] && 
                crossZeros[boxPosition[1,1]] == crossZeros[boxPosition[2,0]])
            {
                return GetWinner();
            }

            for (int i = 0; i < 3; i++)
            {
                if (boxPosition[0,i].Source != null && boxPosition[1,i].Source != null && boxPosition[2,i].Source != null &&
                    crossZeros[boxPosition[0,i]] == crossZeros[boxPosition[1,i]] && 
                    crossZeros[boxPosition[1,i]] == crossZeros[boxPosition[2,i]])
                {
                    return GetWinner();
                }
                if (boxPosition[i,0].Source != null && boxPosition[i,1].Source != null && boxPosition[i,2].Source != null &&
                    crossZeros[boxPosition[i,0]] == crossZeros[boxPosition[i,1]] && 
                    crossZeros[boxPosition[i,1]] == crossZeros[boxPosition[i,0]])
                {
                    return GetWinner();
                }
            }

            return 0;
        }

        private void CheckForBlankBoxes()
        {
            int win = CheckForWin();
            if (win == 1)
            {
                winStatus.Text = "Cross player won.";
                DisplayAlert("Win", "Cross player won", "OK");
                ResetImages();
            }
            else if (win == 2)
            {
                winStatus.Text = "Zero player won.";
                DisplayAlert("Win", "Zero player won", "OK");
                ResetImages();
            }
            else
            {
                winStatus.Text = "Draw.";
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
                image.Source = null;
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
            else
            {
                DisplayAlert("Message", "Enemy has been choosed this box", "OK");
            }
        }
    }
}