using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day25
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day25.1.txt");
            HashSet<int[]> locks = [];
            HashSet<int[]> keys = [];

            for (int i = 0; i < inputs.Length; i += 8)
            {
                int[] item = [0, 0, 0, 0, 0];
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 1; k < 6; k++)
                    {
                        item[j] += inputs[i + k][j] == '#' ? 1 : 0;
                    }
                }
                if (inputs[i][0] == '#')
                {
                    locks.Add(item);
                }
                else
                {
                    keys.Add(item);
                }
            }

            foreach (int[] item in locks)
            {
                foreach (int[] key in keys)
                {
                    bool fits = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (item[i] + key[i] > 5)
                        {
                            fits = false;
                        }
                    }
                    result += fits ? 1 : 0;
                }
            }

            return result;
        }

        public static string Part2()
        {
            string result = "Successfully delivered the chronicle, and completed Advent of Code 2024";
            return result;
        }
    }
}
