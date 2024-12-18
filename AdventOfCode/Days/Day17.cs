using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day17
    {
        public static string Part1()
        {
            string result = "";
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day17.1.txt");
            int a = int.Parse(inputs[0][12..]);
            int b = int.Parse(inputs[1][12..]);
            int c = int.Parse(inputs[2][12..]);
            List<int> program = inputs[4][9..].Split(',').Select(int.Parse).ToList();
            

            result = RunProgram(a, b, c, program);
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day17.1.txt");
            List<int> program = inputs[4][9..].Split(',').Select(int.Parse).ToList();
            List<int> reverseProgram = new(program);
            reverseProgram.Reverse();
            program = program[.. ^2];

            for (int i = 0; i < reverseProgram.Count; i++)
            {
                bool failed = true;
                for (int j = 0; j < 8; j++)
                {
                    if (RunProgramOnce((result << 3) + j, 0, 0, program) == reverseProgram[i])
                    {
                        failed = false;
                        result = (result << 3) + j;
                        break;
                    }
                    if (j == 7 && failed)
                    {
                        while (j == 7)
                        {
                            i--;
                            j = (int)(result % 8);
                            result >>= 3;
                        }
                    }
                }
            }
            return result;
        }

        private static string RunProgram(int a, int b, int c, List<int> program)
        {
            List<int> output = [];
            int programIndex = 0;
            while (programIndex < program.Count)
            {
                switch (program[programIndex])
                {
                    case 0:
                        a = (int)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                    case 1:
                        b = b ^ program[programIndex + 1];
                        break;
                    case 2:
                        b = GetCombo(program[programIndex + 1], a, b, c) % 8;
                        break;
                    case 3:
                        if (a != 0)
                        {
                            programIndex = program[programIndex + 1];
                            continue;
                        }
                        break;
                    case 4:
                        b = b ^ c;
                        break;
                    case 5:
                        output.Add(GetCombo(program[programIndex + 1], a, b, c) % 8);
                        break;
                    case 6:
                        b = (int)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                    case 7:
                        c = (int)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                }
                programIndex += 2;
            }
            return string.Join(',', output);
        }

        private static long RunProgramOnce(long a, long b, long c, List<int> program)
        {
            List<int> output = [];
            int programIndex = 0;
            while (programIndex < program.Count)
            {
                switch (program[programIndex])
                {
                    case 0:
                        a = (long)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                    case 1:
                        b = b ^ program[programIndex + 1];
                        break;
                    case 2:
                        b = GetCombo(program[programIndex + 1], a, b, c) % 8;
                        break;
                    case 3:
                        if (a != 0)
                        {
                            programIndex = program[programIndex + 1];
                            continue;
                        }
                        break;
                    case 4:
                        b = b ^ c;
                        break;
                    case 5:
                        return GetCombo(program[programIndex + 1], a, b, c) % 8;
                    case 6:
                        b = (long)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                    case 7:
                        c = (long)(a / Math.Pow(2, GetCombo(program[programIndex + 1], a, b, c)));
                        break;
                }
                programIndex += 2;
            }
            return -1;
        }

        private static int GetCombo(int operand, int a, int b, int c)
        {
            return operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                _ => throw new NotImplementedException()
            };
        }

        private static long GetCombo(int operand, long a, long b, long c)
        {
            return operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                _ => throw new NotImplementedException()
            };
        }
    }
}
