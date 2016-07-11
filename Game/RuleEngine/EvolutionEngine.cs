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
        private const int UnderPopulationThreshold = 2,
               OverPopulationThreshold = 3,
               ReproductionThreshold = 3;

        private Generation CurrentGeneration { get; set; }  
               
        public EvolutionEngine(Generation initialGeneration)
        {
            CurrentGeneration = initialGeneration;            
        }

        /// <summary>
        /// Applies rules to evolve the current generation into the next generation.
        /// </summary>        
        public void EvolveGeneration()
        {          

            IList<Tuple<int, int, StateOfLife>> cellLifeChangeTupleList = new List<Tuple<int, int, StateOfLife>>();

            for (int row = 0; row < CurrentGeneration.UniverseSize; row++)
            {
                for (int column = 0; column < CurrentGeneration.UniverseSize; column++)
                {
                    Cell cell = CurrentGeneration.GetCell(row, column);

                    int numberOfAliveNeighbors = GetNumberOfAliveNeighbors(CurrentGeneration, cell);

                    //1. Any live cell with fewer than two live neighbours dies (due to underpopulation).
                    //2. Any live cell with two or three live neighbours lives on to the next generation.
                    //3. Any live cell with more than three live neighbours dies, (due to overcrowding).
                    if (cell.Status == StateOfLife.Alive 
                            && (numberOfAliveNeighbors < UnderPopulationThreshold 
                                || numberOfAliveNeighbors > OverPopulationThreshold))
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, StateOfLife>(row, column, StateOfLife.Dead));
                    }

                    //4. Any dead cell with exactly three live neighbours becomes a live cell (by reproduction).
                    else if (cell.Status == StateOfLife.Dead 
                                && numberOfAliveNeighbors == ReproductionThreshold)
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, StateOfLife>(row, column, StateOfLife.Alive));
                    }
                }
            }

            foreach (var tuple in cellLifeChangeTupleList)
            {
                CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3);
            }       
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
       
        /// <summary>
        /// Find the live neighbour cells, there are max eight cells
        /// </summary>
        /// <param name="currentGeneration"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int GetNumberOfAliveNeighbors(Generation currentGeneration, Cell cell)
        {
            int numberOfAliveNeighbours = 0;

            List<Cell> neighboringCells = new List<Cell>
            {
                currentGeneration.GetCell(cell.Row - 1, cell.Column - 1),
                currentGeneration.GetCell(cell.Row - 1, cell.Column + 1),
                currentGeneration.GetCell(cell.Row, cell.Column + 1),
                currentGeneration.GetCell(cell.Row + 1, cell.Column + 1),
                currentGeneration.GetCell(cell.Row + 1, cell.Column),
                currentGeneration.GetCell(cell.Row + 1, cell.Column - 1),
                currentGeneration.GetCell(cell.Row, cell.Column - 1),
                currentGeneration.GetCell(cell.Row - 1, cell.Column)
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