using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day7
    {
        public static async Task<long> Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day7.1.txt");
            List<Task> tasks = [];
            for (int i = 0; i < inputs.Length; i++)
            {
                var split1 = inputs[i].Split(':');
                long target = long.Parse(split1[0]);
                var numbers = split1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                tasks.Add(Task.Run(() =>
                {
                    if (IsValidEquation(target, 0, numbers, Operation.Add, false) || IsValidEquation(target, 1, numbers, Operation.Multiply, false))
                    {
                        Interlocked.Add(ref result, target);
                    }
                }));
            }
            await Task.WhenAll(tasks);
            return result;
        }

        public static async Task<long> Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day7.1.txt");
            List<Task> tasks = [];

            for (int i = 0; i < inputs.Length; i++)
            {
                var split1 = inputs[i].Split(':');
                long target = long.Parse(split1[0]);
                var numbers = split1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                tasks.Add(Task.Run(() =>
                {
                    if (IsValidEquation(target, 0, numbers, Operation.Add, true) || IsValidEquation(target, 1, numbers, Operation.Multiply, true) || IsValidEquation(target, 1, numbers, Operation.Concat, true))
                    {
                        result += target;
                    }
                }));
            }
            await Task.WhenAll(tasks);
            return result;
        }
        
        private static bool IsValidEquation(long target, long sum, int[] numbers, Operation operation, bool allowConcat)
        {
            switch (operation)
            {

                case Operation.Add:
                    sum += numbers[0];
                    break;
                case Operation.Multiply:
                    sum *= numbers[0];
                    break;
                case Operation.Concat:
                    sum = Convert.ToInt64(string.Format("{0}{1}", sum, numbers[0])); ;
                    break;
            };
            if (sum > target)
            {
                return false;
            }
            if (numbers.Length == 1)
            {
                return sum == target;
            }
            else if (allowConcat)
            {
                return IsValidEquation(target, sum, numbers[1..], Operation.Add, allowConcat) || IsValidEquation(target, sum, numbers[1..], Operation.Multiply, allowConcat) || IsValidEquation(target, sum, numbers[1..], Operation.Concat, allowConcat);
            }
            else
            {
                return IsValidEquation(target, sum, numbers[1..], Operation.Add, allowConcat) || IsValidEquation(target, sum, numbers[1..], Operation.Multiply, allowConcat);
            };
        }

        private enum Operation
        {
            Add,
            Multiply,
            Concat
        }
    }
}
