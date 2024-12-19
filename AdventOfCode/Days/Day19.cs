using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day19
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day19.1.txt");
            string[] towels = [.. inputs[0].Split(',', StringSplitOptions.TrimEntries)];
            for (int i = 2; i < inputs.Length; i++)
            {
                if (CheckIfPossible(inputs[i].AsSpan(), towels))
                {
                    result++;
                }
            }
                       
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day19.1.txt");
            string[] towels = [.. inputs[0].Split(',', StringSplitOptions.TrimEntries)];
            for (int i = 2; i < inputs.Length; i++)
            {
                result += CheckIfPossibleAmount(inputs[i].AsSpan(), towels, []);
            }

            return result;
        }

        private static bool CheckIfPossible(ReadOnlySpan<char> design, string[] towels)
        {
            foreach (var towel in towels)
            {
                if (design.StartsWith(towel.AsSpan(), StringComparison.Ordinal))
                {
                    if (towel.Length == design.Length)
                    {
                        return true;
                    }
                    if (CheckIfPossible(design[towel.Length..], towels))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static long CheckIfPossibleAmount(ReadOnlySpan<char> design, string[] towels, Dictionary<int, long> lengthsChecked)
        {
            long res = 0;
            if (lengthsChecked.TryGetValue(design.Length, out long successes))
            {
                res += successes;
            }
            else
            {
                foreach (var towel in towels)
                {
                    if (design.StartsWith(towel.AsSpan(), StringComparison.Ordinal))
                    {
                        if (towel.Length == design.Length)
                        {
                            res += 1;
                        }
                        else if (towel.Length < design.Length)
                        {
                            res += CheckIfPossibleAmount(design[towel.Length..], towels, lengthsChecked);
                        }
                    }
                }
                lengthsChecked.Add(design.Length, res);
            }
            return res;
        }
    }
}
