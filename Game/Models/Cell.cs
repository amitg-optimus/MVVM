using Game.Utilities;

namespace Game.Models
{
    public class Cell : ObservableBase
    {
        public int Row { get; set; }

        public int Column { get; set; }

        private StateOfLife status;

        public StateOfLife Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public Cell(int row, int column, StateOfLife status)
        {
            Row = row;
            Column = column;
            Status = status;
        }

    }
}
