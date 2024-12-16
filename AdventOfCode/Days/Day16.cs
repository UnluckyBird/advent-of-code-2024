using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day16
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day16.1.txt");
            HashSet<(int, int)> roads = [];
            Dictionary<(int, int), (int, Direction)> visited = [];
            (int, int) start = (0, 0);
            (int, int) finish = (0, 0);

            for (int i = 1; i < inputs.Length-1; i++)
            {
                for (int j = 1; j < inputs[i].Length-1; j++)
                {
                    if (inputs[i][j] == '.')
                    {
                        roads.Add((i,j));
                    }
                    else if (inputs[i][j] == 'S')
                    {
                        start = (i, j);
                        roads.Add(start);
                        visited.Add((i, j), (0, Direction.Right));
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        finish = (i, j);
                        roads.Add((i, j));
                    }
                }
            }

            while (true)
            {
                var reindeer = visited.Where(x => roads.Contains(x.Key)).MinBy(x => x.Value.Item1);
                if (reindeer.Key == finish)
                {
                    result = reindeer.Value.Item1;
                    break;
                }
                int addToScore = 1;
                if (roads.Contains((reindeer.Key.Item1-1, reindeer.Key.Item2)))
                {
                    addToScore = reindeer.Value.Item2 != Direction.Up ? 1001 : 1;
                    if (visited.TryGetValue((reindeer.Key.Item1 - 1, reindeer.Key.Item2), out var value))
                    {
                        if (value.Item1 > reindeer.Value.Item1 + addToScore)
                        {
                            visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2)] = (reindeer.Value.Item1 + addToScore, Direction.Up);
                        }
                    }
                    else
                    {
                        visited.Add((reindeer.Key.Item1 - 1, reindeer.Key.Item2), (reindeer.Value.Item1 + addToScore, Direction.Up));
                    }
                }
                if (roads.Contains((reindeer.Key.Item1 + 1, reindeer.Key.Item2)))
                {
                    addToScore = reindeer.Value.Item2 != Direction.Down ? 1001 : 1;
                    if (visited.TryGetValue((reindeer.Key.Item1 + 1, reindeer.Key.Item2), out var value))
                    {
                        if (value.Item1 > reindeer.Value.Item1 + addToScore)
                        {
                            visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2)] = (reindeer.Value.Item1 + addToScore, Direction.Down);
                        }
                    }
                    else
                    {
                        visited.Add((reindeer.Key.Item1 + 1, reindeer.Key.Item2), (reindeer.Value.Item1 + addToScore, Direction.Down));
                    }
                }
                if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 - 1)))
                {
                    addToScore = reindeer.Value.Item2 != Direction.Left ? 1001 : 1;
                    if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 - 1), out var value))
                    {
                        if (value.Item1 > reindeer.Value.Item1 + addToScore)
                        {
                            visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1)] = (reindeer.Value.Item1 + addToScore, Direction.Left);
                        }
                    }
                    else
                    {
                        visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 - 1), (reindeer.Value.Item1 + addToScore, Direction.Left));
                    }
                }
                if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 + 1)))
                {
                    addToScore = reindeer.Value.Item2 != Direction.Right ? 1001 : 1;
                    if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 + 1), out var value))
                    {
                        if (value.Item1 > reindeer.Value.Item1 + addToScore)
                        {
                            visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1)] = (reindeer.Value.Item1 + addToScore, Direction.Right);
                        }
                    }
                    else
                    {
                        visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 + 1), (reindeer.Value.Item1 + addToScore, Direction.Right));
                    }
                }
                roads.Remove(reindeer.Key);
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day16.1.txt");
            HashSet<(int, int, Direction)> roads = [];
            Dictionary<(int, int, Direction), (int, HashSet<(int, int)> path)> visited = [];
            (int, int) start = (0, 0);
            (int, int) finish = (0, 0);

            for (int i = 1; i < inputs.Length - 1; i++)
            {
                for (int j = 1; j < inputs[i].Length - 1; j++)
                {
                    if (inputs[i][j] == '.')
                    {
                        if (inputs[i - 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Up));
                        }
                        if (inputs[i + 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Down));
                        }
                        if (inputs[i][j - 1] == '.')
                        {
                            roads.Add((i, j, Direction.Left));
                        }
                        if (inputs[i][j + 1] == '.')
                        {
                            roads.Add((i, j, Direction.Right));
                        }
                    }
                    else if (inputs[i][j] == 'S')
                    {
                        start = (i, j);
                        if (inputs[i - 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Up));
                            roads.Add((i - 1, j, Direction.Down));
                            visited.Add((i, j, Direction.Up), (1000, []));
                        }
                        if (inputs[i + 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Down));
                            roads.Add((i + 1, j, Direction.Up));
                            visited.Add((i, j, Direction.Down), (1000, []));
                        }
                        if (inputs[i][j - 1] == '.')
                        {
                            roads.Add((i, j, Direction.Left));
                            roads.Add((i, j - 1, Direction.Right));
                            visited.Add((i, j, Direction.Left), (2000, []));
                        }
                        if (inputs[i][j + 1] == '.')
                        {
                            roads.Add((i, j, Direction.Right));
                            roads.Add((i, j + 1, Direction.Left));
                            visited.Add((i, j, Direction.Right), (0, []));
                        }                        
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        finish = (i, j);
                        roads.Add((i, j, Direction.Up));
                        roads.Add((i - 1, j, Direction.Down));

                        roads.Add((i, j, Direction.Down));
                        roads.Add((i + 1, j, Direction.Up));

                        roads.Add((i, j, Direction.Left));
                        roads.Add((i, j - 1, Direction.Right));

                        roads.Add((i, j, Direction.Right));
                        roads.Add((i, j + 1, Direction.Left));
                    }
                }
            }

            while (true)
            {
                var reindeer = visited.Where(x => roads.Contains(x.Key)).MinBy(x => x.Value.Item1);

                if ((reindeer.Key.Item1, reindeer.Key.Item2) == finish)
                {
                    result = reindeer.Value.Item2.Count + 1;
                    break;
                }

                if (reindeer.Key.Item3 == Direction.Up)
                {
                    if (roads.Contains((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Up))) 
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Up)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (upValue.Item1 == reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Up)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, .. upValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Up), (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Left)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (leftValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Left)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. leftValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Left), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Right)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (rightValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Right)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. rightValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Right), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    roads.Remove((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Down));
                    visited.Remove((reindeer.Key.Item1 - 1, reindeer.Key.Item2, Direction.Down));
                }
                else if (reindeer.Key.Item3 == Direction.Down)
                {
                    if (roads.Contains((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Down)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (downValue.Item1 == reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Down)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, .. downValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Down), (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Left)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (leftValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Left)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. leftValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Left), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Right)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (rightValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Right)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. rightValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Right), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    roads.Remove((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Up));
                    visited.Remove((reindeer.Key.Item1 + 1, reindeer.Key.Item2, Direction.Up));
                }
                else if (reindeer.Key.Item3 == Direction.Left)
                {
                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Left)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (leftValue.Item1 == reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Left)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, .. leftValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Left), (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Up)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Up)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (upValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Up)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. upValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Up), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Down)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (downValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Down)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. downValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Down), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    roads.Remove((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Right));
                    visited.Remove((reindeer.Key.Item1, reindeer.Key.Item2 - 1, Direction.Right));
                }
                else if (reindeer.Key.Item3 == Direction.Right)
                {
                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Right)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (rightValue.Item1 == reindeer.Value.Item1 + 1)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Right)] = (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, .. rightValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Right), (reindeer.Value.Item1 + 1, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Up)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Up)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (upValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Up)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. upValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Up), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    if (roads.Contains((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Down)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                            else if (downValue.Item1 == reindeer.Value.Item1 + 1001)
                            {
                                visited[(reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Down)] = (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, .. downValue.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Down), (reindeer.Value.Item1 + 1001, [.. reindeer.Value.Item2, (reindeer.Key.Item1, reindeer.Key.Item2)]));
                        }
                    }

                    roads.Remove((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Left));
                    visited.Remove((reindeer.Key.Item1, reindeer.Key.Item2 + 1, Direction.Left));
                }
                roads.Remove(reindeer.Key);
            }

            return result;
        }

        private enum Direction { Left, Right, Up, Down }
    }
}
