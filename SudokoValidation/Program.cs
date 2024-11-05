using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuValidator
{
    /// <summary>
    /// Represents a Sudoku board and provides functionality to validate its structure.
    /// </summary>
    public class Sudoku
    {
        private readonly int[][] board;
        private readonly int size;         // Size of the Sudoku grid (e.g., 9 for a 9x9 grid)
        private readonly int subgridSize;  // Size of each subgrid (e.g., 3 for a 9x9 grid)

        /// <summary>
        /// Initializes a new instance of the <see cref="Sudoku"/> class with a given board.
        /// Validates that the board is a square grid and meets Sudoku requirements.
        /// </summary>
        /// <param name="board">2D integer array representing the Sudoku board.</param>
        /// <exception cref="ArgumentNullException">Thrown if the board is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the board is not NxN or if rows have inconsistent lengths.</exception>
        public Sudoku(int[][] board)
        {
            this.board = board ?? throw new ArgumentNullException(nameof(board));
            this.size = board.Length;
            this.subgridSize = (int)Math.Sqrt(size);

            // Ensure the board is NxN and that √N is an integer
            if (size == 0 || subgridSize * subgridSize != size)
                throw new ArgumentException("Board must be NxN where N > 0 and √N is an integer.");

            // Check if all rows are equal in length to the board size
            if (board.Any(row => row.Length != size))
                throw new ArgumentException("All rows in the board must have the same length as the board size.");
        }

        /// <summary>
        /// Validates the entire Sudoku board by checking each row, column, and subgrid.
        /// </summary>
        /// <returns>True if the Sudoku board is valid; otherwise, false.</returns>
        public bool Validate()
        {
            return Rows.All(IsValidSet) && Columns.All(IsValidSet) && Subgrids.All(IsValidSet);
        }

        /// <summary>
        /// Gets all rows in the Sudoku board.
        /// </summary>
        private IEnumerable<IEnumerable<int>> Rows => board;

        /// <summary>
        /// Gets all columns in the Sudoku board by transposing rows to columns.
        /// </summary>
        private IEnumerable<IEnumerable<int>> Columns =>
            Enumerable.Range(0, size).Select(col => board.Select(row => row[col]));

        /// <summary>
        /// Gets all subgrids in the Sudoku board.
        /// Each subgrid is represented as a sequence of integers.
        /// </summary>
        private IEnumerable<IEnumerable<int>> Subgrids =>
            Enumerable.Range(0, subgridSize).SelectMany(row =>
                Enumerable.Range(0, subgridSize).Select(col => GetSubgrid(row, col)));

        /// <summary>
        /// Retrieves a specific subgrid in the Sudoku board based on its starting position.
        /// </summary>
        /// <param name="row">The starting row index of the subgrid.</param>
        /// <param name="col">The starting column index of the subgrid.</param>
        /// <returns>A sequence of integers representing the values in the subgrid.</returns>
        private IEnumerable<int> GetSubgrid(int row, int col)
        {
            int startRow = row * subgridSize;
            int startCol = col * subgridSize;

            // Select values within the specified subgrid area
            return Enumerable.Range(0, subgridSize).SelectMany(r =>
                Enumerable.Range(0, subgridSize).Select(c => board[startRow + r][startCol + c]));
        }

        /// <summary>
        /// Checks if a given section (row, column, or subgrid) is valid.
        /// A valid section contains unique values from 1 to the size of the board.
        /// </summary>
        /// <param name="section">An enumerable of integers representing a row, column, or subgrid.</param>
        /// <returns>True if the section is valid; otherwise, false.</returns>
        private bool IsValidSet(IEnumerable<int> section)
        {
            var values = section.ToArray();
            return values.Length == size &&
                   values.All(v => v >= 1 && v <= size) &&
                   values.Distinct().Count() == size;
        }
    }

    /// <summary>
    /// Entry point for testing the Sudoku validator.
    /// </summary>
    class Program
    {
        static void Main()
        {
            int[][] goodSudoku = new int[][] {
                    new int[] {7,8,4, 1,5,9, 3,2,6},
                    new int[] {5,3,9, 6,7,2, 8,4,1},
                    new int[] {6,1,2, 4,3,8, 7,5,9},
                    new int[] {9,2,8, 7,1,5, 4,6,3},
                    new int[] {3,5,7, 8,4,6, 1,9,2},
                    new int[] {4,6,1, 9,2,3, 5,8,7},
                    new int[] {8,7,6, 3,9,4, 2,1,5},
                    new int[] {2,4,3, 5,6,1, 9,7,8},
                    new int[] {1,9,5, 2,8,7, 6,3,4}
                };

            int[][] badSudoku = new int[][] {
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9},
                    new int[] {1,2,3, 4,5,6, 7,8,9}
                };


            var sudoku = new Sudoku(goodSudoku);
            Console.WriteLine(sudoku.Validate());


            sudoku = new Sudoku(badSudoku);
            Console.WriteLine(sudoku.Validate());

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
