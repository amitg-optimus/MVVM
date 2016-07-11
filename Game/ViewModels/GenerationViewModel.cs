using Game.Models;
using Game.RuleEngine;
using Game.Utilities;

namespace Game.ViewModels
{
    /// <summary>
    /// A view model to represent the current generation in
    /// the game of life.
    /// </summary>
    public class GenerationViewModel : ObservableBase
    {
        /// <summary>
        /// Life engine instance.
        /// </summary>
        private readonly EvolutionEngine engine;

        /// <summary>
        /// Gets the current universe size.
        /// </summary>
        public int UniverseSize { get { return engine.GetUniverseSize(); } }              

        #region Command Properties
        /// <summary>
        /// RelayCommand for evolving the current generation.
        /// </summary>
        public RelayCommand<object> EvolveCommand { get; set; }        

        /// <summary>
        /// RelayCommand for toggling a particular cell's life.
        /// </summary>
        public RelayCommand<string> ToggleCellLifeCommand { get; set; }
        #endregion

        /// <summary>
        /// Initialises a new instance of GenerationViewModel with the specified universe size.
        /// </summary>
        /// <param name="universeSize">Universesize.</param>
        public GenerationViewModel(int universeSize)
        {
            engine = new EvolutionEngine(new Generation(universeSize));

            EvolveCommand = new RelayCommand<object>(
                _ => EvolveGeneration()
            );
            

            ToggleCellLifeCommand = new RelayCommand<string>(
                (cellRowColumn) => ToggleCellLife(cellRowColumn)
            );
           
        }
        
        /// <summary>
        /// Gets the specified cell from the current generation.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns></returns>
        public Cell GetCell(int row, int column)
        {
            return engine.GetCell(row, column);
        }

        /// <summary>
        /// Evolves the current generation.
        /// </summary>
        private void EvolveGeneration()
        {
            engine.EvolveGeneration();
        }
       
        /// <summary>
        /// Makes a specfied cell alive or dead.
        /// </summary>
        /// <param name="cellRowColumn">Formatted string identifying a particular cell. Format is "rowIndex,columnIndex"<param>
        private void ToggleCellLife(string cellRowColumn)
        {
            string[] cellRowColumnSplit = cellRowColumn.Split(',');

            int row = int.Parse(cellRowColumnSplit[0]);
            int column = int.Parse(cellRowColumnSplit[1]);

            engine.ToggleCellLife(row, column);
        }       
    }
}