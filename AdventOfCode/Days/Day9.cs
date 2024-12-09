using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Days
{
    public class Day9
    {
        public static long Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day9.1.txt");
            string input = inputs[0];
            List<int> memory = [];
            Stack<int> usedMemory = [];
            List<int> freeMemoryPos = [];
            int id = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < int.Parse(input[i].ToString()); j++)
                    {
                        usedMemory.Push(id);
                        memory.Add(id);
                    }
                    id++;
                }
                else
                {
                    for (int j = 0; j < int.Parse(input[i].ToString()); j++)
                    {
                        memory.Add(-1);
                        freeMemoryPos.Add(memory.Count-1);
                    }
                }
            }
            int totalUsedMemory = usedMemory.Count;
            foreach (int free in freeMemoryPos)
            {
                int lastMemory = usedMemory.Pop();
                memory[free] = lastMemory;
            }
            memory = memory[0..totalUsedMemory];
            for (int i = 0;i < memory.Count; i++)
            {
                result += memory[i] * i;
            }
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines("C:\\Users\\UnluckyBird\\source\\repos\\AdventOfCode\\AdventOfCode2024\\AdventOfCode\\Data\\Day9.1.txt");
            string input = inputs[0];
            List<int> memory = [];
            Stack<(int,int,int)> usedMemory = [];
            List<(int,int)> freeMemoryPos = [];
            int id = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int num = int.Parse(input[i].ToString());
                if (i % 2 == 0)
                {
                    usedMemory.Push((id, memory.Count, num));
                    for (int j = 0; j < num; j++)
                    {
                        memory.Add(id);
                    }
                    id++;
                }
                else
                {
                    freeMemoryPos.Add((memory.Count, num));
                    for (int j = 0; j < num; j++)
                    {
                        memory.Add(-1);
                    }
                }
            }

            while (usedMemory.Count > 0)
            {
                (int memid, int usedMemoryPos, int usedAmount) = usedMemory.Pop();
                for (int i = 0; i < freeMemoryPos.Count; i++)
                {
                    if (freeMemoryPos[i].Item1 > usedMemoryPos)
                    {
                        break;
                    }
                    if (freeMemoryPos[i].Item2 >= usedAmount) 
                    {
                        for (int j = 0; j < usedAmount; j++)
                        {
                            memory[freeMemoryPos[i].Item1 + j] = memid;
                            memory[usedMemoryPos + j] = -1;
                        }
                        freeMemoryPos[i] = (freeMemoryPos[i].Item1 + usedAmount, freeMemoryPos[i].Item2 - usedAmount);
                        break;
                    }
                }
            }
            
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] != -1)
                {
                    result += memory[i] * i;
                }
            }
            return result;
        }
    }
}
