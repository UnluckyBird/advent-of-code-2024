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
            HashSet<(int, int)> visited = [];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (visited.Contains((i, j)) == false)
                    {
                        int edges = 0;
                        char plant = inputs[i][j];
                        HashSet<(int, int)> region = [(i, j)];
                        Queue<(int, int)> queue = new();
                        queue.Enqueue((i, j));
                        while (queue.Count > 0)
                        {
                            (int y, int x) = queue.Dequeue();
                            if (y-1 < 0 || x < 0 || y-1 >= inputs.Length || x >= inputs[0].Length || inputs[y-1][x] != plant)
                            {
                                edges++;
                            }
                            else
                            {
                                if (region.Add((y - 1, x)))
                                {
                                    queue.Enqueue((y - 1, x));
                                }
                            }
                            if (y + 1 < 0 || x < 0 || y + 1 >= inputs.Length || x >= inputs[0].Length || inputs[y + 1][x] != plant)
                            {
                                edges++;
                            }
                            else
                            {
                                if (region.Add((y + 1, x)))
                                {
                                    queue.Enqueue((y + 1, x));
                                }
                            }
                            if (y < 0 || x-1 < 0 || y >= inputs.Length || x-1 >= inputs[0].Length || inputs[y][x-1] != plant)
                            {
                                edges++;
                            }
                            else
                            {
                                if (region.Add((y, x-1)))
                                {
                                    queue.Enqueue((y, x-1));
                                }
                            }
                            if (y < 0 || x+1 < 0 || y >= inputs.Length || x+1 >= inputs[0].Length || inputs[y][x+1] != plant)
                            {
                                edges++;
                            }
                            else
                            {
                                if (region.Add((y, x+1)))
                                {
                                    queue.Enqueue((y, x+1));
                                }
                            }
                        }
                        visited.UnionWith(region);
                        result += edges * region.Count;
                    }
                }
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day12.1.txt");
            HashSet<(int, int)> visited = [];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (visited.Contains((i, j)) == false)
                    {
                        HashSet<(int, int, Direction)> edges = [];
                        char plant = inputs[i][j];
                        HashSet<(int, int)> region = [(i, j)];
                        Queue<(int, int)> queue = new();
                        queue.Enqueue((i, j));
                        while (queue.Count > 0)
                        {
                            (int y, int x) = queue.Dequeue();
                            if (y - 1 < 0 || x < 0 || y - 1 >= inputs.Length || x >= inputs[0].Length || inputs[y - 1][x] != plant)
                            {
                                edges.Add((y - 1, x, Direction.Up));
                            }
                            else
                            {
                                if (region.Add((y - 1, x)))
                                {
                                    queue.Enqueue((y - 1, x));
                                }
                            }
                            if (y + 1 < 0 || x < 0 || y + 1 >= inputs.Length || x >= inputs[0].Length || inputs[y + 1][x] != plant)
                            {
                                edges.Add((y + 1, x, Direction.Down));
                            }
                            else
                            {
                                if (region.Add((y + 1, x)))
                                {
                                    queue.Enqueue((y + 1, x));
                                }
                            }
                            if (y < 0 || x - 1 < 0 || y >= inputs.Length || x - 1 >= inputs[0].Length || inputs[y][x - 1] != plant)
                            {
                                edges.Add((y, x - 1, Direction.Left));
                            }
                            else
                            {
                                if (region.Add((y, x - 1)))
                                {
                                    queue.Enqueue((y, x - 1));
                                }
                            }
                            if (y < 0 || x + 1 < 0 || y >= inputs.Length || x + 1 >= inputs[0].Length || inputs[y][x + 1] != plant)
                            {
                                edges.Add((y, x + 1, Direction.Right));
                            }
                            else
                            {
                                if (region.Add((y, x + 1)))
                                {
                                    queue.Enqueue((y, x + 1));
                                }
                            }
                        }
                        visited.UnionWith(region);
                        int edgesSum = 0;
                        while (edges.Count > 0)
                        {
                            edgesSum++;
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
                        result += edgesSum * region.Count;
                    }
                }
            }

            return result;
        }

        private enum Direction
        {
            Up, Right, Down, Left
        }
    }
}
