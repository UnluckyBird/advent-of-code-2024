using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day12
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day12.1.txt");
            HashSet<(int, int)> allUsed = [];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (allUsed.Contains((i, j)) == false)
                    {
                        HashSet<(int, int)> region = IsRegion(inputs, [], (i, j), inputs[i][j], out int sum);
                        allUsed.UnionWith(region);
                        result += sum * region.Count;
                    }
                }
            }

            return result;
        }

        public static long Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day12.1.txt");
            HashSet<(int, int)> allUsed = [];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (allUsed.Contains((i, j)) == false)
                    {
                        HashSet<(int, int)> region = IsRegionPart2(inputs, [], (i, j), inputs[i][j], Direction.Down, out var edges);
                        allUsed.UnionWith(region);
                        int sum = 0;
                        while (edges.Count > 0)
                        {
                            sum++;
                            (int y, int x, Direction dir) = edges.First();
                            edges.Remove((y, x, dir));
                            if (dir == Direction.Up || dir == Direction.Down)
                            {
                                for (int k = x - 1; k >= 0; k--)
                                {
                                    if (edges.Remove((y, k, dir)) == false)
                                    {
                                        break;
                                    }
                                }
                                for (int k = x + 1; k < inputs.Length; k++)
                                {
                                    if (edges.Remove((y, k, dir)) == false)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int k = y - 1; k >= 0; k--)
                                {
                                    if (edges.Remove((k, x, dir)) == false)
                                    {
                                        break;
                                    }
                                }
                                for (int k = y + 1; k < inputs.Length; k++)
                                {
                                    if (edges.Remove((k, x, dir)) == false)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        result += sum * region.Count;
                    }
                }
            }

            return result;
        }

        private static HashSet<(int, int)> IsRegion(string[] inputs,  HashSet<(int, int)> thisUsed, (int, int) current, char plant, out int sum)
        {
            sum = 1;
            if (current.Item1 >= 0 && current.Item2 >= 0 && current.Item1 < inputs.Length && current.Item2 < inputs[0].Length && inputs[current.Item1][current.Item2] == plant)
            {
                sum = 0;
                if (thisUsed.Add(current))
                {
                    HashSet<(int, int)> set = [current];
                    HashSet<(int, int)> set1 = IsRegion(inputs, thisUsed, (current.Item1 - 1, current.Item2), plant, out int sum1);
                    HashSet<(int, int)> set2 = IsRegion(inputs, thisUsed, (current.Item1 + 1, current.Item2), plant, out int sum2);
                    HashSet<(int, int)> set3 = IsRegion(inputs, thisUsed, (current.Item1, current.Item2 - 1), plant, out int sum3);
                    HashSet<(int, int)> set4 = IsRegion(inputs, thisUsed, (current.Item1, current.Item2 + 1), plant, out int sum4);
                    set.UnionWith(set1);
                    set.UnionWith(set2);
                    set.UnionWith(set3);
                    set.UnionWith(set4);
                    sum = sum1 + sum2 + sum3 + sum4;
                    return set;
                }
            }
            return [];
        }

        private static HashSet<(int, int)> IsRegionPart2(string[] inputs, HashSet<(int, int)> thisUsed, (int, int) current, char plant, Direction dir, out HashSet<(int ,int , Direction)> edges)
        {
            if (current.Item1 >= 0 && current.Item2 >= 0 && current.Item1 < inputs.Length && current.Item2 < inputs[0].Length && inputs[current.Item1][current.Item2] == plant)
            {
                if (thisUsed.Add(current))
                {
                    HashSet<(int, int)> set = [current];
                    HashSet<(int, int)> set1 = IsRegionPart2(inputs, thisUsed, (current.Item1 - 1, current.Item2), plant, Direction.Up, out var edges1);
                    HashSet<(int, int)> set2 = IsRegionPart2(inputs, thisUsed, (current.Item1 + 1, current.Item2), plant, Direction.Down, out var edges2);
                    HashSet<(int, int)> set3 = IsRegionPart2(inputs, thisUsed, (current.Item1, current.Item2 - 1), plant, Direction.Left, out var edges3);
                    HashSet<(int, int)> set4 = IsRegionPart2(inputs, thisUsed, (current.Item1, current.Item2 + 1), plant, Direction.Right, out var edges4);
                    set.UnionWith(set1);
                    set.UnionWith(set2);
                    set.UnionWith(set3);
                    set.UnionWith(set4);
                    edges = edges1;
                    edges.UnionWith(edges2);
                    edges.UnionWith(edges3);
                    edges.UnionWith(edges4);
                    return set;
                }
                edges = [];
            }
            else
            {
                edges = [(current.Item1, current.Item2, dir)];
            }
            return [];
        }

        private enum Direction
        {
            Up, Right, Down, Left
        }
    }
}
