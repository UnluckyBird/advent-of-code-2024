using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day6
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day6.1.txt");

            HashSet<(int, int)> road = [];
            HashSet<(int,int)> obstacles = [];
            (int, int, Direction) player = (0,0, Direction.Up);
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '#')
                    {
                        obstacles.Add((i,j));
                    }
                    else if (inputs[i][j] == '^')
                    {
                        road.Add((i,j));
                        player = (i,j, Direction.Up);
                    }
                }
            }
            while (player.Item1 > 0 && player.Item1 < inputs.Length && player.Item2 > 0 && player.Item2 < inputs[0].Length)
            {
                if (player.Item3 == Direction.Up)
                {
                    if (obstacles.Contains((player.Item1 - 1, player.Item2)) == false)
                    {
                        player.Item1 -= 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Right;
                    }
                }
                else if (player.Item3 == Direction.Right)
                {
                    if (obstacles.Contains((player.Item1, player.Item2 + 1)) == false)
                    {
                        player.Item2 += 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Down;
                    }
                }
                else if (player.Item3 == Direction.Down)
                {
                    if (obstacles.Contains((player.Item1 + 1, player.Item2)) == false)
                    { 
                        player.Item1 += 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Left;
                    }
                }
                else if (player.Item3 == Direction.Left)
                {
                    if(obstacles.Contains((player.Item1, player.Item2 - 1)) == false)
                    {

                        player.Item2 -= 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Up;
                    }
                }
                road.Add((player.Item1, player.Item2));
            }

            road.Remove(road.Last());
            result = road.Count;
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day6.1.txt");

            HashSet<(int, int)> road = [];
            HashSet<(int, int)> obstacles = [];
            (int, int, Direction) player = (0, 0, Direction.Up);
            (int, int, Direction) start = (0, 0, Direction.Up);
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '#')
                    {
                        obstacles.Add((i, j));
                    }
                    else if (inputs[i][j] == '^')
                    {
                        road.Add((i, j));
                        player = (i, j, Direction.Up);
                        start = player;
                    }
                }
            }
            while (player.Item1 > 0 && player.Item1 < inputs[0].Length && player.Item2 > 0 && player.Item2 < inputs.Length)
            {
                if (player.Item3 == Direction.Up)
                {
                    if (obstacles.Contains((player.Item1 - 1, player.Item2)) == false)
                    {
                        player.Item1 -= 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Right;
                    }
                }
                else if (player.Item3 == Direction.Right)
                {
                    if (obstacles.Contains((player.Item1, player.Item2 + 1)) == false)
                    {
                        player.Item2 += 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Down;
                    }
                }
                else if (player.Item3 == Direction.Down)
                {
                    if (obstacles.Contains((player.Item1 + 1, player.Item2)) == false)
                    {
                        player.Item1 += 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Left;
                    }
                }
                else if (player.Item3 == Direction.Left)
                {
                    if (obstacles.Contains((player.Item1, player.Item2 - 1)) == false)
                    {
                        player.Item2 -= 1;
                    }
                    else
                    {
                        player.Item3 = Direction.Up;
                    }
                }
                road.Add((player.Item1, player.Item2));
            }
            road.Remove(road.Last());
            road.Remove(road.First());

            Parallel.ForEach(road, x =>
            {
                HashSet<(int, int)> obs = new(obstacles)
                {
                    x
                };
                if (IsLoop(start, obs, inputs.Length))
                {
                    Interlocked.Increment(ref result);
                }
            });

            return result;
        }

        private static bool IsLoop((int, int, Direction) player, HashSet<(int, int)> obstacles, int Size)
        {
            HashSet<(int, int, Direction)> road = [player];
            while (player.Item1 > 0 && player.Item1 < Size && player.Item2 > 0 && player.Item2 < Size)
            {
                if (player.Item3 == Direction.Up)
                {
                    while (obstacles.Contains((player.Item1 - 1, player.Item2)) == false && player.Item1 > 0)
                    {
                        player.Item1 -= 1;
                    }
                    player.Item3 = Direction.Right;
                }
                else if (player.Item3 == Direction.Right)
                {
                    while (obstacles.Contains((player.Item1, player.Item2 + 1)) == false && player.Item2 < Size)
                    {
                        player.Item2 += 1;
                    }
                    player.Item3 = Direction.Down;
                }
                else if (player.Item3 == Direction.Down)
                {
                    while (obstacles.Contains((player.Item1 + 1, player.Item2)) == false && player.Item1 < Size)
                    {
                        player.Item1 += 1;
                    }
                    player.Item3 = Direction.Left;
                }
                else if (player.Item3 == Direction.Left)
                {
                    while (obstacles.Contains((player.Item1, player.Item2 - 1)) == false && player.Item2 > 0)
                    {
                        player.Item2 -= 1;
                    }
                    player.Item3 = Direction.Up;
                }
                if (road.Add(player) == false)
                {
                    return true;
                }
            }
            return false;
        }

        private enum Direction {
            Up, Right, Down, Left
        }
    }
}
