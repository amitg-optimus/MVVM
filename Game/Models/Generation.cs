﻿using Game.Utilities;
using System;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Represents a particular generation of the game of life.
    /// </summary>
    public class Generation
    {
        /// <summary>
        /// A two-dimensional array representing a finite universe.
        /// </summary>
        private readonly Cell[,] universe;

        /// <summary>
        /// Get the size of the universe.
        /// </summary>
        public int UniverseSize { get; set; }

        /// <summary>
        /// Initialises a new instance of a Generation.
        /// </summary>
        /// <param name="universeSize">Size of the universe.</param>
        public Generation(int universeSize)
        {
            universe = new Cell[universeSize, universeSize];
            UniverseSize = universeSize;

            Initialise();
        }

        /// <summary>
        /// Initialises the generation.
        /// </summary>
        private void Initialise()
        {
            for (int row = 0; row < UniverseSize; row++)
                for (int column = 0; column < UniverseSize; column++)
                    universe[row, column] = new Cell(row, column, StateOfLife.Dead);
        }        

        /// <summary>
        /// Gets a specified cell.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <returns>The specified cell.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row < 0 || row >= UniverseSize)
                return null;

            if (column < 0 || column >= UniverseSize)
                return null;

            return universe[row, column];
        }

        /// <summary>
        /// Sets a particular cell in the universe to be dead or alive.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <param name="alive">A boolean value that indicates if this cell is dead or alive.</param>
        public void SetCell(int row, int column, StateOfLife status)
        {
            Cell cell = GetCell(row, column);

            if (cell == null)
                throw new ArgumentOutOfRangeException(
                    "The specified row and column do not map to a valid cell."
                );

            cell.Status = status;
        }

        /// <summary>
        /// Toggles the living status of a cell.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Colummn index of cell.</param>
        public void ToggleCellLife(int row, int column)
        {
            Cell cell = GetCell(row, column);
            
            if (cell.Status==StateOfLife.Alive)
                cell.Status = StateOfLife.Dead;
            else
                cell.Status = StateOfLife.Alive;
        }

        /// <summary>
        /// Builds a string representation of this generation.
        /// </summary>
        /// <returns>String representation of this generation.</returns>
        public override string ToString()
        {
            StringBuilder gridString = new StringBuilder();

            for (int row = 0; row < UniverseSize; row++)
            {
                for (int column = 0; column < UniverseSize; column++)
                {
                    gridString.Append(
                        string.Format("{0} ", GetCell(row, column).Status == StateOfLife.Alive ? "1" : "0")
                    );
                }

                gridString.AppendLine();
            }

            return gridString.ToString();
        }
    }
}