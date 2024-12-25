using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day24
    {
        public static long Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day24.1.txt");
            SortedDictionary<string, long> values = [];
            Queue<(string, string, string, string)> operations = [];

            bool isStart = true;
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    isStart = false;
                    continue;
                }
                if (isStart)
                {
                    values.Add(input[0..3], long.Parse(input[5].ToString()));
                }
                else
                {
                     var split = input.Split(' ', StringSplitOptions.TrimEntries).ToArray();
                    operations.Enqueue((split[0], split[1], split[2], split[4]));
                }
            }

            while (operations.TryDequeue(out var operation))
            {
                if (values.TryGetValue(operation.Item1, out long valueFirst) == false || values.TryGetValue(operation.Item3, out long valueSecond) == false)
                {
                    operations.Enqueue(operation);
                }
                else
                {
                    if (operation.Item2 == "XOR")
                    {
                        values[operation.Item4] = valueFirst ^ valueSecond;
                    }
                    else if (operation.Item2 == "OR")
                    {
                        values[operation.Item4] = valueFirst | valueSecond;
                    }
                    else if (operation.Item2 == "AND")
                    {
                        values[operation.Item4] = valueFirst & valueSecond;
                    }
                }
            }

            List<long> z = values.Where(x => x.Key[0] == 'z').Select(x => x.Value).ToList();
            for (int i = 0; i < z.Count; i++)
            {
                result += (z[i] << i);
            }

            return result;
        }

        public static string Part2()
        {
            string result = "";
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day24.1.txt");
            Dictionary<string, (string, string, string)> operations = [];
            SortedSet<string> wrongGates = [];

            bool isStart = true;
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    isStart = false;
                    continue;
                }
                if (isStart == false)
                {
                    var split = input.Split(' ', StringSplitOptions.TrimEntries).ToArray();
                    operations[split[4]] = (split[0], split[1], split[2]);
                    
                }
            }

            //Will only find most of the bad output gates, the rest will need to be done manually
            foreach (var operation in operations)
            {
                if (operation.Key[0] == 'z')
                {
                    if (operation.Value.Item2 != "XOR")
                    {
                        wrongGates.Add(operation.Key);
                    }
                }
                else if (operation.Key[0] != 'z' && operation.Value.Item1[0] != 'x' && operation.Value.Item1[0] != 'y' && operation.Value.Item3[0] != 'x' && operation.Value.Item3[0] != 'y' && operation.Value.Item2 == "XOR")
                {
                    wrongGates.Add(operation.Key);
                }
            }

            //Answer found manually looking at the binary difference between expected and actual result of part 1
            //Then making swaps in the dataset so every z(n) is equal (x(n-1) AND y(n-1)) XOR (x(n) XOR (y(n))
            SortedSet<string> answer = ["z10", "kmb", "z15", "tvp", "z25", "dpg", "mmf", "vdk"];
            result = string.Join(',', answer);
            return result;
        }
    }
}
