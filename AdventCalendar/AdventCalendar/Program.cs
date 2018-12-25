using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            //Problem1(@"..\..\problem1.txt");
            //Problem2(@"..\..\problem2.txt");
            //Problem3(@"..\..\problem3.txt");
            //Problem4(@"..\..\problem4.txt");
            //Problem5(@"..\..\problem5.txt");
            //Problem6(@"..\..\problem6.txt");
            //Problem7(@"..\..\problem7.txt");
            //Problem8(@"..\..\problem8.txt");
            //Problem9(@"..\..\problem9.txt");
            //Problem10(@"..\..\problem10.txt");
            //Problem11(@"..\..\problem11.txt");
            //Problem12(@"..\..\problem12.txt");
            //Problem13(@"..\..\problem13.txt");
            //Problem14(@"..\..\problem14.txt");
            //Problem15(@"..\..\problem15.txt");
            //Problem16(@"..\..\problem16.txt");
            //Problem17(@"..\..\problem17.txt");
            //Problem18(@"..\..\problem18.txt");
            //Problem19(@"..\..\problem19.txt");
            //Problem20(@"..\..\problem20.txt");
            //Problem21(@"..\..\problem21.txt");
            //Problem22(@"..\..\problem22.txt");
            //Problem23(@"..\..\problem23.txt");
            //Problem24(@"..\..\problem24.txt");
            Problem25(@"..\..\problem25.txt");
            Console.ReadLine();
        }

        /// <summary>
        /// DAY 1
        /// Part 1: Sum the rows of numbers.
        /// Part 2: Find the first repeting number in the sequence while you sum the rows of numbers.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem1(string __input)
        {
            var line = File.ReadAllLines(__input);

            int freq = 0;
            foreach (var l in line)
            {
                freq += int.Parse(l);
            }
            int freq1 = freq;

            line = File.ReadAllLines(__input);
            freq = 0;
            int duplicate = int.MaxValue;
            Dictionary<int, int> freqs = new Dictionary<int, int>();
            while (duplicate == int.MaxValue)
            {
                foreach (var l in line)
                {
                    freq += int.Parse(l);
                    if (!freqs.ContainsKey(freq))
                        freqs.Add(freq, 1);
                    else
                    {
                        duplicate = freq;
                        break;
                    }
                }
            }

            Console.WriteLine("Day 1, Problem 1: " + freq1.ToString());
            Console.WriteLine("Day 1, Problem 2: " + duplicate.ToString());
        }

        /// <summary>
        /// DAY 2
        /// Part 1: Find words with double or triple letter counts, multiply for checksum.
        /// Part 2: Find the two words that differ by 1 letter, what is letters in common?
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem2(string __input)
        {
            var line = File.ReadAllLines(__input);

            int doubles = 0;
            int triples = 0;
            foreach (var l in line)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                foreach (var c in l.ToCharArray())
                {
                    if (!letters.ContainsKey(c))
                        letters.Add(c, 1);
                    else
                        letters[c]++;
                }
                bool d = false, t = false;
                foreach (var v in letters.Values)
                {
                    if (v == 2 && !d)
                    {
                        doubles++;
                        d = true;
                    }
                    else if (v == 3 && !t)
                    {
                        triples++;
                        t = true;
                    }
                }
            }

            string match = "";
            int diffs = 0;
            foreach (var l in line)
            {
                foreach (var k in line)
                {
                    diffs = 0;
                    for (int i = 0; i < l.Length; i++)
                    {
                        if (l.ToCharArray()[i] != k.ToCharArray()[i])
                        {
                            diffs++;
                            match = k.Remove(i, 1);
                        }
                    }
                    if (diffs == 1)
                        break;
                }
                if (diffs == 1)
                    break;
            }

            Console.WriteLine("Day 2, Problem 1: " + (doubles * triples).ToString());
            Console.WriteLine("Day 2, Problem 2: " + match);
        }

        /// <summary>
        /// DAY 3
        /// Part 1: Count the number of overlapping squares.
        /// Part 2: Find the single claim that is not overlapping.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem3(string __input)
        {
            var line = File.ReadAllLines(__input);

            int[,] table = new int[1100, 1100];
            char[] delims = { '#', ' ', '@', ',', ':', 'x' };
            int sum = 0;
            foreach (var l in line)
            {
                var r = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < int.Parse(r[3]); i++)
                {
                    for (int j = 0; j < int.Parse(r[4]); j++)
                    {
                        table[i + int.Parse(r[1]), j + int.Parse(r[2])]++;
                    }
                }
            }
            for (int i = 0; i < 1100; i++)
            {
                for (int j = 0; j < 1100; j++)
                {
                    if (table[i, j] > 1)
                        sum++;
                }
            }

            string no_overlap = "";
            foreach (var l in line)
            {
                var r = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                bool overlap = false;
                for (int i = 0; i < int.Parse(r[3]); i++)
                {
                    for (int j = 0; j < int.Parse(r[4]); j++)
                    {
                        if (table[i + int.Parse(r[1]), j + int.Parse(r[2])] != 1)
                        {
                            overlap = true;
                            break;
                        }
                    }
                    if (overlap)
                        break;
                }
                if (!overlap)
                    no_overlap = r[0];
            }

            Console.WriteLine("Day 3, Problem 1: " + sum.ToString());
            Console.WriteLine("Day 3, Problem 2: " + no_overlap);
        }

        /// <summary>
        /// DAY 4
        /// Part 1: Sort the list, find the number that is asleep the most. What minute overlaps the most.
        /// Part 2: What minute is slept the most.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem4(string __input)
        {
            var line = File.ReadAllLines(__input);

            char[] delims = { ' ', '[', ']', '#', '-', ':' };
            SortedDictionary<DateTime, string> times = new SortedDictionary<DateTime, string>();
            Dictionary<string, int> guards = new Dictionary<string, int>();
            foreach (var l in line)
            {
                var s = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                var date = new DateTime(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]), int.Parse(s[4]), 0);
                var action = s[6];
                if (!(action.Equals("up") || action.Equals("asleep")) && !guards.ContainsKey(action))
                    guards.Add(action, 0);
                times.Add(date, action);
            }

            Dictionary<string, int[]> sleep = new Dictionary<string, int[]>();
            foreach (var g in guards)
                sleep.Add(g.Key, new int[60]);

            string guard = "";
            int asleep = -1, up = -1;
            foreach (var t in times)
            {
                if (t.Value.Equals("up"))
                    up = t.Key.Minute;
                else if (t.Value.Equals("asleep"))
                    asleep = t.Key.Minute;
                else
                    guard = t.Value;

                if (asleep != -1 && up != -1)
                {
                    for (int i = asleep; i < up; i++)
                        sleep[guard][i]++;

                    asleep = -1;
                    up = -1;
                }
            }

            string guard_max = "";
            int max_sleep = 0;
            int guard_minute = 0;
            foreach (var s in sleep)
            {
                if (s.Value.Sum() > max_sleep)
                {
                    max_sleep = s.Value.Sum();
                    guard_max = s.Key;
                    guard_minute = s.Value.ToList().IndexOf(s.Value.Max());
                }
            }

            int max_sleep_minute = 0;
            int max_sleep_count = 0;
            string max_sleep_guard = "";
            foreach (var s in sleep)
            {
                if (s.Value.Max() > max_sleep_count)
                {
                    max_sleep_count = s.Value.Max();
                    max_sleep_guard = s.Key;
                    max_sleep_minute = s.Value.ToList().IndexOf(s.Value.Max());
                }
            }

            Console.WriteLine("Day 4, Problem 1: " + (int.Parse(guard_max) * guard_minute).ToString());
            Console.WriteLine("Day 4, Problem 2: " + (int.Parse(max_sleep_guard) * max_sleep_minute).ToString());
        }

        /// <summary>
        /// DAY 5
        /// Part 1: Use a stack to clean up matching pairs of lower/Upper case letters. What is the length?
        /// Part 2: Clean up as in part 1, plus remove all occurances of a single character ignoring case.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem5(string __input)
        {
            var line = File.ReadAllLines(__input);

            var polymer = line[0].ToCharArray();
            var chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            Stack<char> s = new Stack<char>();
            s.Push(' ');

            foreach (var c in polymer)
            {
                if (char.IsUpper(c))
                    if (s.Peek().Equals(char.ToLower(c)))
                        s.Pop();
                    else
                        s.Push(c);
                else if (char.IsLower(c))
                    if (s.Peek().Equals(char.ToUpper(c)))
                        s.Pop();
                    else
                        s.Push(c);
            }
            var p1_count = s.Count - 1;

            int shortest = int.MaxValue;
            foreach (var l in chars)
            {
                s.Clear();
                s.Push(' ');
                foreach (var c in polymer)
                {
                    if (!char.ToLower(c).Equals(char.ToLower(l)))
                    {
                        if (char.IsUpper(c))
                        {
                            if (s.Peek().Equals(char.ToLower(c)))
                                s.Pop();
                            else
                                s.Push(c);
                        }
                        else if (char.IsLower(c))
                        {
                            if (s.Peek().Equals(char.ToUpper(c)))
                                s.Pop();
                            else
                                s.Push(c);
                        }
                    }
                }
                if (s.Count - 1 < shortest)
                    shortest = s.Count - 1;
            }

            Console.WriteLine("Day 5, Problem 1: " + p1_count.ToString());
            Console.WriteLine("Day 5, Problem 2: " + shortest.ToString());
        }

        /// <summary>
        /// DAY 6
        /// Part 1: Find size of largest area closest to point.
        /// Part 2: Find total area that has all points within distance.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem6(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ', ',' };
            int grid_size = 400;
            string[,] grid = new string[grid_size, grid_size];
            for (int i = 0; i < grid_size; i++)
                for (int j = 0; j < grid_size; j++)
                    grid[i, j] = "";
            Dictionary<string, int> size = new Dictionary<string, int>();
            for (int i = 0; i < line.Count(); i++)
            {
                var l = line[i].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                grid[int.Parse(l[0]), int.Parse(l[1])] = "A" + i.ToString() + ",0";
                size.Add("A" + i.ToString(), 0);
            }

            for (int distance = 1; distance < grid_size; distance++)
            {
                foreach (var l in line)
                {
                    var loc = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    var x = int.Parse(loc[0]);
                    var y = int.Parse(loc[1]);
                    var name = grid[x, y].Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];

                    int i, j;

                    i = x - distance;
                    for (j = y - distance; j <= y + distance; j++)
                    {
                        var d = Math.Abs(x - i) + Math.Abs(y - j);
                        if (i < 0 || i >= grid_size || j < 0 || j >= grid_size || d == 0)
                            continue;
                        var g = grid[i, j];
                        if (string.IsNullOrEmpty(g))
                            grid[i, j] = name + "," + d.ToString();
                        else if (int.Parse(g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]) > d)
                            grid[i, j] = name + "," + d.ToString();
                        else if (g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1].Equals(d.ToString())
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals("..")
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals(name))
                            grid[i, j] = "..," + d.ToString();
                    }

                    i = x + distance;
                    for (j = y - distance; j <= y + distance; j++)
                    {
                        var d = Math.Abs(x - i) + Math.Abs(y - j);
                        if (i < 0 || i >= grid_size || j < 0 || j >= grid_size || d == 0)
                            continue;
                        var g = grid[i, j];
                        if (string.IsNullOrEmpty(g))
                            grid[i, j] = name + "," + d.ToString();
                        else if (int.Parse(g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]) > d)
                            grid[i, j] = name + "," + d.ToString();
                        else if (g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1].Equals(d.ToString())
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals("..")
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals(name))
                            grid[i, j] = "..," + d.ToString();
                    }

                    j = y - distance;
                    for (i = x - distance; i <= x + distance; i++)
                    {
                        var d = Math.Abs(x - i) + Math.Abs(y - j);
                        if (i < 0 || i >= grid_size || j < 0 || j >= grid_size || d == 0)
                            continue;
                        var g = grid[i, j];
                        if (string.IsNullOrEmpty(g))
                            grid[i, j] = name + "," + d.ToString();
                        else if (int.Parse(g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]) > d)
                            grid[i, j] = name + "," + d.ToString();
                        else if (g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1].Equals(d.ToString())
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals("..")
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals(name))
                            grid[i, j] = "..," + d.ToString();
                    }

                    j = y + distance;
                    for (i = x - distance; i <= x + distance; i++)
                    {
                        var d = Math.Abs(x - i) + Math.Abs(y - j);
                        if (i < 0 || i >= grid_size || j < 0 || j >= grid_size || d == 0)
                            continue;
                        var g = grid[i, j];
                        if (string.IsNullOrEmpty(g))
                            grid[i, j] = name + "," + d.ToString();
                        else if (int.Parse(g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]) > d)
                            grid[i, j] = name + "," + d.ToString();
                        else if (g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[1].Equals(d.ToString())
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals("..")
                            && !g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0].Equals(name))
                            grid[i, j] = "..," + d.ToString();
                    }
                }
            }

            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    var g = grid[i, j];
                    var n = g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (!n.Equals(".."))
                        size[n]++;
                }
            }

            for (int j = 0; j < grid_size; j++)
            {
                int i = 0;
                var g = grid[i, j];
                var n = g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
                if (!n.Equals(".."))
                    size[n] = 0;
                g = grid[j, i];
                n = g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
                if (!n.Equals(".."))
                    size[n] = 0;

                i = grid_size - 1;
                g = grid[i, j];
                n = g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
                if (!n.Equals(".."))
                    size[n] = 0;
                i = grid_size - 1;
                g = grid[j, i];
                n = g.Split(delims, StringSplitOptions.RemoveEmptyEntries)[0];
                if (!n.Equals(".."))
                    size[n] = 0;
            }

            var max = 0;
            foreach (var kv in size)
                if (kv.Value > max)
                    max = kv.Value;

            var max_distance = 10000;
            var region_size = 0;
            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    var curr_distance = 0;
                    foreach (var l in line)
                    {
                        var loc = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                        var x = int.Parse(loc[0]);
                        var y = int.Parse(loc[1]);
                        curr_distance += Math.Abs(i - x) + Math.Abs(j - y);
                    }
                    if (curr_distance < max_distance)
                        region_size++;
                }
            }

            Console.WriteLine("Day 6, Problem 1: " + max);
            Console.WriteLine("Day 6, Problem 2: " + region_size);
        }

        /// <summary>
        /// DAY 7
        /// Part 1: Print the order you traverse the graph.
        /// Part 2: How long does it take to traverse the graph with multiple users.
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem7(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            SortedList<string, List<string>> reqs = new SortedList<string, List<string>>();
            string order = "";
            foreach (var l in line)
            {
                var c = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (!reqs.ContainsKey(c[1]))
                    reqs.Add(c[1], new List<string>());

                if (!reqs.ContainsKey(c[7]))
                    reqs.Add(c[7], new List<string> { c[1] });
                else
                    reqs[c[7]].Add(c[1]);

            }

            while (reqs.Count != 0)
            {
                foreach (var r in reqs)
                {
                    if (r.Value.Count == 0)
                    {
                        var step = r.Key;
                        order += step;
                        reqs.Remove(r.Key);
                        foreach (var s in reqs)
                            s.Value.RemoveAll(item => item.Equals(step));
                        break;
                    }
                }
            }

            foreach (var l in line)
            {
                var c = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (!reqs.ContainsKey(c[1]))
                    reqs.Add(c[1], new List<string>());

                if (!reqs.ContainsKey(c[7]))
                    reqs.Add(c[7], new List<string> { c[1] });
                else
                    reqs[c[7]].Add(c[1]);

            }

            var workers = 5;
            var processing = 0;
            var t = 0;
            string order2 = "";
            Dictionary<string, int> timer = new Dictionary<string, int>();
            while (reqs.Count != 0)
            {
                for (int i = 0; i < timer.Count; i++)
                {
                    var s = timer.ElementAt(i);
                    timer[s.Key] = s.Value - 1;
                    if (timer[s.Key] == 0)
                    {
                        processing--;
                        i--;
                        order2 += s.Key;
                        foreach (var r in reqs)
                            r.Value.RemoveAll(item => item.Equals(s.Key));
                        timer.Remove(s.Key);
                    }
                }

                for (int i = 0; i < reqs.Count; i++)
                {
                    var r = reqs.ElementAt(i);
                    if (processing == workers)
                        break;
                    if (r.Value.Count == 0)
                    {
                        var step = r.Key;
                        reqs.Remove(r.Key);
                        processing++;
                        timer.Add(step, char.ToUpper(step[0]) - 64 + 60);
                    }
                }
                t++;
            }

            Console.WriteLine("Day 7, Problem 1: " + order);
            Console.WriteLine("Day 7, Problem 2: " + (t + timer.ElementAt(0).Value - 1).ToString());
        }

        /// <summary>
        /// DAY 8
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem8(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            var nodes = line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries);

            Stack<int> children = new Stack<int>();
            Stack<int> metadata = new Stack<int>();
            int total = 0;

            int i = 0;

            children.Push(int.Parse(nodes[i++]));
            metadata.Push(int.Parse(nodes[i++]));
            var root = new Node(null);
            var n = root;
            for (; i < nodes.Length;)
            {
                if (children.Peek() != 0)
                {
                    children.Push(children.Pop() - 1);
                    children.Push(int.Parse(nodes[i++]));
                    metadata.Push(int.Parse(nodes[i++]));
                    var c = new Node(n);
                    n.Children.Add(c);
                    n = c;
                }
                if (children.Peek() == 0)
                {
                    children.Pop();
                    int m_count = metadata.Pop();
                    var v = 0;
                    for (int j = 0; j < m_count; j++)
                    {
                        v += int.Parse(nodes[i + j]);
                        n.Metadata.Add(int.Parse(nodes[i + j]));
                    }
                    total += v;
                    if (n.Children.Count == 0)
                        n.Value += v;
                    i += m_count;
                    n = n.Parent;
                }
            }


            Console.WriteLine("Day 8, Problem 1: " + total);
            Console.WriteLine("Day 8, Problem 2: " + root.CalcValue());
        }

        /// <summary>
        /// Helper class for building and traversing tree for Day 8.
        /// </summary>
        private class Node
        {
            public List<Node> Children { get; set; }
            public List<int> Metadata { get; set; }
            public Node Parent { get; set; }
            public int Value { get; set; }
            public Node(Node parent)
            {
                Children = new List<Node>();
                Metadata = new List<int>();
                Value = 0;
                Parent = parent;
            }

            public int CalcValue()
            {
                var v = 0;
                if (Children.Count == 0)
                    v = Value;
                else
                {
                    foreach (var m in Metadata)
                        if (m <= Children.Count)
                            v += Children[m - 1].CalcValue();
                    Value = v;
                }
                return v;
            }
        }

        /// <summary>
        /// DAY 9
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem9(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };

            var players = int.Parse(line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[0]);
            var marbles = int.Parse(line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[6]);

            var scores = new long[players];
            var circle = new LinkedList<long>();
            var current = circle.AddFirst(0);

            for (int i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    scores[i % players] += i;
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? circle.Last;
                    }
                    scores[i % players] += current.Value;
                    var remove = current;
                    current = remove.Next;
                    circle.Remove(remove);
                }
                else
                {
                    current = circle.AddAfter(current.Next ?? circle.First, i);
                }
            }

            Console.WriteLine("Day 9, Problem 1: " + scores.Max());

            marbles *= 100;
            scores = new long[players];
            circle = new LinkedList<long>();
            current = circle.AddFirst(0);

            for (int i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    scores[i % players] += i;
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? circle.Last;
                    }
                    scores[i % players] += current.Value;
                    var remove = current;
                    current = remove.Next;
                    circle.Remove(remove);
                }
                else
                {
                    current = circle.AddAfter(current.Next ?? circle.First, i);
                }
            }
            Console.WriteLine("Day 9, Problem 2: " + scores.Max());
        }

        /// <summary>
        /// DAY 10
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem10(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ',', ' ', '<', '>' };
            char[] d = { };
            List<Point> p = new List<Point>();
            List<Point> v = new List<Point>();

            foreach (var l in line)
            {
                var s = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                p.Add(new Point(int.Parse(s[1]), int.Parse(s[2])));
                v.Add(new Point(int.Parse(s[4]), int.Parse(s[5])));
            }

            int x_min = p.Min(x => x.X);
            int x_max = p.Max(x => x.X);
            int y_min = p.Min(x => x.Y);
            int y_max = p.Max(x => x.Y);

            for (int t = 1; ; t++)
            {
                var last = p.Select(x => new Point(x.X, x.Y)).ToList();
                for (int i = 0; i < p.Count; i++)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }

                var x_new_min = p.Min(x => x.X);
                var x_new_max = p.Max(x => x.X);
                var y_new_min = p.Min(x => x.Y);
                var y_new_max = p.Max(x => x.Y);
                if ((x_new_max - x_new_min) > (x_max - x_min) && (y_new_max - y_new_min) > (y_max - y_min))
                {
                    Console.WriteLine("Day 10, Problem 1: ");
                    for (var i = y_new_min; i <= y_new_max; i++)
                    {
                        for (var j = x_new_min; j <= x_new_max; j++)
                        {
                            Console.Write(last.Any(x => x.Y == i && x.X == j) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Day 10, Problem 2: " + (t - 1).ToString());
                    break;
                }
                x_min = x_new_max;
                x_max = x_new_max;
                y_min = y_new_min;
                y_max = y_new_max;
            }
        }

        /// <summary>
        /// Helper class for Day 10 & others.
        /// </summary>
        private class Point
        {
            public int X
            { get; set; }
            public int Y
            { get; set; }
            public List<Point> Path
            { get; set; }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public Point(int x, int y, List<Point> path)
            {
                X = x;
                Y = y;
                Path = path;
            }

            public override bool Equals(object obj)
            {
                Point p = (Point)obj;
                return (X == p.X) && (Y == p.Y);
            }

            public override int GetHashCode()
            {
                return (X << 2) ^ Y;
            }
        }

        /// <summary>
        /// DAY 11
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem11(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            int serial = int.Parse(line[0]);
            int grid_size = 300;
            int[,] grid = new int[300, 300];

            for (int i = 1; i < grid_size; i++)
            {
                for (int j = 1; j < grid_size; j++)
                {
                    var rack_id = i + 10;
                    var power = rack_id * j;
                    power += serial;
                    power *= rack_id;
                    power = int.Parse(power.ToString().Substring(power.ToString().Length - 3, 1));
                    power -= 5;
                    grid[i - 1, j - 1] = power;
                }
            }

            int max = 0;
            string max_pos = "";
            var size = 3;
            for (int i = 0; i < grid_size - size; i++)
            {
                for (int j = 0; j < grid_size - size; j++)
                {
                    var val = 0;
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            val += grid[i + x, j + y];
                        }
                    }

                    if (val > max)
                    {
                        max = val;
                        max_pos = (i + 1).ToString() + "," + (j + 1).ToString();
                    }
                }
            }

            Console.WriteLine("Day 11, Problem 1: " + max_pos);

            for (size = 1; size <= 20; size++)//doesnt actually need to run to 300, takes forever if it trys
            {
                for (int i = 0; i < grid_size - size; i++)
                {
                    for (int j = 0; j < grid_size - size; j++)
                    {
                        var val = 0;
                        for (int x = 0; x < size; x++)
                        {
                            for (int y = 0; y < size; y++)
                            {
                                val += grid[i + x, j + y];
                            }
                        }

                        if (val > max)
                        {
                            max = val;
                            max_pos = (i + 1).ToString() + "," + (j + 1).ToString() + "," + size.ToString();
                        }
                    }
                }
            }

            Console.WriteLine("Day 11, Problem 2: " + max_pos);
        }

        /// <summary>
        /// DAY 12
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem12(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            int start = -5;
            string gen = "....." + line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[2] + ".....";


            Dictionary<string, string> rules = new Dictionary<string, string>();
            for (int i = 2; i < line.Length; i++)
            {
                var l = line[i].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(l[0], l[2]);
            }

            //Console.WriteLine("0: " + gen);

            int gen_20 = 0;
            long last_gen = 0;
            for (long i = 1; i <= 1000; i++)
            {
                var new_gen = "";
                for (int j = 2; j < gen.Length - 2; j++)
                {
                    var p = gen.Substring(j - 2, 5);
                    if (rules.ContainsKey(p))
                        new_gen += rules[p];
                    else
                        new_gen += ".";
                }
                var t = new_gen.Split(new char[] { '#' });
                new_gen = new_gen.Remove(0, t[0].Count());
                new_gen = new_gen.Remove(new_gen.Length - t[t.Length - 1].Count(), t[t.Length - 1].Count());
                gen = "....." + new_gen + ".....";
                start = start + 2 + t[0].Count() - 5;

                int plant = 0;
                var s = start;
                foreach (var c in gen.ToCharArray())
                {
                    if (c.Equals('#'))
                        plant += start;
                    start++;
                }
                start = s;
                if (i == 20)
                    gen_20 = plant;
                //Console.WriteLine(i.ToString() + ":" + (plant - last_gen).ToString());
                last_gen = plant;
            }
            last_gen = last_gen + 59 * (50000000000 - 1000);

            Console.WriteLine("Day 12, Problem 1: " + gen_20.ToString());
            Console.WriteLine("Day 12, Problem 2: " + last_gen.ToString());
        }

        /// <summary>
        /// DAY 13
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem13(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };

            List<List<char>> track = new List<List<char>>();
            foreach (var l in line)
                track.Add(l.ToList());

            string crash = "";
            List<Cart> carts = new List<Cart>();
            int num_carts = 0;
            for (int i = 0; i < track.Count; i++)
            {
                for (int j = 0; j < track[0].Count; j++)
                {
                    switch (track[i][j])
                    {
                        case '>':
                            carts.Add(new Cart(j, i, '>', num_carts++));
                            track[i][j] = '-';
                            break;
                        case 'v':
                            carts.Add(new Cart(j, i, 'v', num_carts++));
                            track[i][j] = '|';
                            break;
                        case '<':
                            carts.Add(new Cart(j, i, '<', num_carts++));
                            track[i][j] = '-';
                            break;
                        case '^':
                            carts.Add(new Cart(j, i, '^', num_carts++));
                            track[i][j] = '|';
                            break;
                    }
                }
            }

            int rem = carts.Count;
            while (rem > 1)
            {
                carts = carts.OrderBy(c => c.X).ThenBy(c => c.Y).ToList();
                foreach (var c in carts)
                {
                    if (c.Crashed == true)
                        continue;
                    switch (c.Dir)
                    {
                        case '>':
                            c.X++;
                            switch (track[c.Y][c.X])
                            {
                                case '-':
                                    break;
                                case '|':
                                    break;
                                case '\\':
                                    c.Dir = 'v';
                                    break;
                                case '/':
                                    c.Dir = '^';
                                    break;
                                case '+':
                                    switch (c.Turn)
                                    {
                                        case 0:
                                            c.Dir = '^';
                                            break;
                                        case 1:
                                            c.Dir = '>';
                                            break;
                                        case 2:
                                            c.Dir = 'v';
                                            break;
                                    }
                                    c.Turn = (c.Turn + 1) % 3;
                                    break;
                            }
                            break;
                        case 'v':
                            c.Y++;
                            switch (track[c.Y][c.X])
                            {
                                case '-':
                                    break;
                                case '|':
                                    break;
                                case '\\':
                                    c.Dir = '>';
                                    break;
                                case '/':
                                    c.Dir = '<';
                                    break;
                                case '+':
                                    switch (c.Turn)
                                    {
                                        case 0:
                                            c.Dir = '>';
                                            break;
                                        case 1:
                                            c.Dir = 'v';
                                            break;
                                        case 2:
                                            c.Dir = '<';
                                            break;
                                    }
                                    c.Turn = (c.Turn + 1) % 3;
                                    break;
                            }
                            break;
                        case '<':
                            c.X--;
                            switch (track[c.Y][c.X])
                            {
                                case '-':
                                    break;
                                case '|':
                                    break;
                                case '\\':
                                    c.Dir = '^';
                                    break;
                                case '/':
                                    c.Dir = 'v';
                                    break;
                                case '+':
                                    switch (c.Turn)
                                    {
                                        case 0:
                                            c.Dir = 'v';
                                            break;
                                        case 1:
                                            c.Dir = '<';
                                            break;
                                        case 2:
                                            c.Dir = '^';
                                            break;
                                    }
                                    c.Turn = (c.Turn + 1) % 3;
                                    break;
                            }
                            break;
                        case '^':
                            c.Y--;
                            switch (track[c.Y][c.X])
                            {
                                case '-':
                                    break;
                                case '|':
                                    break;
                                case '\\':
                                    c.Dir = '<';
                                    break;
                                case '/':
                                    c.Dir = '>';
                                    break;
                                case '+':
                                    switch (c.Turn)
                                    {
                                        case 0:
                                            c.Dir = '<';
                                            break;
                                        case 1:
                                            c.Dir = '^';
                                            break;
                                        case 2:
                                            c.Dir = '>';
                                            break;
                                    }
                                    c.Turn = (c.Turn + 1) % 3;
                                    break;
                            }
                            break;
                    }
                    var collisions = carts.Where(cart => cart.X == c.X && cart.Y == c.Y && cart.Number != c.Number && cart.Crashed == false);
                    if (collisions.Count() != 0)
                    {
                        if (crash.Equals(""))
                            crash = c.X.ToString() + "," + c.Y.ToString();
                        c.Crashed = true;
                        collisions.ElementAt(0).Crashed = true;
                        rem -= 2;
                    }
                }
            }
            Cart uncrash = carts.Find(c => c.Crashed == false);

            Console.WriteLine("Day 13, Problem 1: " + crash);
            Console.WriteLine("Day 13, Problem 2: " + uncrash.X + "," + uncrash.Y);
        }

        /// <summary>
        /// Helper class for day 13.
        /// </summary>
        private class Cart
        {
            public int X
            { get; set; }
            public int Y
            { get; set; }
            public int Turn
            { get; set; }
            public char Dir
            { get; set; }
            public int Number
            { get; set; }
            public bool Crashed
            { get; set; }
            public Cart(int x, int y, char dir, int num)
            {
                X = x;
                Y = y;
                Turn = 0;
                Dir = dir;
                Number = num;
                Crashed = false;
            }
        }

        /// <summary>
        /// DAY 14
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem14(string __input)
        {
            var line = File.ReadAllLines(__input);
            var scores = "";
            var r_count = int.Parse(line[0]);
            var recepies = new List<int> { 3, 7 };
            var elf1 = 0;
            var elf2 = 1;

            while (recepies.Count < r_count + 10)
            {
                var r = recepies[elf1] + recepies[elf2];
                if (r >= 10)
                {
                    recepies.Add(r / 10);
                    recepies.Add(r % 10);
                }
                else
                    recepies.Add(r);

                elf1 = (elf1 + 1 + recepies[elf1]) % recepies.Count;
                elf2 = (elf2 + 1 + recepies[elf2]) % recepies.Count;

            }

            scores = string.Join("", recepies);

            var found = scores.Contains(r_count.ToString());
            int i = 0;
            int offset = 0;
            while (!found)
            {
                var r = recepies[elf1] + recepies[elf2];
                if (r >= 10)
                {
                    recepies.Add(r / 10);
                    recepies.Add(r % 10);
                }
                else
                    recepies.Add(r);

                elf1 = (elf1 + 1 + recepies[elf1]) % recepies.Count;
                elf2 = (elf2 + 1 + recepies[elf2]) % recepies.Count;

                while (i + offset < recepies.Count)
                {
                    if (int.Parse(r_count.ToString()[offset].ToString()) == (recepies[i + offset]))
                    {
                        if (offset == (5))
                        {
                            found = true;
                            break;
                        }
                        offset++;
                    }
                    else
                    {
                        offset = 0;
                        i++;
                    }
                }
            }

            Console.WriteLine("Day 14, Problem 1: " + scores.Substring(r_count, 10));
            Console.WriteLine("Day 14, Problem 2: " + i);
        }

        /// <summary>
        /// DAY 15
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem15(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            List<List<char>> map = new List<List<char>>();
            foreach (var l in line)
                map.Add(l.ToList());

            List<NPC> npcs = new List<NPC>();
            List<Point> dirs = new List<Point>
            {
                new Point(0,-1),
                new Point(-1,0),
                new Point(1,0),
                new Point(0,1)
            };

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[0].Count; j++)
                {
                    if (map[i][j].Equals('E'))
                    {
                        map[i][j] = '.';
                        npcs.Add(new NPC(j, i, 'E', 200, 3));
                    }
                    if (map[i][j].Equals('G'))
                    {
                        map[i][j] = '.';
                        npcs.Add(new NPC(j, i, 'G', 200, 3));
                    }
                }
            }
            List<NPC> orig = npcs.Select(x => new NPC(x.X, x.Y, x.Type, x.HP, x.AP)).ToList();

            int score = 0;
            for (int time = 0; ; time++)
            {
                npcs = npcs.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();
                for (int i = 0; i < npcs.Count; i++)
                {
                    var n = npcs[i];
                    var targets = npcs.FindAll(x => !x.Type.Equals(n.Type));
                    if (targets.Count == 0)
                    {
                        score = time * npcs.Sum(x => x.HP);
                        break;
                    }
                    if (!targets.Any(t => (Math.Abs(n.X - t.X) + Math.Abs(n.Y - t.Y)) == 1))
                    {
                        HashSet<Point> inrange = new HashSet<Point>();
                        foreach (var t in targets)
                        {
                            foreach (var d in dirs)
                            {
                                var p = new Point(t.X + d.X, t.Y + d.Y);
                                if (map[p.Y][p.X] == '.' && npcs.All(x => x.X != p.X || x.Y != p.Y))
                                    inrange.Add(p);
                            }
                        }

                        Queue<Point> queue = new Queue<Point>();
                        Dictionary<Point, Point> previous = new Dictionary<Point, Point>();
                        queue.Enqueue(new Point(n.X, n.Y));
                        previous.Add(new Point(n.X, n.Y), new Point(-1, -1));
                        while (queue.Count > 0)
                        {
                            var p = queue.Dequeue();
                            foreach (var d in dirs)
                            {
                                var m = new Point(p.X + d.X, p.Y + d.Y);
                                if (previous.ContainsKey(m) || !(map[m.Y][m.X] == '.' && npcs.All(x => x.X != m.X || x.Y != m.Y)))
                                    continue;
                                queue.Enqueue(m);
                                previous.Add(m, new Point(p.X, p.Y));
                            }
                        }

                        var paths = inrange.Select(x => new Point(x.X, x.Y, GetPath(new Point(x.X, x.Y), previous, n))).ToList();
                        paths = paths.Where(x => x.Path != null).OrderBy(x => x.Path.Count).ThenBy(x => x.Y).ThenBy(x => x.X).ToList();
                        var path = paths.FirstOrDefault();
                        if (path != null && path.Path != null)
                        {
                            n.X = path.Path[0].X;
                            n.Y = path.Path[0].Y;
                        }

                    }

                    var target = targets.Where(t => (Math.Abs(n.X - t.X) + Math.Abs(n.Y - t.Y)) == 1).OrderBy(t => t.HP).ThenBy(t => t.Y).ThenBy(t => t.X).FirstOrDefault();
                    if (target == null)
                        continue;
                    target.HP -= n.AP;

                    if (target.HP > 0)
                        continue;

                    int index = npcs.IndexOf(target);
                    npcs.RemoveAt(index);
                    if (index < i)
                        i--;
                }
                if (score != 0)
                    break;
            }

            int score2 = 0;
            for (int atk = 4; ; atk++)
            {
                npcs = orig.Select(x => new NPC(x.X, x.Y, x.Type, x.HP, x.AP)).ToList();
                foreach (var n in npcs)
                    if (n.Type.Equals('E'))
                        n.AP = atk;
                bool elf_dead = false;
                for (int time = 0; ; time++)
                {
                    npcs = npcs.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();
                    for (int i = 0; i < npcs.Count; i++)
                    {
                        var n = npcs[i];
                        var targets = npcs.FindAll(x => !x.Type.Equals(n.Type));
                        if (targets.Count == 0)
                        {
                            score2 = time * npcs.Sum(x => x.HP);
                            break;
                        }
                        if (!targets.Any(t => (Math.Abs(n.X - t.X) + Math.Abs(n.Y - t.Y)) == 1))
                        {
                            HashSet<Point> inrange = new HashSet<Point>();
                            foreach (var t in targets)
                            {
                                foreach (var d in dirs)
                                {
                                    var p = new Point(t.X + d.X, t.Y + d.Y);
                                    if (map[p.Y][p.X] == '.' && npcs.All(x => x.X != p.X || x.Y != p.Y))
                                        inrange.Add(p);
                                }
                            }

                            Queue<Point> queue = new Queue<Point>();
                            Dictionary<Point, Point> previous = new Dictionary<Point, Point>();
                            queue.Enqueue(new Point(n.X, n.Y));
                            previous.Add(new Point(n.X, n.Y), new Point(-1, -1));
                            while (queue.Count > 0)
                            {
                                var p = queue.Dequeue();
                                foreach (var d in dirs)
                                {
                                    var m = new Point(p.X + d.X, p.Y + d.Y);
                                    if (previous.ContainsKey(m) || !(map[m.Y][m.X] == '.' && npcs.All(x => x.X != m.X || x.Y != m.Y)))
                                        continue;
                                    queue.Enqueue(m);
                                    previous.Add(m, new Point(p.X, p.Y));
                                }
                            }

                            var paths = inrange.Select(x => new Point(x.X, x.Y, GetPath(new Point(x.X, x.Y), previous, n))).ToList();
                            paths = paths.Where(x => x.Path != null).OrderBy(x => x.Path.Count).ThenBy(x => x.Y).ThenBy(x => x.X).ToList();
                            var path = paths.FirstOrDefault();
                            if (path != null && path.Path != null)
                            {
                                n.X = path.Path[0].X;
                                n.Y = path.Path[0].Y;
                            }

                        }

                        var target = targets.Where(t => (Math.Abs(n.X - t.X) + Math.Abs(n.Y - t.Y)) == 1).OrderBy(t => t.HP).ThenBy(t => t.Y).ThenBy(t => t.X).FirstOrDefault();
                        if (target == null)
                            continue;
                        target.HP -= n.AP;

                        if (target.HP > 0)
                            continue;

                        if (target.Type.Equals('E'))
                        {
                            elf_dead = true;
                            break;
                        }

                        int index = npcs.IndexOf(target);
                        npcs.RemoveAt(index);
                        if (index < i)
                            i--;
                    }
                    if (elf_dead || score2 != 0)
                        break;
                }
                if (score2 != 0)
                    break;
            }


            Console.WriteLine("Day 15, Problem 1: " + score);
            Console.WriteLine("Day 15, Problem 2: " + score2);
        }

        /// <summary>
        /// Helper for day 15.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="previous"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static List<Point> GetPath(Point dest, Dictionary<Point, Point> previous, NPC n)
        {
            if (!previous.ContainsKey(dest))
                return null;
            List<Point> path = new List<Point>();
            var p = new Point(dest.X, dest.Y);
            while (p.X != n.X || p.Y != n.Y)
            {
                path.Add(new Point(p.X, p.Y));
                p = previous[p];
            }
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Helper for day 15.
        /// </summary>
        private class NPC
        {
            public int X
            { get; set; }
            public int Y
            { get; set; }
            public char Type
            { get; }
            public int HP
            { get; set; }
            public int AP
            { get; set; }
            public NPC(int x, int y, char type, int hp, int ap)
            {
                X = x;
                Y = y;
                Type = type;
                HP = hp;
                AP = ap;
            }
        }

        /// <summary>
        /// DAY 16
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem16(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ', ',', '[', ']' };

            int[] regs = new int[4];
            int dups = 0;
            Dictionary<int, string> opcodes = new Dictionary<int, string>
            {
                { 0,"" },{ 1,"" },{ 2,"" },{ 3,"" },{ 4,"" },{ 5,"" },{ 6,"" },{ 7,"" },
                { 8,"" },{ 9,"" },{ 10,"" },{ 11,"" },{ 12,"" },{ 13,"" },{ 14,"" },{ 15,"" }
            };

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals(""))
                    continue;
                var s1 = line[i].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (s1[0].Equals("Before:"))
                {
                    var s2 = line[i + 1].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    var s3 = line[i + 2].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    var new_regs = new int[4];
                    var count = 0;

                    regs[0] = int.Parse(s1[1]);
                    regs[1] = int.Parse(s1[2]);
                    regs[2] = int.Parse(s1[3]);
                    regs[3] = int.Parse(s1[4]);

                    new_regs[0] = int.Parse(s3[1]);
                    new_regs[1] = int.Parse(s3[2]);
                    new_regs[2] = int.Parse(s3[3]);
                    new_regs[3] = int.Parse(s3[4]);

                    var ins = int.Parse(s2[0]);
                    var a = int.Parse(s2[1]);
                    var b = int.Parse(s2[2]);
                    var c = int.Parse(s2[3]);

                    if (new_regs[c] == (regs[a] + regs[b]))
                    {
                        opcodes[ins] += "addr,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] + b))
                    {
                        opcodes[ins] += "addi,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] * regs[b]))
                    {
                        opcodes[ins] += "mulr,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] * b))
                    {
                        opcodes[ins] += "muli,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] & regs[b]))
                    {
                        opcodes[ins] += "banr,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] & b))
                    {
                        opcodes[ins] += "bani,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] | regs[b]))
                    {
                        opcodes[ins] += "borr,";
                        count++;
                    }
                    if (new_regs[c] == (regs[a] | b))
                    {
                        opcodes[ins] += "bori,";
                        count++;
                    }
                    if (new_regs[c] == regs[a])
                    {
                        opcodes[ins] += "setr,";
                        count++;
                    }
                    if (new_regs[c] == a)
                    {
                        opcodes[ins] += "seti,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (a > regs[b])) || (new_regs[c] == 0 && (a <= regs[b])))
                    {
                        opcodes[ins] += "gtir,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (regs[a] > b)) || (new_regs[c] == 0 && (regs[a] <= b)))
                    {
                        opcodes[ins] += "gtri,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (regs[a] > regs[b])) || (new_regs[c] == 0 && (regs[a] <= regs[b])))
                    {
                        opcodes[ins] += "gtrr,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (a == regs[b])) || (new_regs[c] == 0 && (a != regs[b])))
                    {
                        opcodes[ins] += "eqir,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (regs[a] == b)) || (new_regs[c] == 0 && (regs[a] != b)))
                    {
                        opcodes[ins] += "eqri,";
                        count++;
                    }
                    if ((new_regs[c] == 1 && (regs[a] == regs[b])) || (new_regs[c] == 0 && (regs[a] != regs[b])))
                    {
                        opcodes[ins] += "eqrr,";
                        count++;
                    }

                    if (count >= 3)
                        dups++;

                    i += 3;
                }
            }

            Dictionary<string, int> freqs = new Dictionary<string, int>();
            List<Dictionary<string, int>> f_list = new List<Dictionary<string, int>>();
            foreach (var kvp in opcodes)
            {
                freqs = new Dictionary<string, int>();
                var str = kvp.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in str)
                {
                    if (freqs.ContainsKey(s))
                        freqs[s]++;
                    else
                        freqs.Add(s, 1);
                }


                f_list.Add(freqs);
            }
            for (int k = 0; k < 16; k++)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (f_list[i].Count == 1)
                    {
                        var rem = f_list[i].ElementAt(0).Key;
                        for (int j = 0; j < 16; j++)
                        {
                            if (i != j)
                                f_list[j].Remove(rem);
                        }
                    }
                }
            }

            regs = new int[4];
            for (int i = 0; i < line.Length; i++)
            {
                delims = new char[] { ' ' };
                if (line[i].Equals(""))
                    continue;
                var s1 = line[i].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (s1[0].Equals("Before:"))
                    i += 3;
                else
                {
                    var a = int.Parse(s1[1]);
                    var b = int.Parse(s1[2]);
                    var c = int.Parse(s1[3]);
                    switch (int.Parse(s1[0]))
                    {
                        case 0:
                            //eqri
                            regs[c] = regs[a] == b ? 1 : 0;
                            break;
                        case 1:
                            //banr
                            regs[c] = regs[a] & regs[b];
                            break;
                        case 2:
                            //bori
                            regs[c] = regs[a] | b;
                            break;
                        case 3:
                            //mulr
                            regs[c] = regs[a] * regs[b];
                            break;
                        case 4:
                            //seti
                            regs[c] = a;
                            break;
                        case 5:
                            //bani
                            regs[c] = regs[a] & b;
                            break;
                        case 6:
                            //muli
                            regs[c] = regs[a] * b;
                            break;
                        case 7:
                            //gtrr
                            regs[c] = regs[a] > regs[b] ? 1 : 0;
                            break;
                        case 8:
                            //setr
                            regs[c] = regs[a];
                            break;
                        case 9:
                            //addi
                            regs[c] = regs[a] + b;
                            break;
                        case 10:
                            //gtir
                            regs[c] = a > regs[b] ? 1 : 0;
                            break;
                        case 11:
                            //borr
                            regs[c] = regs[a] | regs[b];
                            break;
                        case 12:
                            //addr
                            regs[c] = regs[a] + regs[b];
                            break;
                        case 13:
                            //eqrr
                            regs[c] = regs[a] == regs[b] ? 1 : 0;
                            break;
                        case 14:
                            //gtri
                            regs[c] = regs[a] > b ? 1 : 0;
                            break;
                        case 15:
                            //eqir
                            regs[c] = a == regs[b] ? 1 : 0;
                            break;
                    }
                }
            }

            Console.WriteLine("Day 16, Problem 1: " + dups);
            Console.WriteLine("Day 16, Problem 2: " + regs[0]);
        }

        /// <summary>
        /// DAY 17
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem17(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { '=', ',', ' ', '.' };
            var min_x = int.MaxValue;
            var max_x = 0;
            var min_y = int.MaxValue;
            var max_y = 0;

            foreach (var l in line)
            {
                var s = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (s[0].Equals("x"))
                {
                    if (int.Parse(s[1]) < min_x)
                        min_x = int.Parse(s[1]);
                    if (int.Parse(s[1]) > max_x)
                        max_x = int.Parse(s[1]);
                    if (int.Parse(s[3]) < min_y)
                        min_y = int.Parse(s[3]);
                    if (int.Parse(s[4]) > max_y)
                        max_y = int.Parse(s[4]);
                }
                else
                {
                    if (int.Parse(s[1]) < min_y)
                        min_y = int.Parse(s[1]);
                    if (int.Parse(s[1]) > max_y)
                        max_y = int.Parse(s[1]);
                    if (int.Parse(s[3]) < min_x)
                        min_x = int.Parse(s[3]);
                    if (int.Parse(s[4]) > max_x)
                        max_x = int.Parse(s[4]);
                }
            }
            max_x += 2;
            min_x -= 2;
            var y = max_y - min_y;
            var x = max_x - min_x;
            char[,] ug = new char[y + 1, x + 1];
            for (int i = 0; i < y + 1; i++)
                for (int j = 0; j < x + 1; j++)
                    ug[i, j] = '.';

            foreach (var l in line)
            {
                var s = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (s[0].Equals("x"))
                {
                    x = int.Parse(s[1]) - min_x;
                    for (int i = int.Parse(s[3]) - min_y; i <= int.Parse(s[4]) - min_y; i++)
                    {
                        ug[i, x] = '#';
                    }
                }
                else
                {
                    y = int.Parse(s[1]) - min_y;
                    for (int i = int.Parse(s[3]) - min_x; i <= int.Parse(s[4]) - min_x; i++)
                    {
                        ug[y, i] = '#';
                    }
                }
            }

            var source = new Point((max_x - min_x) / 2, 0);
            var c = new Point(source.X, source.Y);
            Stack<Point> fill = new Stack<Point>();
            fill.Push(c);
            while (fill.Count != 0)
            {
                c = fill.Pop();
                if (c.Y + 1 > max_y - min_y)
                {
                    ug[c.Y, c.X] = '|';
                    while (fill.Count > 0 && fill.Peek().X == c.X)
                        fill.Pop();
                }
                else if (ug[c.Y + 1, c.X].Equals('|'))
                {
                    ug[c.Y, c.X] = '|';
                    while (fill.Count > 0 && fill.Peek().X == c.X)
                        fill.Pop();
                }
                else if (ug[c.Y + 1, c.X].Equals('.'))
                {
                    ug[c.Y, c.X] = '|';
                    fill.Push(new Point(c.X, c.Y));
                    fill.Push(new Point(c.X, c.Y + 1));
                }
                else
                {
                    var flood = true;
                    var left = 0;
                    var right = max_x - min_x;
                    for (int i = c.X; i >= 0; i--)
                    {
                        if (ug[c.Y, i].Equals('#'))
                        {
                            left = i;
                            break;
                        }
                    }
                    for (int i = c.X; i <= max_x - min_x; i++)
                    {
                        if (ug[c.Y, i].Equals('#'))
                        {
                            right = i;
                            break;
                        }
                    }
                    for (int i = left + 1; i < right; i++)
                    {
                        if (!(ug[c.Y + 1, i].Equals('#') || ug[c.Y + 1, i].Equals('~')))
                        {
                            flood = false;
                            if (i > c.X)
                                right = i + 1;
                            if (i < c.X)
                                left = i - 1;
                        }
                    }
                    if (flood)
                    {
                        for (int i = left + 1; i < right; i++)
                            ug[c.Y, i] = '~';
                    }
                    else
                    {
                        if (!ug[c.Y + 1, c.X].Equals('|'))
                        {
                            for (int i = left + 1; i < right; i++)
                            {
                                if (!ug[c.Y, i].Equals('|'))
                                {
                                    ug[c.Y, i] = '|';
                                    fill.Push(new Point(i, c.Y));
                                }
                            }
                        }
                    }
                }
            }

            var water = 0;
            var still_water = 0;
            for (int i = 0; i < max_y - min_y + 1; i++)
                for (int j = 0; j < max_x - min_x + 1; j++)
                    if (ug[i, j].Equals('~') || ug[i, j].Equals('|'))
                    {
                        if (ug[i, j].Equals('~'))
                            still_water++;
                        water++;
                    }

            Console.WriteLine("Day 17, Problem 1: " + water);
            Console.WriteLine("Day 17, Problem 2: " + still_water);
        }

        /// <summary>
        /// DAY 18
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem18(string __input)
        {
            var line = File.ReadAllLines(__input);
            List<List<char>> forest = new List<List<char>>();
            foreach (var l in line)
                forest.Add(l.ToList());

            List<List<char>> new_forest = new List<List<char>>();
            forest.ForEach(f => new_forest.Add(f.ToList()));

            List<int> lumber_list = new List<int>();

            var trees = 0;
            var lumber = 0;
            var empty = 0;
            for (int m = 0; m < 1000; m++)
            {
                for (int i = 0; i < forest.Count; i++)
                {
                    for (int j = 0; j < forest[i].Count; j++)
                    {
                        trees = 0;
                        lumber = 0;
                        empty = 0;
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            switch (forest[i - 1][j - 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (i - 1 >= 0)
                        {
                            switch (forest[i - 1][j])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (i - 1 >= 0 && j + 1 < forest[i].Count)
                        {
                            switch (forest[i - 1][j + 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            switch (forest[i][j - 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (j + 1 < forest[i].Count)
                        {
                            switch (forest[i][j + 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (i + 1 < forest.Count && j - 1 >= 0)
                        {
                            switch (forest[i + 1][j - 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (i + 1 < forest.Count)
                        {
                            switch (forest[i + 1][j])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (i + 1 < forest.Count && j + 1 < forest[i].Count)
                        {
                            switch (forest[i + 1][j + 1])
                            {
                                case '.':
                                    empty++;
                                    break;
                                case '#':
                                    lumber++;
                                    break;
                                case '|':
                                    trees++;
                                    break;
                            }
                        }
                        if (forest[i][j].Equals('.') && trees >= 3)
                            new_forest[i][j] = '|';
                        if (forest[i][j].Equals('|') && lumber >= 3)
                            new_forest[i][j] = '#';
                        if (forest[i][j].Equals('#') && (lumber == 0 || trees == 0))
                            new_forest[i][j] = '.';
                    }
                }

                forest = new List<List<char>>();
                new_forest.ForEach(f => forest.Add(f.ToList()));

                trees = 0;
                lumber = 0;
                empty = 0;
                for (int i = 0; i < forest.Count; i++)
                {
                    for (int j = 0; j < forest[i].Count; j++)
                    {
                        switch (forest[i][j])
                        {
                            case '.':
                                empty++;
                                break;
                            case '#':
                                lumber++;
                                break;
                            case '|':
                                trees++;
                                break;
                        }
                    }
                }
                lumber_list.Add(trees * lumber);
            }

            Console.WriteLine("Day 18, Problem 1: " + lumber_list[9].ToString());
            var time = 1000000000;
            while (time >= 1000)
                time -= 28;
            Console.WriteLine("Day 18, Problem 2: " + lumber_list[time - 1]);
        }

        /// <summary>
        /// DAY 19
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem19(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            int[] regs = new int[6];
            int ip_reg = int.Parse(line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]);
            int ip = 0;
            while (ip + 1 < line.Length)
            {
                var s1 = line[ip + 1].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                var a = int.Parse(s1[1]);
                var b = int.Parse(s1[2]);
                var c = int.Parse(s1[3]);

                regs[ip_reg] = ip;
                switch (s1[0])
                {
                    case "eqri":
                        //eqri
                        regs[c] = regs[a] == b ? 1 : 0;
                        break;
                    case "banr":
                        //banr
                        regs[c] = regs[a] & regs[b];
                        break;
                    case "bori":
                        //bori
                        regs[c] = regs[a] | b;
                        break;
                    case "mulr":
                        //mulr
                        regs[c] = regs[a] * regs[b];
                        break;
                    case "seti":
                        //seti
                        regs[c] = a;
                        break;
                    case "bani":
                        //bani
                        regs[c] = regs[a] & b;
                        break;
                    case "muli":
                        //muli
                        regs[c] = regs[a] * b;
                        break;
                    case "gtrr":
                        //gtrr
                        regs[c] = regs[a] > regs[b] ? 1 : 0;
                        break;
                    case "setr":
                        //setr
                        regs[c] = regs[a];
                        break;
                    case "addi":
                        //addi
                        regs[c] = regs[a] + b;
                        break;
                    case "gtir":
                        //gtir
                        regs[c] = a > regs[b] ? 1 : 0;
                        break;
                    case "borr":
                        //borr
                        regs[c] = regs[a] | regs[b];
                        break;
                    case "addr":
                        //addr
                        regs[c] = regs[a] + regs[b];
                        break;
                    case "eqrr":
                        //eqrr
                        regs[c] = regs[a] == regs[b] ? 1 : 0;
                        break;
                    case "gtri":
                        //gtri
                        regs[c] = regs[a] > b ? 1 : 0;
                        break;
                    case "eqir":
                        //eqir
                        regs[c] = a == regs[b] ? 1 : 0;
                        break;
                }
                ip = regs[ip_reg];
                ip++;
            }
            var reg0 = regs[0];

            regs = new int[6];
            ip = 0;
            regs[0] = 1;
            //the program calculates the sum of the factors of regs[1]
            //when a == 1, b == 10551343
            //when a == 0, b == 943
            //while (ip + 1 < line.Length)
            //{
            //    var s1 = line[ip + 1].Split(delims, StringSplitOptions.RemoveEmptyEntries);
            //    var a = int.Parse(s1[1]);
            //    var b = int.Parse(s1[2]);
            //    var c = int.Parse(s1[3]);

            //    regs[ip_reg] = ip;
            //    switch (s1[0])
            //    {
            //        case "eqri":
            //            //eqri
            //            regs[c] = regs[a] == b ? 1 : 0;
            //            break;
            //        case "banr":
            //            //banr
            //            regs[c] = regs[a] & regs[b];
            //            break;
            //        case "bori":
            //            //bori
            //            regs[c] = regs[a] | b;
            //            break;
            //        case "mulr":
            //            //mulr
            //            regs[c] = regs[a] * regs[b];
            //            break;
            //        case "seti":
            //            //seti
            //            regs[c] = a;
            //            break;
            //        case "bani":
            //            //bani
            //            regs[c] = regs[a] & b;
            //            break;
            //        case "muli":
            //            //muli
            //            regs[c] = regs[a] * b;
            //            break;
            //        case "gtrr":
            //            //gtrr
            //            regs[c] = regs[a] > regs[b] ? 1 : 0;
            //            break;
            //        case "setr":
            //            //setr
            //            regs[c] = regs[a];
            //            break;
            //        case "addi":
            //            //addi
            //            regs[c] = regs[a] + b;
            //            break;
            //        case "gtir":
            //            //gtir
            //            regs[c] = a > regs[b] ? 1 : 0;
            //            break;
            //        case "borr":
            //            //borr
            //            regs[c] = regs[a] | regs[b];
            //            break;
            //        case "addr":
            //            //addr
            //            regs[c] = regs[a] + regs[b];
            //            break;
            //        case "eqrr":
            //            //eqrr
            //            regs[c] = regs[a] == regs[b] ? 1 : 0;
            //            break;
            //        case "gtri":
            //            //gtri
            //            regs[c] = regs[a] > b ? 1 : 0;
            //            break;
            //        case "eqir":
            //            //eqir
            //            regs[c] = a == regs[b] ? 1 : 0;
            //            break;
            //    }
            //    ip = regs[ip_reg];
            //    ip++;
            //}
            var sum = 1 + 11 + 743 + 1291 + 8173 + 14201 + 959213 + 10551343;
            Console.WriteLine("Day 19, Problem 1: " + reg0);
            Console.WriteLine("Day 19, Problem 2: " + sum);
        }

        /// <summary>
        /// DAY 20
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem20(string __input)
        {
            var line = File.ReadAllLines(__input);
            var input = line[0].Substring(1, line[0].Length - 2);
            int size = 1000;
            List<List<int>> rooms = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                var l = new List<int>();
                for (int j = 0; j < size; j++)
                    l.Add(int.MaxValue);
                rooms.Add(l);
            }

            rooms[size / 2][size / 2] = 0;
            Eval(input, ref rooms, size / 2, size / 2);

            int max = 0;
            int over_1k = 0;
            foreach (var y in rooms)
                foreach (var x in y)
                {
                    if (x != int.MaxValue)
                        max = Math.Max(max, x);
                    if (x != int.MaxValue && x >= 1000)
                        over_1k++;
                }

            Console.WriteLine("Day 20, Problem 1: " + max);
            Console.WriteLine("Day 20, Problem 2: " + over_1k);
        }

        /// <summary>
        /// Helper for day 20.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void Eval(string s, ref List<List<int>> rooms, int x, int y)
        {
            int i;
            for (i = 0; i < s.Length && !s[i].Equals('('); i++)
            {
                int new_x = x;
                int new_y = y;
                switch (s[i])
                {
                    case 'N':
                        new_y--;
                        break;
                    case 'E':
                        new_x++;
                        break;
                    case 'S':
                        new_y++;
                        break;
                    case 'W':
                        new_x--;
                        break;
                }
                rooms[new_y][new_x] = Math.Min(rooms[new_y][new_x], rooms[y][x] + 1);
                x = new_x;
                y = new_y;
            }
            if (i == s.Length)
                return;
            int j = i + 1;
            int depth = 1;
            for (; ; j++)
            {
                if (s[j].Equals('('))
                    depth++;
                else if (s[j].Equals(')'))
                    depth--;
                if (depth == 0)
                    break;
            }
            string s2 = s.Substring(i + 1, j - i - 1);
            for (; ; )
            {
                int k;
                int depth2 = 0;
                for (k = 0; k < s2.Length; k++)
                {
                    if (s2[k].Equals('('))
                        depth2++;
                    else if (s2[k].Equals(')'))
                        depth2--;
                    if (depth2 == 0 && s2[k].Equals('|'))
                        break;
                }
                Eval(s2.Substring(0, k), ref rooms, x, y);
                if (k == s2.Length)
                    break;
                s2 = s2.Substring(k + 1);
            }
            if (j < s.Length - 1)
                Eval(s.Substring(j + 1), ref rooms, x, y);
        }

        /// <summary>
        /// DAY 21
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem21(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };
            int[] regs = new int[6];
            int ip_reg = int.Parse(line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]);
            int ip = 0;
            Dictionary<int, int> halts = new Dictionary<int, int>();
            while (ip + 1 < line.Length)
            {
                var s1 = line[ip + 1].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                var a = int.Parse(s1[1]);
                var b = int.Parse(s1[2]);
                var c = int.Parse(s1[3]);

                regs[ip_reg] = ip;
                if (ip == 28)
                {
                    if (halts.ContainsKey(regs[2]))
                        break;
                    else
                        halts.Add(regs[2], 0);
                }
                switch (s1[0])
                {
                    case "eqri":
                        //eqri
                        regs[c] = regs[a] == b ? 1 : 0;
                        break;
                    case "banr":
                        //banr
                        regs[c] = regs[a] & regs[b];
                        break;
                    case "bori":
                        //bori
                        regs[c] = regs[a] | b;
                        break;
                    case "mulr":
                        //mulr
                        regs[c] = regs[a] * regs[b];
                        break;
                    case "seti":
                        //seti
                        regs[c] = a;
                        break;
                    case "bani":
                        //bani
                        regs[c] = regs[a] & b;
                        break;
                    case "muli":
                        //muli
                        regs[c] = regs[a] * b;
                        break;
                    case "gtrr":
                        //gtrr
                        regs[c] = regs[a] > regs[b] ? 1 : 0;
                        break;
                    case "setr":
                        //setr
                        regs[c] = regs[a];
                        break;
                    case "addi":
                        //addi
                        regs[c] = regs[a] + b;
                        break;
                    case "gtir":
                        //gtir
                        regs[c] = a > regs[b] ? 1 : 0;
                        break;
                    case "borr":
                        //borr
                        regs[c] = regs[a] | regs[b];
                        break;
                    case "addr":
                        //addr
                        regs[c] = regs[a] + regs[b];
                        break;
                    case "eqrr":
                        //eqrr
                        regs[c] = regs[a] == regs[b] ? 1 : 0;
                        break;
                    case "gtri":
                        //gtri
                        regs[c] = regs[a] > b ? 1 : 0;
                        break;
                    case "eqir":
                        //eqir
                        regs[c] = a == regs[b] ? 1 : 0;
                        break;
                }
                ip = regs[ip_reg];
                ip++;
            }



            Console.WriteLine("Day 21, Problem 1: " + halts.ElementAt(0).Key);
            Console.WriteLine("Day 21, Problem 2: " + halts.ElementAt(halts.Count - 1).Key);
        }

        /// <summary>
        /// DAY 22
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem22(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ', ',' };
            int depth = int.Parse(line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]);
            int target_x = int.Parse(line[1].Split(delims, StringSplitOptions.RemoveEmptyEntries)[1]);
            int target_y = int.Parse(line[1].Split(delims, StringSplitOptions.RemoveEmptyEntries)[2]);
            int[,] geologic = new int[1000, 1000];
            int[,] erosion = new int[1000, 1000];
            int[,] cave = new int[1000, 1000];

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (i == 0 && j == 0)
                        geologic[j, i] = 0;
                    else if (i == target_y && j == target_x)
                        geologic[j, i] = 0;
                    else if (i == 0)
                        geologic[j, i] = j * 16807;
                    else if (j == 0)
                        geologic[j, i] = i * 48271;
                    else
                        geologic[j, i] = erosion[j - 1, i] * erosion[j, i - 1];

                    erosion[j, i] = (geologic[j, i] + depth) % 20183;
                    cave[j, i] = erosion[j, i] % 3;
                }
            }

            int risk = 0;
            for (int i = 0; i < target_y + 1; i++)
                for (int j = 0; j < target_x + 1; j++)
                    risk += cave[j, i];

            Queue<(int x, int y, int tool, int switching, int t)> queue = new Queue<(int x, int y, int tool, int switching, int t)>();
            HashSet<(int x, int y, int tool)> seen = new HashSet<(int x, int y, int tool)>();
            queue.Enqueue((0, 0, 1, 0, 0));
            seen.Add((0, 0, 1));

            int time = 0;
            while (queue.Count > 0)
            {
                (int x, int y, int tool, int switching, int minutes) = queue.Dequeue();
                if (switching > 0)
                {
                    if (switching != 1 || seen.Add((x, y, tool)))
                        queue.Enqueue((x, y, tool, switching - 1, minutes + 1));
                    continue;
                }

                if (x == target_x && y == target_y && tool == 1)
                {
                    time = minutes;
                    break;
                }

                if (cave[x + 1, y] != tool && seen.Add((x + 1, y, tool)))
                    queue.Enqueue((x + 1, y, tool, 0, minutes + 1));

                if (x - 1 >= 0)
                    if (cave[x - 1, y] != tool && seen.Add((x - 1, y, tool)))
                        queue.Enqueue((x - 1, y, tool, 0, minutes + 1));

                if (cave[x, y + 1] != tool && seen.Add((x, y + 1, tool)))
                    queue.Enqueue((x, y + 1, tool, 0, minutes + 1));

                if (y - 1 >= 0)
                    if (cave[x, y - 1] != tool && seen.Add((x, y - 1, tool)))
                        queue.Enqueue((x, y - 1, tool, 0, minutes + 1));

                queue.Enqueue((x, y, (cave[x, y] + 1) % 3, 6, minutes + 1));
                queue.Enqueue((x, y, (cave[x, y] + 2) % 3, 6, minutes + 1));
            }

            Console.WriteLine("Day 22, Problem 1: " + risk);
            Console.WriteLine("Day 22, Problem 2: " + time);
        }

        /// <summary>
        /// DAY 23
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem23(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ', ',', '=', '<', '>' };
            Dictionary<(int x, int y, int z), int> nano = new Dictionary<(int x, int y, int z), int>();
            int max_r = 0;
            (int x, int y, int z) max_nano = (0, 0, 0);
            foreach (var l in line)
            {
                var s = l.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                nano.Add((int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])), int.Parse(s[5]));
                if (int.Parse(s[5]) > max_r)
                {
                    max_r = int.Parse(s[5]);
                    max_nano = (int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
                }
            }
            int in_range = 0;
            foreach (var n in nano)
                if (Math.Abs(n.Key.x - max_nano.x) + Math.Abs(n.Key.y - max_nano.y) + Math.Abs(n.Key.z - max_nano.z) <= max_r)
                    in_range++;

            Queue<(int, int)> q = new Queue<(int, int)>();
            foreach (var n in nano)
            {
                int d = Math.Abs(n.Key.x) + Math.Abs(n.Key.y) + Math.Abs(n.Key.z);
                q.Enqueue((Math.Max(0, d - n.Value), 1));
                q.Enqueue((d + n.Value, -1));
            }
            q = new Queue<(int, int)>(q.OrderBy(x => x.Item1));
            int count = 0;
            int max = 0;
            int best = 0;
            while (q.Count > 0)
            {
                (int dist, int end) = q.Dequeue();
                count += end;
                if (count > max)
                {
                    best = dist;
                    max = count;
                }
            }

            Console.WriteLine("Day 23, Problem 1: " + in_range);
            Console.WriteLine("Day 23, Problem 2: " + best);
        }

        /// <summary>
        /// DAY 24
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem24(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ' ' };

            List<Group> units = new List<Group>();
            var immuneSystem = false;
            foreach (var l in line)
            {
                if (l == "Immune System:")
                    immuneSystem = true;
                else if (l == "Infection:")
                    immuneSystem = false;
                else if (l != "")
                {
                    var rx = @"(\d+) units each with (\d+) hit points(.*)with an attack that does (\d+)(.*)damage at initiative (\d+)";
                    var m = Regex.Match(l, rx);

                    var str = m.Groups[3].Value.Trim();
                    var immune = "";
                    var weak = "";
                    if (str != "")
                    {
                        str = str.Substring(1, str.Length - 2);
                        foreach (var part in str.Split(new char[] { ';' }))
                        {
                            var k = part.Split(new string[] { " to " }, StringSplitOptions.RemoveEmptyEntries);
                            var w = k[0].Trim();
                            if (w == "immune")
                                immune = k[1];
                            else if (w == "weak")
                                weak = k[1];
                        }
                    }
                    Group g = new Group(int.Parse(m.Groups[1].Value),
                                        int.Parse(m.Groups[2].Value),
                                        immune,
                                        weak,
                                        int.Parse(m.Groups[4].Value),
                                        m.Groups[5].Value.Trim(),
                                        int.Parse(m.Groups[6].Value),
                                        immuneSystem ? "immune" : "infection");
                    units.Add(g);
                }
            }
            var n_units = units.Select(x => new Group(x.Units, x.HP, x.Immune, x.Weak, x.AP, x.AP_Type, x.Initiative, x.Type)).ToList();
            var result = EvaluateImmuneSystem(n_units, 0);
            var score = result.Sum(x => x.Units);
            var immune_score = 0;
            for (int i = 1; ; i++)
            {
                n_units = units.Select(x => new Group(x.Units, x.HP, x.Immune, x.Weak, x.AP, x.AP_Type, x.Initiative, x.Type)).ToList();
                result = EvaluateImmuneSystem(n_units, i);

                bool immune_win = true;
                foreach (var u in result)
                    if (u.Type.Equals("infection"))
                        immune_win = false;
                if (immune_win)
                {
                    immune_score = result.Sum(x => x.Units);
                    break;
                }

            }

            Console.WriteLine("Day 24, Problem 1: " + score);
            Console.WriteLine("Day 24, Problem 2: " + immune_score);
        }

        /// <summary>
        /// Helper for day 24.
        /// </summary>
        /// <param name="units"></param>
        /// <param name="boost"></param>
        /// <returns></returns>
        private static List<Group> EvaluateImmuneSystem(List<Group> units, int boost)
        {
            foreach (var u in units)
                if (u.Type.Equals("immune"))
                    u.AP += boost;

            while (true)
            {
                units = units.OrderByDescending(x => x.EP()).ThenByDescending(x => x.Initiative).ToList();
                foreach (var u in units)
                {
                    u.Under_Attack = false;
                    u.Target = -1;
                }

                for (int i = 0; i < units.Count; i++)
                {
                    List<(int num, int dmg, int ep, int init)> targets = new List<(int num, int dmg, int ep, int init)>();
                    for (int j = 0; j < units.Count; j++)
                    {
                        if (units[i].Type == units[j].Type)
                            continue;
                        var d = 0;
                        if (units[j].Immune.Contains(units[i].AP_Type))
                            d = 0;
                        else if (units[j].Weak.Contains(units[i].AP_Type))
                            d = units[i].EP() * 2;
                        else
                            d = units[i].EP();
                        if (!units[j].Under_Attack && d > 0)
                            targets.Add((j, d, units[j].EP(), units[j].Initiative));
                    }
                    targets = targets.OrderByDescending(x => x.dmg).ThenByDescending(x => x.ep).ThenByDescending(x => x.init).ToList();
                    if (targets.Count != 0)
                    {
                        units[i].Target = targets[0].num;
                        units[targets[0].num].Under_Attack = true;
                    }
                }

                var attacking = units.OrderByDescending(x => x.Initiative).ToList();

                var has_attacks = false;
                foreach (var u in units)
                    if (u.Target != -1)
                        has_attacks = true;
                if (!has_attacks)
                    break;

                var effective_attack = false;

                for (int i = 0; i < attacking.Count; i++)
                {
                    if (attacking[i].Target == -1)
                        continue;

                    var d = 0;
                    if (units[attacking[i].Target].Immune.Contains(attacking[i].AP_Type))
                        d = 0;
                    else if (units[attacking[i].Target].Weak.Contains(attacking[i].AP_Type))
                        d = attacking[i].EP() * 2;
                    else
                        d = attacking[i].EP();

                    var attack = (d / units[attacking[i].Target].HP);
                    if (attack > 0)
                        effective_attack = true;

                    units[attacking[i].Target].Units -= attack;
                    if (units[attacking[i].Target].Units < 0)
                        units[attacking[i].Target].Units = 0;
                }
                if (!effective_attack)
                    break;

                units = units.Where(x => x.Units != 0).ToList();

                bool has_immune = false;
                bool has_infection = false;
                foreach (var u in units)
                {
                    if (u.Type.Equals("immune"))
                        has_immune = true;
                    else if (u.Type.Equals("infection"))
                        has_infection = true;
                }
                if (!has_immune || !has_infection)
                    break;
            }
            return units;
        }

        /// <summary>
        /// Helper for day 24.
        /// </summary>
        private class Group
        {
            public int Units { get; set; }
            public int HP { get; set; }
            public string Immune { get; set; }
            public string Weak { get; set; }
            public int AP { get; set; }
            public string AP_Type { get; set; }
            public int Initiative { get; set; }
            public string Type { get; set; }
            public bool Under_Attack { get; set; }
            public int Target { get; set; }
            public int EP()
            {
                return Units * AP;
            }
            public Group(int units, int hp, string immune, string weak, int ap, string ap_type, int initiative, string type)
            {
                Units = units;
                HP = hp;
                Immune = immune;
                Weak = weak;
                AP = ap;
                AP_Type = ap_type;
                Initiative = initiative;
                Type = type;
                Under_Attack = false;
                Target = -1;
            }
        }

        /// <summary>
        /// DAY 25
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem25(string __input)
        {
            var line = File.ReadAllLines(__input);
            char[] delims = { ',' };

            List<List<(int x, int y, int z, int w)>> constellations = new List<List<(int x, int y, int z, int w)>>();
            var s = line[0].Split(delims, StringSplitOptions.RemoveEmptyEntries);
            (int x, int y, int z, int w) = (int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
            constellations.Add(new List<(int x, int y, int z, int w)>());
            constellations[0].Add((x, y, z, w));

            for (int i = 1; i < line.Length; i++)
            {
                s = line[i].Split(delims, StringSplitOptions.RemoveEmptyEntries);
                (x, y, z, w) = (int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
                foreach (var c in constellations)
                {
                    bool contains = false;
                    foreach (var p in c)
                    {

                    }
                }

            }
            Console.WriteLine("Day 25, Problem 1: ");
            Console.WriteLine("Day 25, Problem 2: ");
        }
    }
}
