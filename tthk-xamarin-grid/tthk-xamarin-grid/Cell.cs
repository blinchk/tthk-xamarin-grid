using Xamarin.Forms;

namespace tthk_xamarin_grid
{
    class Cell : Image
    {
        const string CELL_DEEPSKYBLUE_COLOR = "#0099FF";
        int _CellStatus;
        int[] _CellPosition;

        public Cell(int row, int column) 
        {
            HeightRequest = 125;
            BackgroundColor = Color.FromHex(CELL_DEEPSKYBLUE_COLOR);
            _CellPosition = new int[2] { row, column };
        }

        public int[] CellsPosition
        {
            get => _CellPosition;
        }

        public int CellStatus
        {
            get => _CellStatus;
            set => _CellStatus = value;
        }

        public void SetCross()
        {
            Source = "cross.png";
        }

        public void SetZero()
        {
            Source = "zero.png";
        }


    }
}
