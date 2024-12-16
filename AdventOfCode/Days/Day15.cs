using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day15
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day15.1.txt");
            HashSet<(int, int)> walls = [];
            HashSet<(int, int)> boxes = [];
            (int, int) robot = (0, 0);

            bool isMap = true;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(inputs[i]))
                {
                    isMap = false;
                    continue; 
                }
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (isMap)
                    {
                        if (inputs[i][j] == '#')
                        {
                            walls.Add((i, j));
                        }
                        else if (inputs[i][j] == '@')
                        {
                            robot = (i, j);
                        }
                        else if (inputs[i][j] == 'O')
                        {
                            boxes.Add((i, j));
                        }
                    }
                    else
                    {
                        (int ,int) freeSpace = (-1,-1);
                        if (inputs[i][j] == '<')
                        {
                            for (int k = robot.Item2-1; k >= 0; k--)
                            {
                                if (walls.Contains((robot.Item1, k)))
                                {
                                    break;
                                }
                                else if (boxes.Contains((robot.Item1, k)) == false) 
                                {
                                    freeSpace = (robot.Item1, k);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                if (boxes.Remove((robot.Item1, robot.Item2 - 1)))
                                {
                                    boxes.Add(freeSpace);
                                }
                                robot = (robot.Item1, robot.Item2 - 1);
                            }
                        }
                        else if (inputs[i][j] == '>')
                        {
                            for (int k = robot.Item2 + 1; k < inputs[i].Length; k++)
                            {
                                if (walls.Contains((robot.Item1, k)))
                                {
                                    break;
                                }
                                else if (boxes.Contains((robot.Item1, k)) == false)
                                {
                                    freeSpace = (robot.Item1, k);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                if (boxes.Remove((robot.Item1, robot.Item2 + 1)))
                                {
                                    boxes.Add(freeSpace);
                                }
                                robot = (robot.Item1, robot.Item2 + 1);
                            }
                        }
                        else if (inputs[i][j] == '^')
                        {
                            for (int k = robot.Item1 - 1; k >= 0; k--)
                            {
                                if (walls.Contains((k, robot.Item2)))
                                {
                                    break;
                                }
                                else if (boxes.Contains((k, robot.Item2)) == false)
                                {
                                    freeSpace = (k, robot.Item2);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                if (boxes.Remove((robot.Item1 - 1, robot.Item2)))
                                {
                                    boxes.Add(freeSpace);
                                }
                                robot = (robot.Item1- 1, robot.Item2);
                            }
                        }
                        else if (inputs[i][j] == 'v')
                        {
                            for (int k = robot.Item1 + 1; k < inputs.Length; k++)
                            {
                                if (walls.Contains((k, robot.Item2)))
                                {
                                    break;
                                }
                                else if (boxes.Contains((k, robot.Item2)) == false)
                                {
                                    freeSpace = (k, robot.Item2);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                if (boxes.Remove((robot.Item1 + 1, robot.Item2)))
                                {
                                    boxes.Add(freeSpace);
                                }
                                robot = (robot.Item1 + 1, robot.Item2);
                            }
                        }
                    }
                }
            }
            foreach (var box in boxes)
            {
                result += box.Item1 * 100 + box.Item2;
            }
            
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day15.1.txt");
            HashSet<(int, int)> walls = [];
            HashSet<(int, int)> leftBoxes = [];
            HashSet<(int, int)> rightBoxes = [];
            (int, int) robot = (0, 0);

            bool isMap = true;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(inputs[i]))
                {
                    isMap = false;
                    continue;
                }
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (isMap)
                    {
                        if (inputs[i][j] == '#')
                        {
                            walls.Add((i, j*2));
                            walls.Add((i, (j*2)+1));
                        }
                        else if (inputs[i][j] == '@')
                        {
                            robot = (i, j*2);
                        }
                        else if (inputs[i][j] == 'O')
                        {
                            leftBoxes.Add((i, j*2));
                            rightBoxes.Add((i, (j*2)+1));
                        }
                    }
                    else
                    {
                        (int, int) freeSpace = (-1, -1);
                        if (inputs[i][j] == '<')
                        {
                            for (int k = robot.Item2 - 1; k >= 0; k--)
                            {
                                if (walls.Contains((robot.Item1, k)))
                                {
                                    break;
                                }
                                else if (rightBoxes.Contains((robot.Item1, k)) == false && leftBoxes.Contains((robot.Item1, k)) == false)
                                {
                                    freeSpace = (robot.Item1, k);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                for (int k = robot.Item2 - 1; k > freeSpace.Item2; k--)
                                {
                                    if (rightBoxes.Remove((robot.Item1, k)))
                                    {
                                        rightBoxes.Add((robot.Item1, k - 1));
                                        if (leftBoxes.Remove((robot.Item1, k - 1)))
                                        {
                                            leftBoxes.Add((robot.Item1, k - 2));
                                        }
                                        k--;
                                    }
                                }
                                robot = (robot.Item1, robot.Item2 - 1);
                            }
                        }
                        else if (inputs[i][j] == '>')
                        {
                            for (int k = robot.Item2 + 1; k < inputs[i].Length; k++)
                            {
                                if (walls.Contains((robot.Item1, k)))
                                {
                                    break;
                                }
                                else if (rightBoxes.Contains((robot.Item1, k)) == false && leftBoxes.Contains((robot.Item1, k)) == false)
                                {
                                    freeSpace = (robot.Item1, k);
                                    break;
                                }
                            }
                            if (freeSpace != (-1, -1))
                            {
                                for (int k = robot.Item2 + 1; k < freeSpace.Item2; k++)
                                {
                                    if (leftBoxes.Remove((robot.Item1, k)))
                                    {
                                        leftBoxes.Add((robot.Item1, k + 1));
                                        if (rightBoxes.Remove((robot.Item1, k + 1)))
                                        {
                                            rightBoxes.Add((robot.Item1, k + 2));
                                        }
                                        k++;
                                    }
                                }
                                robot = (robot.Item1, robot.Item2 + 1);
                            }
                        }
                        else if (inputs[i][j] == '^')
                        {
                            if (CanMoveUp(walls, leftBoxes, rightBoxes, (robot.Item1, robot.Item2)))
                            {
                                MoveUp(walls, leftBoxes, rightBoxes, (robot.Item1, robot.Item2));
                                robot = (robot.Item1 - 1, robot.Item2);
                            };
                        }
                        else if (inputs[i][j] == 'v')
                        {
                            if (CanMoveDown(walls, leftBoxes, rightBoxes, (robot.Item1, robot.Item2)))
                            {
                                MoveDown(walls, leftBoxes, rightBoxes, (robot.Item1, robot.Item2));
                                robot = (robot.Item1 + 1, robot.Item2);
                            };
                        }
                    }
                }
            }
            foreach (var box in leftBoxes)
            {
                result += box.Item1 * 100 + box.Item2;
            }

            return result;
        }

        private static bool CanMoveUp(HashSet<(int, int)> walls, HashSet<(int, int)> leftBoxes, HashSet<(int, int)> rightBoxes, (int, int) start)
        {
            if (walls.Contains((start.Item1 - 1, start.Item2)))
            {
                return false;
            }
            if (leftBoxes.Contains((start.Item1 - 1, start.Item2)))
            {
                return CanMoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2)) && CanMoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2 + 1));
            }
            if (rightBoxes.Contains((start.Item1 - 1, start.Item2)))
            {
                return CanMoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2)) && CanMoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2 - 1));
            }
            return true;
        }

        private static void MoveUp(HashSet<(int, int)> walls, HashSet<(int, int)> leftBoxes, HashSet<(int, int)> rightBoxes, (int, int) start)
        {
            if (leftBoxes.Contains((start.Item1 - 1, start.Item2)))
            {
                MoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2));
                MoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2 + 1));
            }
            else if (rightBoxes.Contains((start.Item1 - 1, start.Item2)))
            {
                MoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2));
                MoveUp(walls, leftBoxes, rightBoxes, (start.Item1 - 1, start.Item2 - 1));
            }

            if (leftBoxes.Remove((start.Item1, start.Item2)))
            {
                leftBoxes.Add((start.Item1 - 1, start.Item2));
            }
            if (rightBoxes.Remove((start.Item1, start.Item2)))
            {
                rightBoxes.Add((start.Item1 - 1, start.Item2));
            }
        }

        private static bool CanMoveDown(HashSet<(int, int)> walls, HashSet<(int, int)> leftBoxes, HashSet<(int, int)> rightBoxes, (int, int) start)
        {
            if (walls.Contains((start.Item1 + 1, start.Item2)))
            {
                return false;
            }
            if (leftBoxes.Contains((start.Item1 + 1, start.Item2)))
            {
                return CanMoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2)) && CanMoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2 + 1));
            }
            if (rightBoxes.Contains((start.Item1 + 1, start.Item2)))
            {
                return CanMoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2)) && CanMoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2 - 1));
            }
            return true;
        }

        private static void MoveDown(HashSet<(int, int)> walls, HashSet<(int, int)> leftBoxes, HashSet<(int, int)> rightBoxes, (int, int) start)
        {
            if (leftBoxes.Contains((start.Item1 + 1, start.Item2)))
            {
                MoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2));
                MoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2 + 1));
            }
            else if (rightBoxes.Contains((start.Item1 + 1, start.Item2)))
            {
                MoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2));
                MoveDown(walls, leftBoxes, rightBoxes, (start.Item1 + 1, start.Item2 - 1));
            }

            if (leftBoxes.Remove((start.Item1, start.Item2)))
            {
                leftBoxes.Add((start.Item1 + 1, start.Item2));
            }
            if (rightBoxes.Remove((start.Item1, start.Item2)))
            {
                rightBoxes.Add((start.Item1 + 1, start.Item2));
            }
        }
    }
}
