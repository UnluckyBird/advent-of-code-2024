using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day2
    {
        public static int Part1()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day2.1.txt");

            foreach (string input in inputs)
            {
                List<int> levels = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                if (CheckReport(levels, checkSubsequnce: false))
                {
                    result++;
                }
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day2.1.txt");

            foreach (string input in inputs)
            {
                List<int> levels = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                if (CheckReport(levels, checkSubsequnce: true))
                {
                    result++;
                }
            }

            return result;
        }

        private static bool CheckReport(List<int> levels, bool checkSubsequnce = false)
        {
            bool ascending = levels[0] < levels[1];
            for (int i = 1; i < levels.Count; i++)
            {
                if (ascending && levels[i] - levels[i - 1] < 1 || levels[i] - levels[i - 1] > 3)
                {
                    if (checkSubsequnce)
                    {
                        List<List<int>> subsequences = [];
                        for (int j = 0; j < levels.Count; j++)
                        {
                            var list = levels.ToList();
                            list.RemoveAt(j);
                            subsequences.Add(list);
                        }
                        return subsequences.Any(s => CheckReport(s));
                    }
                    return false;
                }
                if (!ascending && levels[i - 1] - levels[i] < 1 || levels[i - 1] - levels[i] > 3)
                {
                    if (checkSubsequnce)
                    {
                        List<List<int>> subsequences = [];
                        for (int j = 0; j < levels.Count; j++)
                        {
                            var list = levels.ToList();
                            list.RemoveAt(j);
                            subsequences.Add(list);
                        }
                        return subsequences.Any(s => CheckReport(s));
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
