using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day21
    {
        private static readonly Dictionary<char, (int, int)> numpad = new()
        {
            ['7'] = (0, 0),
            ['8'] = (1, 0),
            ['9'] = (2, 0),
            ['4'] = (0, 1),
            ['5'] = (1, 1),
            ['6'] = (2, 1),
            ['1'] = (0, 2),
            ['2'] = (1, 2),
            ['3'] = (2, 2),
            ['0'] = (1, 3),
            ['A'] = (2, 3),
        };
        private static readonly Dictionary<(char, char), string> keypad = new()
        {
            [('^', '^')] = "A",
            [('^', 'A')] = ">A",
            [('^', '<')] = "v<A",
            [('^', 'v')] = "vA",
            [('^', '>')] = "v>A",
            [('A', 'A')] = "A",
            [('A', '^')] = "<A",
            [('A', '>')] = "vA",
            [('A', 'v')] = "<vA",
            [('A', '<')] = "v<<A",
            [('<', '<')] = "A",
            [('<', '^')] = ">^A",
            [('<', 'v')] = ">A",
            [('<', '>')] = ">>A",
            [('<', 'A')] = ">>^A",
            [('v', 'v')] = "A",
            [('v', '^')] = "^A",
            [('v', '<')] = "<A",
            [('v', '>')] = ">A",
            [('v', 'A')] = "^>A",
            [('>', '>')] = "A",
            [('>', 'A')] = "^A",
            [('>', 'v')] = "<A",
            [('>', '<')] = "<<A",
            [('>', '^')] = "<^A",
        };

        public static long Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day21.1.txt");

            Dictionary<(char, char, int), long> memory = new()
            {
                [('^', '^', 1)] = 1,
                [('^', 'A', 1)] = 2,
                [('^', '<', 1)] = 3,
                [('^', 'v', 1)] = 2,
                [('^', '>', 1)] = 3,
                [('A', 'A', 1)] = 1,
                [('A', '^', 1)] = 2,
                [('A', '>', 1)] = 2,
                [('A', 'v', 1)] = 3,
                [('A', '<', 1)] = 4,
                [('<', '<', 1)] = 1,
                [('<', '^', 1)] = 3,
                [('<', 'v', 1)] = 2,
                [('<', '>', 1)] = 3,
                [('<', 'A', 1)] = 4,
                [('v', 'v', 1)] = 1,
                [('v', '^', 1)] = 2,
                [('v', '<', 1)] = 2,
                [('v', '>', 1)] = 2,
                [('v', 'A', 1)] = 3,
                [('>', '>', 1)] = 1,
                [('>', 'A', 1)] = 2,
                [('>', 'v', 1)] = 2,
                [('>', '<', 1)] = 3,
                [('>', '^', 1)] = 3,
            };

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = 'A' + inputs[i];
                List<string> paths = [""];
                for (int j = 1; j < inputs[i].Length; j++)
                {
                    var start = numpad[inputs[i][j - 1]];
                    var end = numpad[inputs[i][j]];
                    List<string> shortPaths = ToNumpadEnd([(0, 3), start], start, end, "");
                    int minLength = shortPaths.Min(p => p.Length);
                    shortPaths = shortPaths.Where(p => p.Length == minLength).ToList();

                    List<string> newPaths = [];
                    paths.ForEach(p => shortPaths.ForEach(sp => newPaths.Add(p + sp)));
                    paths = newPaths;
                }

                List<long> lengths = [];
                foreach (string path in paths)
                {
                    long shortPath = goDeeper('A', path[0], 0, 2, memory);
                    for (int j =1; j<path.Length; j++)
                    {
                        shortPath += goDeeper(path[j-1], path[j], 0, 2, memory);
                    }
                    lengths.Add(shortPath);
                }
                
                result += lengths.Min() * int.Parse(inputs[i][1..^1]);
            }
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day21.1.txt");

            Dictionary<(char, char, int), long> memory = new()
            {
                [('^', '^', 1)] = 1,
                [('^', 'A', 1)] = 2,
                [('^', '<', 1)] = 3,
                [('^', 'v', 1)] = 2,
                [('^', '>', 1)] = 3,
                [('A', 'A', 1)] = 1,
                [('A', '^', 1)] = 2,
                [('A', '>', 1)] = 2,
                [('A', 'v', 1)] = 3,
                [('A', '<', 1)] = 4,
                [('<', '<', 1)] = 1,
                [('<', '^', 1)] = 3,
                [('<', 'v', 1)] = 2,
                [('<', '>', 1)] = 3,
                [('<', 'A', 1)] = 4,
                [('v', 'v', 1)] = 1,
                [('v', '^', 1)] = 2,
                [('v', '<', 1)] = 2,
                [('v', '>', 1)] = 2,
                [('v', 'A', 1)] = 3,
                [('>', '>', 1)] = 1,
                [('>', 'A', 1)] = 2,
                [('>', 'v', 1)] = 2,
                [('>', '<', 1)] = 3,
                [('>', '^', 1)] = 3,
            };

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = 'A' + inputs[i];
                List<string> paths = [""];
                for (int j = 1; j < inputs[i].Length; j++)
                {
                    var start = numpad[inputs[i][j - 1]];
                    var end = numpad[inputs[i][j]];
                    List<string> shortPaths = ToNumpadEnd([(0, 3), start], start, end, "");
                    int minLength = shortPaths.Min(p => p.Length);
                    shortPaths = shortPaths.Where(p => p.Length == minLength).ToList();

                    List<string> newPaths = [];
                    paths.ForEach(p => shortPaths.ForEach(sp => newPaths.Add(p + sp)));
                    paths = newPaths;
                }

                List<long> lengths = [];
                foreach (string path in paths)
                {
                    long shortPath = goDeeper('A', path[0], 0, 25, memory);
                    for (int j = 1; j < path.Length; j++)
                    {
                        shortPath += goDeeper(path[j - 1], path[j], 0, 25, memory);
                    }
                    lengths.Add(shortPath);
                }

                result += lengths.Min() * int.Parse(inputs[i][1..^1]);
            }
            return result;
        }

        private static List<string> ToNumpadEnd(HashSet<(int, int)> visited, (int, int) start, (int, int) end, string path)
        {
            if (start == end)
            {
                return [path + 'A'];
            }
            List<string> res = [];
            if (start.Item1 >= 0 && start.Item1 < 3 && start.Item2 >= 0 && start.Item2 < 4 && path.Length <= 5) {
                if (visited.Contains((start.Item1 - 1, start.Item2)) == false) {
                    res.AddRange(ToNumpadEnd([.. visited, (start.Item1 - 1, start.Item2)], (start.Item1 - 1, start.Item2), end, path + '<'));
                }
                if (visited.Contains((start.Item1 + 1, start.Item2)) == false)
                {
                    res.AddRange(ToNumpadEnd([.. visited, (start.Item1 + 1, start.Item2)], (start.Item1 + 1, start.Item2), end, path + '>'));
                }
                if (visited.Contains((start.Item1, start.Item2 - 1)) == false)
                {
                    res.AddRange(ToNumpadEnd([.. visited, (start.Item1, start.Item2 - 1)], (start.Item1, start.Item2 - 1), end, path + '^'));
                }
                if (visited.Contains((start.Item1, start.Item2 + 1)) == false)
                {
                    res.AddRange(ToNumpadEnd([.. visited, (start.Item1, start.Item2 + 1)], (start.Item1, start.Item2 + 1), end, path + 'v'));
                }
            }
            return res;
        }

        private static long goDeeper(char first, char second, int depth, int end, Dictionary<(char, char, int), long> memory)
        {
            long res = 0;
            if (memory.TryGetValue((first, second, end - depth), out var value))
            {
                res = value;
            }
            else
            {
                var check = keypad[(first, second)];
                res += goDeeper('A', check[0], depth + 1, end, memory);
                for (int i = 1; i < check.Length; i++)
                {
                    res += goDeeper(check[i - 1], check[i], depth + 1, end, memory);
                }

                memory[(first, second, end - depth)] = res;
            }

            return res;
        }
    }
}
