using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day18
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day18.1.txt");
            HashSet<(int, int)> walls = [];
            HashSet<(int, int)> visited = [];
            PriorityQueue<(int, int), int> queue = new();
            queue.Enqueue((0, 0), 0);
            (int, int) gridSize = (70, 70);

            for (int i = 0; i < 1024; i++)
            {
                List<int> wall = inputs[i].Split(',').Select(int.Parse).ToList();
                walls.Add((wall[0], wall[1]));
            }

            while (queue.TryDequeue(out var historian, out int priority))
            {
                if (historian == gridSize)
                {
                    result = priority;
                    break;
                }
                else if (historian.Item1 < 0 || historian.Item2 < 0 || historian.Item1 > gridSize.Item1 || historian.Item2 > gridSize.Item2)
                {
                    continue;
                }

                if (walls.Contains((historian.Item1 - 1, historian.Item2)) == false)
                {
                    if (visited.Contains((historian.Item1 - 1, historian.Item2)) == false)
                    {
                        visited.Add((historian.Item1 - 1, historian.Item2));
                        queue.Enqueue((historian.Item1 - 1, historian.Item2), priority + 1);
                    }
                }

                if (walls.Contains((historian.Item1 + 1, historian.Item2)) == false)
                {
                    if (visited.Contains((historian.Item1 + 1, historian.Item2)) == false)
                    {
                        visited.Add((historian.Item1 + 1, historian.Item2));
                        queue.Enqueue((historian.Item1 + 1, historian.Item2), priority + 1);
                    }
                }


                if (walls.Contains((historian.Item1, historian.Item2 - 1)) == false)
                {
                    if (visited.Contains((historian.Item1, historian.Item2 - 1)) == false)
                    {
                        visited.Add((historian.Item1, historian.Item2 - 1));
                        queue.Enqueue((historian.Item1, historian.Item2 - 1), priority + 1);
                    }
                }

                if (walls.Contains((historian.Item1, historian.Item2 + 1)) == false)
                {
                    if (visited.Contains((historian.Item1, historian.Item2 + 1)) == false)
                    {
                        visited.Add((historian.Item1, historian.Item2 + 1));
                        queue.Enqueue((historian.Item1, historian.Item2 + 1), priority + 1);
                    }
                }
            }

            return result;
        }

        public static string Part2()
        {
            string result = "";
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day18.1.txt");
            HashSet<(int, int)> walls = [];
            Queue<(int, int)> unusedWalls = [];
            HashSet<(int, int)> visited = [];
            PriorityQueue<(int, int, HashSet<(int, int)>), int> queue = new();
            queue.Enqueue((0, 0, []), 0);
            (int, int) gridSize = (70, 70);
            int startFill = 1024;

            for (int i = 0; i < startFill; i++)
            {
                List<int> wall = inputs[i].Split(',').Select(int.Parse).ToList();
                walls.Add((wall[0], wall[1]));
            }
            for (int i = startFill; i < inputs.Length; i++)
            {
                List<int> wall = inputs[i].Split(',').Select(int.Parse).ToList();
                unusedWalls.Enqueue((wall[0], wall[1]));
            }

            (int, int) lastUsedWall = (0,0);
            while (queue.TryDequeue(out var historian, out int priority))
            {
                if (historian.Item1 == gridSize.Item1 && historian.Item2 == gridSize.Item2)
                {
                    do
                    {
                        lastUsedWall = unusedWalls.Dequeue();
                        walls.Add(lastUsedWall);
                    } while (historian.Item3.Contains(lastUsedWall) == false);
                    visited.Clear();
                    queue.Clear();
                    queue.Enqueue((0, 0, []), 0);
                    continue;
                }
                else if (historian.Item1 < 0 || historian.Item2 < 0 || historian.Item1 > gridSize.Item1 || historian.Item2 > gridSize.Item2)
                {
                    continue;
                }

                if (walls.Contains((historian.Item1 - 1, historian.Item2)) == false)
                {
                    if (visited.Contains((historian.Item1 - 1, historian.Item2)) == false)
                    {
                        visited.Add((historian.Item1 - 1, historian.Item2));
                        queue.Enqueue((historian.Item1 - 1, historian.Item2, [.. historian.Item3, (historian.Item1, historian.Item2)]), priority + 1);
                    }
                }

                if (walls.Contains((historian.Item1 + 1, historian.Item2)) == false)
                {
                    if (visited.Contains((historian.Item1 + 1, historian.Item2)) == false)
                    {
                        visited.Add((historian.Item1 + 1, historian.Item2));
                        queue.Enqueue((historian.Item1 + 1, historian.Item2, [.. historian.Item3, (historian.Item1, historian.Item2)]), priority + 1);
                    }
                }


                if (walls.Contains((historian.Item1, historian.Item2 - 1)) == false)
                {
                    if (visited.Contains((historian.Item1, historian.Item2 - 1)) == false)
                    {
                        visited.Add((historian.Item1, historian.Item2 - 1));
                        queue.Enqueue((historian.Item1, historian.Item2 - 1, [.. historian.Item3, (historian.Item1, historian.Item2)]), priority + 1);
                    }
                }

                if (walls.Contains((historian.Item1, historian.Item2 + 1)) == false)
                {
                    if (visited.Contains((historian.Item1, historian.Item2 + 1)) == false)
                    {
                        visited.Add((historian.Item1, historian.Item2 + 1));
                        queue.Enqueue((historian.Item1, historian.Item2 + 1, [.. historian.Item3, (historian.Item1, historian.Item2)]), priority + 1);
                    }
                }
            }
            result = $"{lastUsedWall.Item1},{lastUsedWall.Item2}";
            return result;
        }
    }
}
