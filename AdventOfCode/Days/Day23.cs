using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day23
    {
        public static long Part1()
        {
            long result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day23.1.txt");
            Dictionary<string, List<string>> connections = [];
            HashSet<string> parties = [];

            foreach (string input in inputs)
            {
                if (connections.TryGetValue(input[0..2], out List<string> firstList))
                {
                    firstList.Add(input[3..5]);
                }
                else
                {
                    connections[input[0..2]] = [input[3..5]];
                }

                if (connections.TryGetValue(input[3..5], out List<string> secondList))
                {
                    secondList.Add(input[0..2]);
                }
                else
                {
                    connections[input[3..5]] = [input[0..2]];
                }
            }

            foreach (var connection in connections)
            {
                if (IsParty(connection.Key, 0 , 3, connections, out var party))
                {
                    foreach (var p in party)
                    {
                        var pa = p.Split(',').ToList();
                        if (pa.Any(x => x.StartsWith('t')))
                        {
                            pa.Sort();
                            parties.Add(string.Join("", pa));
                        }
                    }
                }
            }

            result = parties.Count;
            return result;
        }

        public static string Part2()
        {
            string result = "";
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day23.1.txt");
            Dictionary<string, HashSet<string>> connections = [];

            foreach (string input in inputs)
            {
                if (connections.TryGetValue(input[0..2], out HashSet<string> firstList))
                {
                    firstList.Add(input[3..5]);
                }
                else
                {
                    connections[input[0..2]] = [input[3..5]];
                }

                if (connections.TryGetValue(input[3..5], out HashSet<string> secondList))
                {
                    secondList.Add(input[0..2]);
                }
                else
                {
                    connections[input[3..5]] = [input[0..2]];
                }
            }

            SortedSet<string> largest = [];
            foreach (var connection in connections)
            {
                SortedSet<string> party = LargestParty([], connection.Key, connections, []);
                if (party.Count > largest.Count)
                {
                    largest = party;
                }
            }

            result = string.Join(',', largest);
            return result;
        }

        private static bool IsParty(string connection, int depth, int endDepth, Dictionary<string, List<string>> connections, out List<string> party)
        {
            if (depth == endDepth && connection[0..2] == connection[^2..])
            {
                party = [connection[..^3]];
                return true;
            }
            else if (depth >= endDepth)
            {
                party = [];
                return false;
            }

            party = [];
            bool isParty = false;
            foreach (var input in connections[connection[^2..]])
            {
                if (IsParty(connection + "," + input, depth + 1, endDepth, connections, out var list))
                {
                    party.AddRange(list);
                    isParty = true;
                }
            }
            return isParty;
        }

        private static SortedSet<string> LargestParty(SortedSet<string> connection, string check, Dictionary<string, HashSet<string>> connections, HashSet<string> checkedNodes)
        {
            SortedSet<string> result = connection;
            if (connection.Contains(check) == false && checkedNodes.Add(check))
            {
                HashSet<string> hsh = connections[check];
                if (connection.All(hsh.Contains))
                {
                    connection.Add(check);
                    foreach (string h in hsh)
                    {
                        SortedSet<string> largest = LargestParty([.. connection], h, connections, checkedNodes);
                        if (largest.Count > result.Count)
                        {
                            result = largest;
                        }
                    }
                }
            }
            return result;
        }
    }
}
