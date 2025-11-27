using System;

namespace sudoku_board
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int n2 = n * n;
            char[,] grid = new char[n2, n2];
            char[] symbols = new char[n2];
            int symbolsCount = 0;

            // read n^2 rows
            for (int i = 0; i < n2; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length != n2)
                {
                    Console.WriteLine("Invalid number of arguments");
                    Environment.Exit(1);
                }

                // write each character from the row into the grid
                for (int j = 0; j < n2; j++)
                {
                    if (input[j].Length != 1)
                    {
                        Console.WriteLine("Invalid character length");
                        Environment.Exit(1);
                    }

                    char c = Convert.ToChar(input[j]);
                    if (c == '0')
                    {
                        grid[i, j] = c;
                        continue;
                    }

                    if (!ValidPosition(grid, n, i, j, c))
                    {
                        Console.WriteLine("Character already exists in the row, column or box");
                        Environment.Exit(1);
                    }

                    // check if symbol already exists in symbols[]
                    int symb;
                    for (symb = 0; symb < n2; symb++)
                        if (symbols[symb] == c)
                            break;

                    // add symbol
                    if (symb == n2)
                    {
                        if (symbolsCount >= n2)
                        {
                            Console.WriteLine("Too many different characters provided");
                            Environment.Exit(1);
                        }

                        symbols[symbolsCount++] = c;
                    }

                    grid[i, j] = c;
                }
            }

            if (symbolsCount != symbols.Length)
            {
                Console.WriteLine("Too few different characters provided");
                Environment.Exit(1);
            }

            // fill empty spaces in grid
            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    if (grid[i, j] != '0')
                        continue;

                    // attempt each symbol in the current position
                    for (int k = 0; k < symbolsCount; k++)
                    {
                        char c = symbols[k];
                        if (ValidPosition(grid, n, i, j, c))
                        {
                            grid[i, j] = c;
                            break;
                        }
                    }
                }
            }

            // print grid
            Console.WriteLine();
            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        static bool ValidPosition(char[,] grid, int n, int row, int col, char symbol)
        {
            int n2 = n * n;
            // check if the symbol already exists in the row
            for (int k = 0; k < n2; k++)
            {
                if (grid[row, k] == symbol)
                {
                    return false;
                }
            }

            // check if the symbol already exists in the column
            for (int k = 0; k < n2; k++)
            {
                if (grid[k, col] == symbol)
                {
                    return false;
                }
            }

            // find box position
            int boxX = row / n;
            int boxY = col / n;

            // find box starting position
            int boxStartX = boxX * n;
            int boxStartY = boxY * n;

            // check if the symbol already exists in the box
            for (int k = boxStartX; k < boxStartY + n; k++)
            {
                for (int l = boxStartY; l < boxStartY + n; l++)
                {
                    if (grid[k, l] == symbol)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
