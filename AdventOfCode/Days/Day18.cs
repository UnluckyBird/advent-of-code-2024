using System;
using System.Collections.Generic;
using System.IO;

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
                var span = inputs[i].AsSpan();
                var split = span.IndexOf(',');
                walls.Add((int.Parse(span.Slice(0, split)), int.Parse(span.Slice(split + 1))));
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
            (int, int)[] allWalls = new (int,int)[inputs.Length];
            HashSet<(int, int)> walls = [];
            HashSet<(int, int)> visited = [];
            (int, int) gridSize = (70, 70);
            
            for (int i = 0; i < inputs.Length; i++)
            {
                var span = inputs[i].AsSpan();
                var split = span.IndexOf(',');
                allWalls[i] = (int.Parse(span[..split]), int.Parse(span.Slice(split + 1)));
            }

            int lower = 0;
            int upper = allWalls.Length - 1;
            PriorityQueue<(int, int), int> queue = new();
            queue.Enqueue((0, 0), 0);
            (int, int) lastFailedWall = (0,0);
            while (lower != upper)
            {
                bool succeeded = false;
                int check = (upper + lower) / 2;
                walls = [..allWalls[..(check + 1)]];
                while (queue.TryDequeue(out var historian, out int priority))
                {
                    if (historian == gridSize)
                    {
                        succeeded = true;
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
                queue.Clear();
                visited.Clear();
                queue.Enqueue((0, 0), 0);
                visited.Add((0, 0));
                if (succeeded)
                {
                    lower = check + 1;
                }
                else
                {
                    upper = check;
                    lastFailedWall = allWalls[check];
                }
            }
            result = $"{lastFailedWall.Item1},{lastFailedWall.Item2}";
            return result;
        }
    }
}
