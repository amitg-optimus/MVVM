using Game.Utilities;

namespace Game.Models
{
    public class Cell : ObservableBase
    {
        /// <summary>
        /// Row number
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Column Number
        /// </summary>
        public int Column { get; set; }

        private StateOfLife status;

        /// <summary>
        /// Keeps the status of Cell i.e., Alive or Dead
        /// </summary>
        public StateOfLife Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Inintialize the Cell
        /// </summary>
        /// <param name="row">row number</param>
        /// <param name="column">column number</param>
        /// <param name="status">Alive/Dead</param>
        public Cell(int row, int column, StateOfLife status)
        {
            Row = row;
            Column = column;
            Status = status;
        }

    }
}
