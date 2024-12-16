using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day13
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day13.1.txt");

            for (int i = 0; i < inputs.Length; i += 4)
            {
                var buttonA = inputs[i].Substring(12).Split(", Y+").Select(int.Parse).ToArray();
                var buttonB = inputs[i + 1].Substring(12).Split(", Y+", StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
                var target = inputs[i + 2].Substring(9).Split(", Y=").Select(int.Parse).ToArray();
                int a = ((target[0] * buttonB[1]) - (target[1] * buttonB[0])) / ((buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]));
                int b = ((target[1] * buttonA[0]) - (target[0] * buttonA[1])) / ((buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]));
                if (buttonA[0] * a + buttonB[0] * b == target[0] && buttonA[1] * a + buttonB[1] * b == target[1])
                {
                    result += 3 * a + b;
                }

            }
            return result;
        }

        public static long Part2()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day13.1.txt");

            for (int i = 0; i < inputs.Length; i += 4)
            {
                var buttonA = inputs[i].Substring(12).Split(", Y+").Select(int.Parse).ToArray();
                var buttonB = inputs[i + 1].Substring(12).Split(", Y+", StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
                var target = inputs[i + 2].Substring(9).Split(", Y=").Select(long.Parse).ToArray();
                target[0] += 10000000000000;
                target[1] += 10000000000000;
                long a = ((target[0] * buttonB[1]) - (target[1] * buttonB[0])) / ((buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]));
                long b = ((target[1] * buttonA[0]) - (target[0] * buttonA[1])) / ((buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]));
                if (buttonA[0]*a + buttonB[0]*b == target[0] && buttonA[1] * a + buttonB[1] * b == target[1])
                {
                    result += 3*a + b;
                } 
            }
            return result;
        }
    }
}
