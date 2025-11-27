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

                    // check if the symbol already exists in the row
                    for (int k = 0; k < n2; k++)
                    {
                        if (grid[i, k] == c)
                        {
                            Console.WriteLine("Character already exists in the row");
                            Environment.Exit(1);
                        }
                    }

                    // check if the symbol already exists in the column
                    for (int k = 0; k < n2; k++)
                    {
                        if (grid[k, j] == c)
                        {
                            Console.WriteLine("Character already exists in the column");
                            Environment.Exit(1);
                        }
                    }

                    // find box position
                    int boxX = i / n;
                    int boxY = j / n;

                    // find box starting position
                    int boxStartX = boxX * n;
                    int boxStartY = boxY * n;

                    // check if the symbol already exists in the box
                    for (int k = boxStartX; k < boxStartY + n; k++)
                    {
                        for (int l = boxStartY; l < boxStartY + n; l++)
                        {
                            if (grid[k, l] == c)
                            {
                                Console.WriteLine("Character already exists in the box");
                                Environment.Exit(1);
                            }
                        }
                    }

                    grid[i, j] = c;
                }
            }
            
            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
