using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day16
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day16.1.txt");
            HashSet<(int, int)> roads = [];
            Dictionary<(int, int, Direction), int> visited = [];
            PriorityQueue<(int, int, Direction), int> queue = new();
            (int, int) finish = (0, 0);

            for (int i = 1; i < inputs.Length - 1; i++)
            {
                for (int j = 1; j < inputs[i].Length - 1; j++)
                {
                    if (inputs[i][j] == '.')
                    {
                        roads.Add((i, j));
                    }
                    else if (inputs[i][j] == 'S')
                    {
                        queue.Enqueue((i, j, Direction.Right), 0);
                        visited.Add((i, j, Direction.Right), 0);
                        queue.Enqueue((i, j, Direction.Up), 1000);
                        visited.Add((i, j, Direction.Up), 1000);
                        queue.Enqueue((i, j, Direction.Down), 1000);
                        visited.Add((i, j, Direction.Down), 1000);
                        queue.Enqueue((i, j, Direction.Left), 2000);
                        visited.Add((i, j, Direction.Left), 2000);
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        finish = (i, j);
                        roads.Add((i, j));
                    }
                }
            }

            while (queue.TryDequeue(out var reindeer, out int priority))
            {
                if ((reindeer.Item1, reindeer.Item2) == finish)
                {
                    result = visited[reindeer];
                    break;
                }

                if (reindeer.Item3 == Direction.Up)
                {
                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), out var upValue))
                        {
                            if (upValue > priority + 1)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Up)] = priority + 1;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), priority + 1);
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue > priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Left)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), priority + 1001);
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue > priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Right)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), priority + 1001);
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1 - 1, reindeer.Item2));
                    visited.Remove((reindeer.Item1 - 1, reindeer.Item2, Direction.Down));
                }
                else if (reindeer.Item3 == Direction.Down)
                {
                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), out var downValue))
                        {
                            if (downValue > priority + 1)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Down)] = priority + 1;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), priority + 1);
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue > priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Left)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), priority + 1001);
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue > priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Right)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), priority + 1001);
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1 + 1, reindeer.Item2));
                    visited.Remove((reindeer.Item1 + 1, reindeer.Item2, Direction.Up));
                }
                else if (reindeer.Item3 == Direction.Left)
                {
                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), out var leftValue))
                        {
                            if (leftValue > priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Left)] = priority + 1;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), priority + 1);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), out var upValue))
                        {
                            if (upValue > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Up)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), priority + 1001);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), out var downValue))
                        {
                            if (downValue > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Down)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), priority + 1001);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1, reindeer.Item2 - 1));
                    visited.Remove((reindeer.Item1, reindeer.Item2 - 1, Direction.Right));
                }
                else if (reindeer.Item3 == Direction.Right)
                {
                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), out var rightValue))
                        {
                            if (rightValue > priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Right)] = priority + 1;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), priority + 1);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), out var upValue))
                        {
                            if (upValue > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Up)] = priority + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), priority + 1001);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), out var downValue))
                        {
                            if (downValue > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Down)] = reindeer.Item1 + 1001;
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), priority + 1001);
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), priority + 1001);
                        }
                    }
                    roads.Remove((reindeer.Item1, reindeer.Item2 + 1));
                    visited.Remove((reindeer.Item1, reindeer.Item2 + 1, Direction.Left));
                }
                roads.Remove((reindeer.Item1, reindeer.Item2));
            }

            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day16.1.txt");
            HashSet<(int, int, Direction)> roads = [];
            Dictionary<(int, int, Direction), (int, HashSet<(int, int)>)> visited = [];
            PriorityQueue<(int, int, Direction), int> queue = new();
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
                        queue.Enqueue((i, j, Direction.Right), 0);
                        visited.Add((i, j, Direction.Right), (0, []));
                        queue.Enqueue((i, j, Direction.Up), 1000);
                        visited.Add((i, j, Direction.Up), (1000, []));
                        queue.Enqueue((i, j, Direction.Down), 1000);
                        visited.Add((i, j, Direction.Down), (1000, []));
                        queue.Enqueue((i, j, Direction.Left), 2000);
                        visited.Add((i, j, Direction.Left), (2000, []));
                    }
                    else if (inputs[i][j] == 'E')
                    {
                        finish = (i, j);
                        if (inputs[i - 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Down));
                            roads.Add((i - 1, j, Direction.Down));
                        }
                        if (inputs[i + 1][j] == '.')
                        {
                            roads.Add((i, j, Direction.Up));
                            roads.Add((i + 1, j, Direction.Up));
                        }
                        if (inputs[i][j - 1] == '.')
                        {
                            roads.Add((i, j, Direction.Right));
                            roads.Add((i, j - 1, Direction.Right));
                        }
                        if (inputs[i][j + 1] == '.')
                        {
                            roads.Add((i, j, Direction.Left));
                            roads.Add((i, j + 1, Direction.Left));
                        }
                    }
                }
            }

            while (queue.TryDequeue(out var reindeer, out int priority))
            {
                if ((reindeer.Item1, reindeer.Item2) == finish)
                {
                    result = visited[reindeer].Item2.Count + 1;
                    break;
                }

                if (reindeer.Item3 == Direction.Up)
                {
                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2, Direction.Up)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > priority + 1)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Up)] = (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (upValue.Item1 == priority + 1)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Up)] = (priority + 1, [.. visited[reindeer].Item2, .. upValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Up), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Left)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (leftValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Left)] = (priority + 1001, [.. visited[reindeer].Item2, .. leftValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Left), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 - 1, reindeer.Item2, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Right)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (rightValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1 - 1, reindeer.Item2, Direction.Right)] = (priority + 1001, [.. visited[reindeer].Item2, .. rightValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 - 1, reindeer.Item2, Direction.Right), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1 - 1, reindeer.Item2, Direction.Down));
                    visited.Remove((reindeer.Item1 - 1, reindeer.Item2, Direction.Down));
                }
                else if (reindeer.Item3 == Direction.Down)
                {
                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > priority + 1)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Down)] = (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (downValue.Item1 == priority + 1)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Down)] = (priority + 1, [.. visited[reindeer].Item2, .. downValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Down), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Left)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (leftValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Left)] = (priority + 1001, [.. visited[reindeer].Item2, .. leftValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Left), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1 + 1, reindeer.Item2, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Right)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (rightValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1 + 1, reindeer.Item2, Direction.Right)] = (priority + 1001, [.. visited[reindeer].Item2, .. rightValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1 + 1, reindeer.Item2, Direction.Right), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1 + 1, reindeer.Item2, Direction.Up));
                    visited.Remove((reindeer.Item1 + 1, reindeer.Item2, Direction.Up));
                }
                else if (reindeer.Item3 == Direction.Left)
                {
                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1, Direction.Left)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), out var leftValue))
                        {
                            if (leftValue.Item1 > priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Left)] = (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (leftValue.Item1 == priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Left)] = (priority + 1, [.. visited[reindeer].Item2, .. leftValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Left), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1, Direction.Up)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Up)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (upValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Up)] = (priority + 1001, [.. visited[reindeer].Item2, .. upValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Up), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 - 1, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Down)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (downValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 - 1, Direction.Down)] = (priority + 1001, [.. visited[reindeer].Item2, .. downValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 - 1, Direction.Down), priority + 1001);
                        }
                    }

                    roads.Remove((reindeer.Item1, reindeer.Item2 - 1, Direction.Right));
                    visited.Remove((reindeer.Item1, reindeer.Item2 - 1, Direction.Right));
                }
                else if (reindeer.Item3 == Direction.Right)
                {
                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1, Direction.Right)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), out var rightValue))
                        {
                            if (rightValue.Item1 > priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Right)] = (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (rightValue.Item1 == priority + 1)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Right)] = (priority + 1, [.. visited[reindeer].Item2, .. rightValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), (priority + 1, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Right), priority + 1);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1, Direction.Up)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), out var upValue))
                        {
                            if (upValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Up)] = (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (upValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Up)] = (priority + 1001, [.. visited[reindeer].Item2, .. upValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Up), priority + 1001);
                        }
                    }

                    if (roads.Contains((reindeer.Item1, reindeer.Item2 + 1, Direction.Down)))
                    {
                        if (visited.TryGetValue((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), out var downValue))
                        {
                            if (downValue.Item1 > priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Down)] = (reindeer.Item1 + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                            else if (downValue.Item1 == priority + 1001)
                            {
                                visited[(reindeer.Item1, reindeer.Item2 + 1, Direction.Down)] = (reindeer.Item1 + 1001, [.. visited[reindeer].Item2, .. downValue.Item2, (reindeer.Item1, reindeer.Item2)]);
                            }
                        }
                        else
                        {
                            visited.Add((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), (priority + 1001, [.. visited[reindeer].Item2, (reindeer.Item1, reindeer.Item2)]));
                            queue.Enqueue((reindeer.Item1, reindeer.Item2 + 1, Direction.Down), priority + 1001);
                        }
                    }
                    roads.Remove((reindeer.Item1, reindeer.Item2 + 1, Direction.Left));
                    visited.Remove((reindeer.Item1, reindeer.Item2 + 1, Direction.Left));
                }
                roads.Remove((reindeer.Item1, reindeer.Item2, reindeer.Item3));
            }

            return result;
        }

        private enum Direction { Left, Right, Up, Down }
    }
}
