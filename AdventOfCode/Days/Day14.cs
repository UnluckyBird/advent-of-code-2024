using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day14
    {
        public static int Part1()
        {
            int result = 0;
            (int, int) gridSize = (101,103);
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day14.1.txt");
            Dictionary<(int, int), List<(int, int)>> robots = [];

            foreach (string input in inputs)
            {
                var robot = input.Substring(2).Replace("v=", ",").Split(',').Select(int.Parse).ToList();
                if (robots.TryAdd((robot[0], robot[1]), [(robot[2], robot[3])]) == false)
                {
                    robots[(robot[0], robot[1])].Add((robot[2], robot[3]));
                }
            }

            for (int i = 0; i < 100; i++)
            {
                Dictionary<(int, int), List<(int, int)>> newRobots = [];
                foreach (var robotList in robots)
                {
                    foreach (var robot in robotList.Value)
                    {
                        int x = robotList.Key.Item1 + robot.Item1;
                        int y = robotList.Key.Item2 + robot.Item2;
                        if (x < 0)
                        {
                            x += gridSize.Item1;
                        }
                        else if (x >= gridSize.Item1)
                        {
                            x -= gridSize.Item1;
                        }
                        if (y < 0)
                        {
                            y += gridSize.Item2;
                        }
                        else if (y >= gridSize.Item2)
                        {
                            y -= gridSize.Item2;
                        }
                        if (newRobots.TryAdd((x,y), [(robot.Item1, robot.Item2)]) == false)
                        {
                            newRobots[(x,y)].Add((robot.Item1, robot.Item2));
                        }
                    }
                }
                robots = newRobots;
            }
            int q1 = 0;
            for (int i = 0; i < gridSize.Item1/2; i++)
            {
                for (int j = 0; j < gridSize.Item2/2; j++)
                {
                    if (robots.TryGetValue((i,j), out var value))
                    {
                        q1 += value.Count;
                    }
                }
            }
            int q2 = 0;
            for (int i = (gridSize.Item1 / 2) + 1; i < gridSize.Item1; i++)
            {
                for (int j = 0; j < gridSize.Item2 / 2; j++)
                {
                    if (robots.TryGetValue((i,j), out var value))
                    {
                        q2 += value.Count;
                    }
                }
            }
            int q3 = 0;
            for (int i = 0; i < gridSize.Item1 / 2; i++)
            {
                for (int j = (gridSize.Item2 / 2) + 1; j < gridSize.Item2; j++)
                {
                    if (robots.TryGetValue((i,j), out var value))
                    {
                        q3 += value.Count;
                    }
                }
            }
            int q4 = 0;
            for (int i = (gridSize.Item1 / 2) + 1; i < gridSize.Item1; i++)
            {
                for (int j = (gridSize.Item2 / 2) + 1; j < gridSize.Item2; j++)
                {
                    if (robots.TryGetValue((i,j), out var value))
                    {
                        q4 += value.Count;
                    }
                }
            }
            result = q1 * q2 * q3 * q4;
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            (int, int) gridSize = (101, 103);
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day14.1.txt");
            Dictionary<(int, int), List<(int, int)>> robots = [];

            foreach (string input in inputs)
            {
                var robot = input.Substring(2).Replace("v=", ",").Split(',').Select(int.Parse).ToList();
                if (robots.TryAdd((robot[0], robot[1]), [(robot[2], robot[3])]) == false)
                {
                    robots[(robot[0], robot[1])].Add((robot[2], robot[3]));
                }
            }

            for (int i = 0; i < 8000; i++)
            {
                Dictionary<(int, int), List<(int, int)>> newRobots = [];
                foreach (var robotList in robots)
                {
                    foreach (var robot in robotList.Value)
                    {
                        int x = robotList.Key.Item1 + robot.Item1;
                        int y = robotList.Key.Item2 + robot.Item2;
                        if (x < 0)
                        {
                            x += gridSize.Item1;
                        }
                        else if (x >= gridSize.Item1)
                        {
                            x -= gridSize.Item1;
                        }
                        if (y < 0)
                        {
                            y += gridSize.Item2;
                        }
                        else if (y >= gridSize.Item2)
                        {
                            y -= gridSize.Item2;
                        }
                        if (newRobots.TryAdd((x, y), [(robot.Item1, robot.Item2)]) == false)
                        {
                            newRobots[(x, y)].Add((robot.Item1, robot.Item2));
                        }
                    }
                }
                robots = newRobots;
                
                bool resultFound = false;
                for (int j = 0; j < gridSize.Item1; j++)
                {
                    int line = 0;
                    for (int k = 0; k < gridSize.Item2; k++)
                    {
                        if (robots.ContainsKey((j, k)))
                        {
                            line += 1;
                        }
                        else
                        {
                            line = 0;
                        }
                        if (line > 10)
                        {
                            resultFound = true;
                            result = i + 1;
                            break;
                        }
                    }
                    if (resultFound)
                    {
                        break;
                    }
                }
                
                /*if (resultFound)
                {
                    for (int j = 0; j < gridSize.Item2; j++)
                    {
                        string line = "";
                        for (int k = 0; k < gridSize.Item1; k++)
                        {
                            if (robots.ContainsKey((k, j)))
                            {
                                line += '#';
                            }
                            else
                            {
                                line += ' ';
                            }
                        }
                        Console.WriteLine(line);
                    }
                }*/
            }
            
            return result;
        }
    }
}
