# Sudoku Validator

## Overview

The Sudoku Validator is a C# application designed to validate the correctness of Sudoku boards. It checks if a given Sudoku board adheres to the fundamental rules of Sudoku, ensuring that each row, column, and subgrid contains unique values from 1 to N, where N is the size of the board.

## Features

- Validates standard NxN Sudoku boards (e.g., 9x9).
- Checks for unique values in each row, column, and subgrid.
- Provides error handling for invalid board configurations.
- Can be easily integrated into other applications for Sudoku validation.

## Getting Started

### Prerequisites

- .NET SDK (version 5.0 or higher)
- A compatible IDE such as Visual Studio 2022 or Visual Studio Code

### Installation

1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/yourusername/sudoku-validator.git

### Usage
- .Open the Program.cs file to view the main entry point of the application.
- .You can modify the goodSudoku and badSudoku 2D arrays to test different Sudoku boards.

### Example
- .To validate a Sudoku board, create an instance of the Sudoku class and call the Validate method:
```bash
var sudoku = new Sudoku(goodSudoku);
bool isValid = sudoku.Validate();
Console.WriteLine(isValid); // Output: True or False
```
### Code
Program.cs: Contains the Sudoku class which includes methods for validating the board and retrieving rows, columns, and subgrids.
The entry point of the application for testing the Sudoku validator with sample boards.

### Error Handling
The application provides the following error handling mechanisms:

Throws ArgumentNullException if the provided board is null.
Throws ArgumentException if the board is not NxN or if rows have inconsistent lengths.

### Algorithms Used
The validation algorithm used is based on checking three key components of the Sudoku board:

- Rows: Each row must contain unique values from 1 to N.
- Columns: Each column must contain unique values from 1 to N.
- Subgrids: Each subgrid must also contain unique values from 1 to N.
The algorithm iterates over the rows, columns, and subgrids, collecting values and ensuring that they meet the uniqueness criteria.

### Contributing
Contributions are welcome! If you have suggestions for improvements or new features, please open an issue or submit a pull request.
