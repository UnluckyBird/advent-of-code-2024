using System;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day4
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day4.1.txt");

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == 'X')
                    {
                        if (CheckChar(inputs, i, j+1, 'M') && CheckChar(inputs, i, j + 2, 'A') && CheckChar(inputs, i, j + 3, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i + 1, j + 1, 'M') && CheckChar(inputs, i+2, j + 2, 'A') && CheckChar(inputs, i+3, j + 3, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i+1, j, 'M') && CheckChar(inputs, i+2, j, 'A') && CheckChar(inputs, i+3, j, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i+1, j -1, 'M') && CheckChar(inputs, i+2, j -2, 'A') && CheckChar(inputs, i+3, j -3, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i, j - 1, 'M') && CheckChar(inputs, i, j - 2, 'A') && CheckChar(inputs, i, j - 3, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i-1, j - 1, 'M') && CheckChar(inputs, i-2, j - 2, 'A') && CheckChar(inputs, i-3, j - 3, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i-1, j, 'M') && CheckChar(inputs, i-2, j, 'A') && CheckChar(inputs, i-3 , j, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i-1, j + 1, 'M') && CheckChar(inputs, i-2, j + 2, 'A') && CheckChar(inputs, i-3, j + 3, 'S'))
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day4.1.txt");

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == 'A')
                    {
                        if (CheckChar(inputs, i - 1, j - 1, 'M') && CheckChar(inputs, i - 1, j + 1, 'M') && CheckChar(inputs, i + 1, j - 1, 'S') && CheckChar(inputs, i+1, j + 1, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i - 1, j + 1, 'M') && CheckChar(inputs, i + 1, j + 1, 'M') && CheckChar(inputs, i + 1, j - 1, 'S') && CheckChar(inputs, i - 1, j - 1, 'S'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i - 1, j - 1, 'S') && CheckChar(inputs, i - 1, j + 1, 'S') && CheckChar(inputs, i + 1, j - 1, 'M') && CheckChar(inputs, i + 1, j + 1, 'M'))
                        {
                            result++;
                        }
                        if (CheckChar(inputs, i - 1, j + 1, 'S') && CheckChar(inputs, i + 1, j + 1, 'S') && CheckChar(inputs, i + 1, j - 1, 'M') && CheckChar(inputs, i - 1, j - 1, 'M'))
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        private static bool CheckChar(string[] input, int i, int j, char check)
        {
            if (i >= 0 && i < input.Length && j >= 0 && j < input[i].Length)
            {
                if (input[i][j] == check)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
