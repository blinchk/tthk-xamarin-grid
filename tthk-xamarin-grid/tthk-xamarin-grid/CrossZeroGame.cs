using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static tthk_xamarin_grid.CrossZeroValues;

namespace tthk_xamarin_grid
{
	public class CrossZeroGame : ContentPage
    {
        private Cell cell;
        private Button resetGameButton;
        private Label currentTurn, winStatus;
        public Grid generatableGrid;
        List<Cell> cells;
        public bool Turn;
        const int GridColumnsRowsNum = 3; // Grid will be 5x5
        private const bool Cross = true;
        private const bool Zero = false;

        public CrossZeroGame()
        {
            generatableGrid = StartGame();
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
                Children = { currentTurn, winStatus, generatableGrid, buttonsLayout }
            };

            Content = stackLayout;
            resetGameButton.Clicked += ResetButtonClicked;
        }

        private int CheckForWin()
        {
            return 0;
        }

        private void CheckForBlankBoxes()
        {
            int notBlankCells = 0;
            foreach (Cell cell in cells)
            {
                if (cell.Status != CrossZeroValues.Zero)
                {
                    notBlankCells++;
                }
            }

            if (notBlankCells == 0)
            {
            }
        }

        private void GetRandomTurn()
        {
            Random random = new Random();
            Turn = Convert.ToBoolean(random.Next(2));
            currentTurn.Text = Turn ? "X player turn" : "O player turn";
        }

        private void ResetButtonClicked(object sender, EventArgs e)
        {
            StartGame();
        }

        private Grid FillGroundWithCells(Grid playground)
        {
            for (int i = 0; i < GridColumnsRowsNum; i++)
            {
                for (int j = 0; j < GridColumnsRowsNum; j++)
                {
                    cell = new Cell(i, j);
                    generatableGrid.Children.Add(cell, cell.Row, cell.Column);
                    cells.Add(cell);
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += CrossOrZeroTapped;
                    cell.GestureRecognizers.Add(tap);
                }
            }

            return playground;
        }

        private Grid SetRowAndColumnDefintions(Grid playground)
        {
            for (int i = 0; i < GridColumnsRowsNum; i++)
            {
                playground.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                playground.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            return playground;
        }
        
        private Grid StartGame()
        {
            Grid playground = new Grid() {
                HeightRequest = 375
            };
            playground = SetRowAndColumnDefintions(playground);
            playground = FillGroundWithCells(playground);
            GetRandomTurn();
            return playground;
        }

        private void UpdateTurn()
        {
            if (Turn == Cross)
            {
                Turn = Zero;
            }
            else
            {
                Turn = Cross;
            }
        }

        private void CrossOrZeroTapped(object sender, EventArgs e)
        {
            Cell tappedCell = sender as Cell;
            if (tappedCell != null && 
                Turn == Cross && 
                tappedCell.Status == CrossZeroValues.Null)
            {
                tappedCell.Status = CrossZeroValues.Cross;
            }
            else if (tappedCell != null && 
                     Turn == Zero && 
                     tappedCell.Status == CrossZeroValues.Null)
            {
                tappedCell.Status = CrossZeroValues.Zero;
            }
        }
    }
}