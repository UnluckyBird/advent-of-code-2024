using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day22
    {
        public static long Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day22.1.txt");

            foreach (string input in inputs)
            {
                long res = long.Parse(input);
                for (int i = 0; i < 2000; i++)
                {
                    res = ((res << 6) ^ res) % 16777216;
                    res = ((res >> 5) ^ res);
                    res = ((res << 11) ^ res) % 16777216;
                }
                result += res;
            }
            
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day22.1.txt");

            List<Dictionary<string, long>> allDifferences = [];

            foreach (string input in inputs)
            {
                Dictionary<string, long> differences = [];
                long res = long.Parse(input);
                Queue<long> sequence = new();
                for (int i = 0; i < 2000; i++)
                {
                    long lastRes = res % 10;
                    res = ((res << 6) ^ res) % 16777216;
                    res = ((res >> 5) ^ res);
                    res = ((res << 11) ^ res) % 16777216;
                    long diff = (res % 10) - lastRes;
                    sequence.Enqueue(diff);
                    if (sequence.Count == 4)
                    {
                        string seq = "";
                        foreach (int num in sequence)
                        {
                            seq += num.ToString();
                        }
                        sequence.Dequeue();
                        if (differences.TryGetValue(seq, out long value) == false)
                        {
                            differences[seq] = res % 10;
                        }
                    }
                }
                allDifferences.Add(differences);
            }

            Parallel.For(-9, 10, (i) =>
            {
                Parallel.For(-9, 10, (j) =>
                {
                    for (int k = -9; k < 10; k++)
                    {
                        for (int l = -9; l < 10; l++)
                        {
                            string seq = i.ToString() + j.ToString() + k.ToString() + l.ToString();
                            long res = allDifferences.Sum(x => x.TryGetValue(seq, out long value) ? value : 0);
                            if (res > result)
                            {
                                Interlocked.Exchange(ref result, res);
                            }
                        }
                    }
                });
            });

            return result;
        }
    }
}
