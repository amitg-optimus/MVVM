using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Game.Models;
using Game.Utilities;

namespace Game.RuleEngine
{
    /// <summary>
    /// The engine that implements Conway's Game of Life rules.
    /// </summary>
    public class EvolutionEngine
    {
        /// <summary>
        /// Gets the current generation.
        /// </summary>
        private Generation CurrentGeneration { get; set; }        

        /// <summary>
        /// Initialises a new instance of the LifeEngine with a specified initial generation.
        /// </summary>
        /// <param name="initialGeneration">The initial generation to start from.</param>
        public EvolutionEngine(Generation initialGeneration)
        {
            CurrentGeneration = initialGeneration;            
        }

        /// <summary>
        /// Applies Conway's life rules to evolve the current generation into the next generation.
        /// </summary>
        /// <returns>An EvolutionEngineActionResult.</returns>
        public EvolutionEngineActionResult EvolveGeneration()
        {
            const int UnderPopulationThreshold = 2,
                OverPopulationThreshold = 3,
                ReproductionThreshold = 3;

            IList<Tuple<int, int, StateOfLife>> cellLifeChangeTupleList = new List<Tuple<int, int, StateOfLife>>();

            for (int row = 0; row < CurrentGeneration.UniverseSize; row++)
            {
                for (int column = 0; column < CurrentGeneration.UniverseSize; column++)
                {
                    Cell cell = CurrentGeneration.GetCell(row, column);

                    int numberOfAliveNeighbors = GetNumberOfAliveNeighbors(CurrentGeneration, cell);

                    if (cell.Status == StateOfLife.Alive && (numberOfAliveNeighbors < UnderPopulationThreshold || numberOfAliveNeighbors > OverPopulationThreshold))
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, StateOfLife>(row, column, StateOfLife.Dead));
                    }
                    else if (cell.Status == StateOfLife.Dead && numberOfAliveNeighbors == ReproductionThreshold)
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, StateOfLife>(row, column, StateOfLife.Alive));
                    }
                }
            }

            if (cellLifeChangeTupleList.Any())
            {
                Parallel.ForEach(
                    cellLifeChangeTupleList,
                    tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
                );
            }

            return new EvolutionEngineActionResult(
                evolutionEnded: !cellLifeChangeTupleList.Any()
                
            );
        }       
         
        
        public int GetUniverseSize()
        {
            return CurrentGeneration.UniverseSize;
        }
        
        public Cell GetCell(int row, int column)
        {
            return CurrentGeneration.GetCell(row, column);
        }
       
        public void SetCell(int row, int column, StateOfLife status)
        {
            CurrentGeneration.SetCell(row, column, status);
        }
       
        public void ToggleCellLife(int row, int column)
        {
            CurrentGeneration.ToggleCellLife(row, column);
        }
       
        private int GetNumberOfAliveNeighbors(Generation generation, Cell cell)
        {
            int numberOfAliveNeighbours = 0;

            List<Cell> neighboringCells = new List<Cell>
            {
                generation.GetCell(cell.Row - 1, cell.Column - 1),
                generation.GetCell(cell.Row - 1, cell.Column + 1),
                generation.GetCell(cell.Row, cell.Column + 1),
                generation.GetCell(cell.Row + 1, cell.Column + 1),
                generation.GetCell(cell.Row + 1, cell.Column),
                generation.GetCell(cell.Row + 1, cell.Column - 1),
                generation.GetCell(cell.Row, cell.Column - 1),
                generation.GetCell(cell.Row - 1, cell.Column)
            };            

            foreach (var neighboringCell in neighboringCells)
            {
                if(neighboringCell != null && neighboringCell.Status== StateOfLife.Alive)
                {
                    numberOfAliveNeighbours++;
                }
            }

            return numberOfAliveNeighbours;
        }
    }
}