using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day11
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day11.1.txt");
            Dictionary<long, int> numbers = inputs[0].Split(' ').Select(long.Parse).ToDictionary(n => n, n => 1);

            for (int i = 0; i < 25; i++)
            {
                Dictionary<long, int> newNumbers = [];
                foreach (KeyValuePair<long, int> number in numbers)
                {
                    int numLength = (int)Math.Floor(Math.Log10(number.Key)) + 1;
                    if (number.Key == 0)
                    {
                        if (newNumbers.TryGetValue(1, out _))
                        {
                            newNumbers[1] = newNumbers[1] + number.Value;
                        }
                        else
                        {
                            newNumbers[1] = number.Value;
                        };
                    }
                    else if (numLength % 2 == 0)
                    {
                        int halfNum = (int)Math.Pow(10, numLength / 2);
                        long leftNum = number.Key / halfNum;
                        if (newNumbers.TryGetValue(leftNum, out _))
                        {
                            newNumbers[leftNum] = newNumbers[leftNum] + number.Value;
                        }
                        else
                        {
                            newNumbers[leftNum] = number.Value;
                        };

                        long rightNum = number.Key % halfNum;
                        if (newNumbers.TryGetValue(rightNum, out _))
                        {
                            newNumbers[rightNum] = newNumbers[rightNum] + number.Value;
                        }
                        else
                        {
                            newNumbers[rightNum] = number.Value;
                        };
                    }
                    else
                    {
                        long num = number.Key * 2024;
                        if (newNumbers.TryGetValue(num, out _))
                        {
                            newNumbers[num] = newNumbers[num] + number.Value;
                        }
                        else
                        {
                            newNumbers[num] = number.Value;
                        };
                    }
                }
                numbers = newNumbers;
            }

            result = numbers.Values.Sum();
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day11.1.txt");
            Dictionary<long, long> numbers = inputs[0].Split(' ').Select(long.Parse).ToDictionary(n => n, n => 1L);

            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> newNumbers = [];
                foreach (KeyValuePair<long, long> number in numbers)
                {
                    int numLength = (int)Math.Floor(Math.Log10(number.Key)) + 1;
                    if (number.Key == 0)
                    {
                        if (newNumbers.TryGetValue(1, out _))
                        {
                            newNumbers[1] = newNumbers[1] + number.Value;
                        }
                        else
                        {
                            newNumbers[1] = number.Value;
                        };
                    }
                    else if (numLength % 2 == 0)
                    {
                        int halfNum = (int)Math.Pow(10, numLength / 2);
                        long leftNum = number.Key / halfNum;
                        if (newNumbers.TryGetValue(leftNum, out _))
                        {
                            newNumbers[leftNum] = newNumbers[leftNum] + number.Value;
                        }
                        else
                        {
                            newNumbers[leftNum] = number.Value;
                        };

                        long rightNum = number.Key % halfNum;
                        if (newNumbers.TryGetValue(rightNum, out _))
                        {
                            newNumbers[rightNum] = newNumbers[rightNum] + number.Value;
                        }
                        else
                        {
                            newNumbers[rightNum] = number.Value;
                        };
                    }
                    else
                    {
                        long num = number.Key * 2024;
                        if (newNumbers.TryGetValue(num, out _))
                        {
                            newNumbers[num] = newNumbers[num] + number.Value;
                        }
                        else
                        {
                            newNumbers[num] = number.Value;
                        };
                    }
                }
                numbers = newNumbers;
            }

            result = numbers.Values.Sum();
            return result;
        }
    }
}
