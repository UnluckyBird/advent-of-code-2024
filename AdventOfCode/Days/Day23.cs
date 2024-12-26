using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day23
    {
        public static int Part1()
        {
            int result = 0;
            string[] inputs = File.ReadAllLines(AppContext.BaseDirectory + "\\Data\\Day23.1.txt");
            Dictionary<string, HashSet<string>> connections = [];
            ConcurrentDictionary<string, int> parties = [];

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
            
            Parallel.ForEach(connections, (connection) =>
            {
                var p = IsPartyOfThree([connection.Key], connection.Key, connections);
                p.ForEach(x =>
                {
                    if (x.Any(y => y[0] == 't'))
                    {
                        parties.TryAdd(string.Join("", x), 0);
                    }
                });
            });
            
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
            Parallel.ForEach(connections, (connection) =>
            {
                SortedSet<string> party = LargestParty([], connection.Key, connections, []);
                if (party.Count > largest.Count)
                {
                    Interlocked.Exchange(ref largest, party);
                }
            });

            result = string.Join(',', largest);
            return result;
        }

        private static List<SortedSet<string>> IsPartyOfThree(SortedSet<string> connection, string check, Dictionary<string, HashSet<string>> connections)
        {
            if (connection.Count == 3)
            {
                return [connection];
            }

            List<SortedSet<string>> allThrees = [];
            HashSet<string> checkConnections = connections[check];
            foreach (string checkConn in checkConnections)
            {
                HashSet<string> hsh = connections[checkConn];
                if (connection.All(hsh.Contains))
                {
                    allThrees.AddRange(IsPartyOfThree([.. connection, checkConn], checkConn, connections));
                }
            }
            return allThrees;
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
