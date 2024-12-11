using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day5
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day5.1.txt");
            bool fillOrdering = false;
            Dictionary<int, List<int>> rules = [];
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    fillOrdering = true;
                }
                else if (!fillOrdering)
                {
                    List<int> pages = input.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                    if (rules.TryGetValue(pages[0], out var value))
                    {
                        value.Add(pages[1]);
                    }
                    else
                    {
                        rules.Add(pages[0], [pages[1]]);
                    }
                }
                else
                {
                    List<int> update = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    bool failed = false;
                    for (int i = update.Count-1; i >= 0 ; i--) 
                    {

                        if (!rules.TryGetValue(update[i], out var value))
                        {
                            continue;
                        }
                        for (int j = i-1; j >= 0; j--)
                        {
                            if (value.Contains(update[j]))
                            {
                                failed = true;
                                break;
                            }
                        } 
                        if (failed)
                        {
                            break;
                        }
                    }
                    if (!failed)
                    {
                        result += update[(update.Count / 2)];
                    }
                }
            }
            return result;
        }

        public static int Part2()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day5.1.txt");

            bool fillOrdering = false;
            Dictionary<int, List<int>> rules = [];
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    fillOrdering = true;
                }
                else if (!fillOrdering)
                {
                    List<int> pages = input.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                    if (rules.TryGetValue(pages[0], out var value))
                    {
                        value.Add(pages[1]);
                    }
                    else
                    {
                        rules.Add(pages[0], [pages[1]]);
                    }
                }
                else
                {
                    List<int> update = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    bool failed = false;
                    for (int i = update.Count - 1; i >= 0; i--)
                    {

                        if (!rules.TryGetValue(update[i], out var value))
                        {
                            continue;
                        }
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (value.Contains(update[j]))
                            {
                                int move = update[j];
                                update.RemoveAt(j);
                                update.Insert(i, move);
                                failed = true;
                                i++;
                                break;
                            }
                        }
                    }
                    if (failed)
                    {
                        result += update[(update.Count / 2)];
                    }
                }
            }
            return result;
        }
    }
}
