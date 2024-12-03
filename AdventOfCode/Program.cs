using AdventOfCode.Days;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            Console.WriteLine("********************************************************");
            Console.WriteLine("** ADVENT OF CODE 2024 BY MARTIN DE FRIES JUSTINUSSEN **");
            Console.WriteLine("********************************************************");
            //Day 1
            watch.Start();
            Console.WriteLine("Day 1.1: " + Day1.Part1());
            Console.WriteLine("Day 1.2: " + Day1.Part2());
            watch.Stop();
            Console.WriteLine($"Day 1 Execution Time: {watch.ElapsedMilliseconds} ms");

            //Day 2
            watch.Start();
            Console.WriteLine("Day 2.1: " + Day2.Part1());
            Console.WriteLine("Day 2.2: " + Day2.Part2());
            watch.Stop();
            Console.WriteLine($"Day 2 Execution Time: {watch.ElapsedMilliseconds} ms");

            //Day 3
            watch.Start();
            Console.WriteLine("Day 3.1: " + Day3.Part1());
            Console.WriteLine("Day 3.2: " + Day3.Part2());
            watch.Stop();
            Console.WriteLine($"Day 3 Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
