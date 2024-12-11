using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day1
    {
        public static int Part1()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day1.1.txt");

            List<int> first = [];
            List<int> second = [];
            foreach (string input in inputs)
            {
                var ids = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                first.Add(int.Parse(ids[0]));
                second.Add(int.Parse(ids[1]));
            }
            first.Sort();
            second.Sort();
            result = first.Zip(second).Sum(x => Math.Abs(x.First - x.Second));

            return result;
        }

        public static int Part2()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day1.1.txt");

            List<int> first = [];
            List<int> second = [];
            foreach (string input in inputs)
            {
                var ids = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                first.Add(int.Parse(ids[0]));
                second.Add(int.Parse(ids[1]));
            }
            var dict = second.GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count());
            first.ForEach(x => result += x * (dict.TryGetValue(x, out int value) ? value : 0));

            return result;
        }
    }
}
