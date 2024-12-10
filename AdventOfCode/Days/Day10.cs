using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day10
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day10.1.txt");

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '0')
                    {
                        HashSet<(int, int)> set = IsPeak(inputs, (i, j), -1);
                        set.Remove((-1, -1));
                        result += set.Count;
                    }
                }
            }
            
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day10.1.txt");

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] == '0')
                    {
                        result += IsPeakPart2(inputs, (i, j), -1);
                    }
                }
            }

            return result;
        }

        private static HashSet<(int,int)> IsPeak(string[] inputs, (int, int) current, int lastValue)
        {
            if (current.Item1 >= 0 && current.Item2 >= 0 && current.Item1 < inputs.Length && current.Item2 < inputs[0].Length)
            {
                int inputNum = int.Parse(inputs[current.Item1][current.Item2].ToString());
                
                if (inputNum == lastValue + 1)
                {
                    if (inputNum == 9)
                    {
                        return [(current.Item1, current.Item2)];
                    }
                    else
                    {
                        HashSet<(int, int)> set = IsPeak(inputs, (current.Item1 - 1, current.Item2), inputNum);
                        set.UnionWith(IsPeak(inputs, (current.Item1 + 1, current.Item2), inputNum));
                        set.UnionWith(IsPeak(inputs, (current.Item1, current.Item2 - 1), inputNum));
                        set.UnionWith(IsPeak(inputs, (current.Item1, current.Item2 + 1), inputNum));
                        return set;
                    }
                }
            }
            return [(-1, -1)];
        }

        private static int IsPeakPart2(string[] inputs, (int, int) current, int lastValue)
        {
            if (current.Item1 >= 0 && current.Item2 >= 0 && current.Item1 < inputs.Length && current.Item2 < inputs[0].Length)
            {
                int inputNum = int.Parse(inputs[current.Item1][current.Item2].ToString());

                if (inputNum == lastValue + 1)
                {
                    if (inputNum == 9)
                    {
                        return 1;
                    }
                    else
                    {
                        int res = IsPeakPart2(inputs, (current.Item1 - 1, current.Item2), inputNum);
                        res += IsPeakPart2(inputs, (current.Item1 + 1, current.Item2), inputNum);
                        res += IsPeakPart2(inputs, (current.Item1, current.Item2 - 1), inputNum);
                        res += IsPeakPart2(inputs, (current.Item1, current.Item2 + 1), inputNum);
                        return res;
                    }
                }
            }
            return 0;
        }
    }
}
