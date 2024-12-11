using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day8
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day8.1.txt");
            Dictionary<char, List<(int, int)>> antennas = [];
            HashSet<(int, int)> antinodes = [];
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] != '.')
                    {
                        if (antennas.TryGetValue(inputs[i][j], out var value))
                        {
                            value.Add((i, j));
                        }
                        else
                        {
                            antennas.Add(inputs[i][j], [(i, j)]);
                        }
                    }
                }
            }
            foreach (var antenna in antennas.Values)
            {
                for (int i = 0; i < antenna.Count; i++)
                {
                    for (int j = i+1; j < antenna.Count; j++)
                    {
                        var antinode = (antenna[i].Item1 + (antenna[i].Item1 - antenna[j].Item1), antenna[i].Item2 + (antenna[i].Item2 - antenna[j].Item2));
                        if (antinode.Item1 >= 0 && antinode.Item2 >= 0 && antinode.Item1 < inputs.Length && antinode.Item2 < inputs[0].Length)
                        {
                            antinodes.Add(antinode);
                        }

                        antinode = (antenna[j].Item1 + (antenna[j].Item1 - antenna[i].Item1), antenna[j].Item2 + (antenna[j].Item2 - antenna[i].Item2));
                        if (antinode.Item1 >= 0 && antinode.Item2 >= 0 && antinode.Item1 < inputs.Length && antinode.Item2 < inputs[0].Length)
                        {
                            antinodes.Add(antinode);
                        }
                    }
                }
            }

            result = antinodes.Count;
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day8.1.txt");
            Dictionary<char, List<(int, int)>> antennas = [];
            HashSet<(int, int)> antinodes = [];
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] != '.')
                    {
                        if (antennas.TryGetValue(inputs[i][j], out var value))
                        {
                            value.Add((i, j));
                        }
                        else
                        {
                            antennas.Add(inputs[i][j], [(i, j)]);
                        }
                    }
                }
            }
            foreach (var antenna in antennas.Values)
            {
                for (int i = 0; i < antenna.Count; i++)
                {
                    for (int j = i + 1; j < antenna.Count; j++)
                    {
                        int gcd = GCD(Math.Abs(antenna[i].Item1 - antenna[j].Item1), Math.Abs(antenna[i].Item2 - antenna[j].Item2));
                        var antinode = antenna[i];
                        while (antinode.Item1 >= 0 && antinode.Item2 >= 0 && antinode.Item1 < inputs.Length && antinode.Item2 < inputs[0].Length)
                        {
                            
                            antinodes.Add(antinode);
                            antinode = (antinode.Item1 + (antenna[i].Item1 - antenna[j].Item1)/gcd, antinode.Item2 + (antenna[i].Item2 - antenna[j].Item2)/gcd);
                        }

                        antinode = antenna[j];
                        while (antinode.Item1 >= 0 && antinode.Item2 >= 0 && antinode.Item1 < inputs.Length && antinode.Item2 < inputs[0].Length)
                        {
                            antinodes.Add(antinode);
                            antinode = (antinode.Item1 + (antenna[j].Item1 - antenna[i].Item1)/gcd, antinode.Item2 + (antenna[j].Item2 - antenna[i].Item2)/gcd);
                        }
                    }
                }
            }

            result = antinodes.Count;
            return result;
        }

        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

    }
}
