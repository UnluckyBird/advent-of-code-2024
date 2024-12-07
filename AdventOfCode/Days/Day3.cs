using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days
{
    public class Day3
    {
        public static int Part1()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day3.1.txt");

            foreach (string input in inputs)
            {
                List<int> indexes = [];
                List<string> matches = Regex.Matches(input, @"mul\([0-9]+,[0-9]+\)")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToList();
                foreach (string match in matches)
                {
                    List<int> values = match[4..^1].Split(',').Select(int.Parse).ToList();
                    result += values[0] * values[1];
                }
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;

            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day3.2.txt");

            bool applyMult = true;
            foreach (string input in inputs)
            {
                List<int> indexes = [];
                List<string> matches = Regex.Matches(input, @"(mul\([0-9]+,[0-9]+\)|do\(\)|don't\(\))")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToList();
                foreach (string match in matches)
                {
                    if (match == "do()")
                    {
                        applyMult = true;
                    }
                    else if (match == "don't()")
                    {
                        applyMult = false;
                    }
                    else
                    {
                        List<int> values = match[4..^1].Split(',').Select(int.Parse).ToList();
                        if (applyMult)
                        {
                            result += values[0] * values[1];
                        }
                    }
                }
            }

            return result;
        }
    }
}
