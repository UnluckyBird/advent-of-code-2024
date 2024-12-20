using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day20
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day20.1.txt");
            HashSet<(int, int)> road = [];
            OrderedDictionary<(int, int), int> visited = [];
            (int, int) position = (0, 0);
            (int, int) finish = (0, 0);

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '.')
                    {
                        road.Add((i,j));
                    }
                    else if (inputs[i][j] == 'S')
                    {
                        road.Add((i, j));
                        position = (i, j);
                        visited.Add(position, 0);
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        road.Add((i, j));
                        finish = (i, j);
                    }
                }
            }

            int time = 1;
            while (position != finish)
            {
                if (road.Contains((position.Item1 - 1, position.Item2)) && visited.ContainsKey((position.Item1 - 1, position.Item2)) == false)
                {
                    position.Item1 -= 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1 + 1, position.Item2)) && visited.ContainsKey((position.Item1 + 1, position.Item2)) == false)
                {
                    position.Item1 += 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1, position.Item2 - 1)) && visited.ContainsKey((position.Item1, position.Item2 - 1)) == false)
                {
                    position.Item2 -= 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1, position.Item2 + 1)) && visited.ContainsKey((position.Item1, position.Item2 + 1)) == false)
                {
                    position.Item2 += 1;
                    visited.Add(position, time++);
                }
            }

            List<int> afterCheats = [];
            foreach (var pair in visited)
            {
                if (road.Contains((pair.Key.Item1 - 1, pair.Key.Item2)) == false && road.Contains((pair.Key.Item1 - 2, pair.Key.Item2)) && visited.TryGetValue((pair.Key.Item1 - 2, pair.Key.Item2), out int upValue))
                {
                    afterCheats.Add(upValue - pair.Value - 2);
                }
                if (road.Contains((pair.Key.Item1 + 1, pair.Key.Item2)) == false && road.Contains((pair.Key.Item1 + 2, pair.Key.Item2)) && visited.TryGetValue((pair.Key.Item1 + 2, pair.Key.Item2), out int downValue))
                {
                    afterCheats.Add(downValue - pair.Value - 2);
                }
                if (road.Contains((pair.Key.Item1, pair.Key.Item2 - 1)) == false && road.Contains((pair.Key.Item1, pair.Key.Item2 - 2)) && visited.TryGetValue((pair.Key.Item1, pair.Key.Item2 - 2), out int leftValue))
                {
                    afterCheats.Add(leftValue - pair.Value - 2);
                }
                if (road.Contains((pair.Key.Item1, pair.Key.Item2 + 1)) == false && road.Contains((pair.Key.Item1, pair.Key.Item2 + 2)) && visited.TryGetValue((pair.Key.Item1, pair.Key.Item2 + 2), out int rightValue))
                {
                    afterCheats.Add(rightValue - pair.Value - 2);
                }
                road.Remove(pair.Key);
            }
            result = afterCheats.Count(c => c >= 100);
            return result;
        }

        public static long Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day20.1.txt");
            HashSet<(int, int)> road = [];
            OrderedDictionary<(int, int), int> visited = [];
            (int, int) position = (0, 0);
            (int, int) finish = (0, 0);
            int cheatCutoff = 100;
            
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '.')
                    {
                        road.Add((i, j));
                    }
                    else if (inputs[i][j] == 'S')
                    {
                        road.Add((i, j));
                        position = (i, j);
                        visited.Add(position, 0);
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        road.Add((i, j));
                        finish = (i, j);
                    }
                }
            }
            
            int time = 1;
            while (position != finish)
            {
                if (road.Contains((position.Item1 - 1, position.Item2)) && visited.ContainsKey((position.Item1 - 1, position.Item2)) == false)
                {
                    position.Item1 -= 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1 + 1, position.Item2)) && visited.ContainsKey((position.Item1 + 1, position.Item2)) == false)
                {
                    position.Item1 += 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1, position.Item2 - 1)) && visited.ContainsKey((position.Item1, position.Item2 - 1)) == false)
                {
                    position.Item2 -= 1;
                    visited.Add(position, time++);
                }
                else if (road.Contains((position.Item1, position.Item2 + 1)) && visited.ContainsKey((position.Item1, position.Item2 + 1)) == false)
                {
                    position.Item2 += 1;
                    visited.Add(position, time++);
                }
            }

            for (int i = 0; i < visited.Count - 1; i++)
            {
                var current = visited.GetAt(i);
                for (int j = i + cheatCutoff; j < visited.Count; j++)
                {
                    var next = visited.GetAt(j);
                    int distance = Math.Abs(current.Key.Item1 - next.Key.Item1) + Math.Abs(current.Key.Item2 - next.Key.Item2);
                    if (distance <= 20)
                    {
                        if (next.Value - current.Value - distance >= cheatCutoff)
                        {
                            result++;
                        }
                    }
                    else
                    {
                        j += distance - 21;
                    }
                }
            }
            return result;
        }
    }
}
