using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace tthk_xamarin_grid
{
    class Cell : Image
    {
        const string CellDeepskyblueColor = "#0099FF";
        public Cell(int row, int column) 
        {
            HeightRequest = 125;
            BackgroundColor = Color.FromHex(CellDeepskyblueColor);
            this.Column = column;
            this.Row = row;
            this.Status = CrossZeroValues.Null;
        }

        public int Row
        {
            get => Row;
            set => Row = value;

        }
        
        public int Column
        {
            get => Column;
            set => Column = value;

        }

        public int CellStatus
        {
            get => CellStatus;
            set => CellStatus = value;
        }

        public CrossZeroValues Status
        {
            get
            {
                return Status;
            }
            set
            {
                if (value == CrossZeroValues.Cross)
                {
                    this.Source = "cross.png";
                }
                else if (value == CrossZeroValues.Zero)
                {
                    this.Source = "zero.png";
                }
                else
                {
                    this.Source = "";
                }

                Status = value;
            }
        }
    }
}
