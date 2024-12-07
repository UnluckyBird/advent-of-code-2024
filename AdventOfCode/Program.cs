using AdventOfCode.Days;
using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = Stopwatch.GetTimestamp();
            Console.WriteLine("********************************************************");
            Console.WriteLine("** ADVENT OF CODE 2024 BY MARTIN DE FRIES JUSTINUSSEN **");
            Console.WriteLine("********************************************************");
            //Day 1
            Console.WriteLine("Day 1.1: " + Day1.Part1());
            Console.WriteLine("Day 1.2: " + Day1.Part2());
            Console.WriteLine($"Day 1 Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

            //Day 2
            var startDay2Time = Stopwatch.GetTimestamp();
            Console.WriteLine("Day 2.1: " + Day2.Part1());
            Console.WriteLine("Day 2.2: " + Day2.Part2());
            Console.WriteLine($"Day 2 Execution Time: {Stopwatch.GetElapsedTime(startDay2Time).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

            //Day 3
            var startDay3Time = Stopwatch.GetTimestamp();
            Console.WriteLine("Day 3.1: " + Day3.Part1());
            Console.WriteLine("Day 3.2: " + Day3.Part2());
            Console.WriteLine($"Day 3 Execution Time: {Stopwatch.GetElapsedTime(startDay3Time).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

            //Day 5
            var startDay5Time = Stopwatch.GetTimestamp();
            Console.WriteLine("Day 5.1: " + Day5.Part1());
            Console.WriteLine("Day 5.2: " + Day5.Part2());
            Console.WriteLine($"Day 5 Execution Time: {Stopwatch.GetElapsedTime(startDay5Time).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

            //Day 6
            var startDay6Time = Stopwatch.GetTimestamp();
            Console.WriteLine("Day 6.1: " + Day6.Part1());
            Console.WriteLine("Day 6.2: " + Day6.Part2());
            Console.WriteLine($"Day 6 Execution Time: {Stopwatch.GetElapsedTime(startDay6Time).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");

            //Day 7
            var startDay7Time = Stopwatch.GetTimestamp();
            Console.WriteLine("Day 7.1: " + Day7.Part1());
            Console.WriteLine("Day 7.2: " + Day7.Part2());
            Console.WriteLine($"Day 7 Execution Time: {Stopwatch.GetElapsedTime(startDay7Time).TotalMilliseconds} ms");
            Console.WriteLine($"Total Execution Time: {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");
        }
    }
}
