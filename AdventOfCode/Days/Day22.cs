using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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

            ConcurrentDictionary<int, long> sequences = [];
            Parallel.ForEach(inputs, input =>
            {
                HashSet<int> seenSequences = [];
                long res = long.Parse(input);
                long lastRes = 0;
                long diff = 0;
                int numSeq = 0;

                for (int i = 0; i < 2000; i++)
                {
                    lastRes = res % 10;
                    res = ((res << 6) ^ res) & 16777215;
                    res = ((res >> 5) ^ res);
                    res = ((res << 11) ^ res) & 16777215;
                    diff = (res % 10) - lastRes + 9;

                    numSeq = (int)diff + numSeq;

                    if (i >= 3)
                    {
                        if (seenSequences.Add(numSeq))
                        {
                            sequences.AddOrUpdate(numSeq, res % 10, (key, oldValue) => oldValue + (res % 10));

                            if (sequences.TryGetValue(numSeq, out long value) && result < value)
                            {
                                result = value;
                            }
                        }
                    }
                    numSeq = (numSeq << 5) & 0b11111111111111111111;
                }
            });  

            return result;
        }
    }
}
