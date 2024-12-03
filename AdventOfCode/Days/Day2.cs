using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day2
    {
        public static int Part1()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day2.1.txt");

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

            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day2.1.txt");

            List<(List<int>, List<int>)> retryLists = [];
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
