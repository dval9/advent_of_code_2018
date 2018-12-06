using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            Problem6(@"..\..\problem6.txt");
            Problem7(@"..\..\problem7.txt");
            Problem8(@"../../problem8.txt");
            Problem9(@"..\..\problem9.txt");
            Problem10(@"..\..\problem10.txt");
            Problem11(@"..\..\problem11.txt");
            Problem12(@"..\..\problem12.txt");
            Problem13(@"..\..\problem13.txt");
            Problem14(@"..\..\problem14.txt");
            Problem15(@"..\..\problem15.txt");
            Problem16(@"..\..\problem16.txt");
            Problem17(@"..\..\problem17.txt");
            Problem18(@"..\..\problem18.txt");
            Problem19(@"..\..\problem19.txt");
            Problem20(@"..\..\problem20.txt");
            Problem21(@"..\..\problem21.txt");
            Problem22(@"..\..\problem22.txt");
            Problem23(@"..\..\problem23.txt");
            Problem24(@"..\..\problem24.txt");
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

            //for (int i = 0; i < grid_size; i++)
            //{
            //    for (int j = 0; j < grid_size; j++)
            //        if (string.IsNullOrEmpty(grid[j, i]))
            //            Console.Write("    |");
            //        else
            //            Console.Write(grid[j, i] + "|");
            //    Console.WriteLine("");
            //    for (int j = 0; j < grid_size; j++)
            //        Console.Write("_____");
            //    Console.WriteLine("");
            //}
            //Console.WriteLine("");

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

            Console.WriteLine("Day 6, Problem 1: " + max);
            Console.WriteLine("Day 6, Problem 2: ");
        }

        /// <summary>
        /// DAY 7
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem7(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 7, Problem 1: ");
            Console.WriteLine("Day 7, Problem 2: ");
        }

        /// <summary>
        /// DAY 8
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem8(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 8, Problem 1: ");
            Console.WriteLine("Day 8, Problem 2: ");
        }

        /// <summary>
        /// DAY 9
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem9(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 9, Problem 1: ");
            Console.WriteLine("Day 9, Problem 2: ");
        }

        /// <summary>
        /// DAY 10
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem10(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 10, Problem 1: ");
            Console.WriteLine("Day 10, Problem 2: ");
        }

        /// <summary>
        /// DAY 11
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem11(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 11, Problem 1: ");
            Console.WriteLine("Day 11, Problem 2: ");
        }

        /// <summary>
        /// DAY 12
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem12(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 12, Problem 1: ");
            Console.WriteLine("Day 12, Problem 2: ");
        }

        /// <summary>
        /// DAY 13
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem13(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 13, Problem 1: ");
            Console.WriteLine("Day 13, Problem 2: ");
        }

        /// <summary>
        /// DAY 14
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem14(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 14, Problem 1: ");
            Console.WriteLine("Day 14, Problem 2: ");
        }

        /// <summary>
        /// DAY 15
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem15(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 15, Problem 1: ");
            Console.WriteLine("Day 15, Problem 2: ");
        }

        /// <summary>
        /// DAY 16
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem16(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 16, Problem 1: ");
            Console.WriteLine("Day 16, Problem 2: ");
        }

        /// <summary>
        /// DAY 17
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem17(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 17, Problem 1: ");
            Console.WriteLine("Day 17, Problem 2: ");
        }

        /// <summary>
        /// DAY 18
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem18(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 18, Problem 1: ");
            Console.WriteLine("Day 18, Problem 2: ");
        }

        /// <summary>
        /// DAY 19
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem19(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 19, Problem 1: ");
            Console.WriteLine("Day 19, Problem 2: ");
        }

        /// <summary>
        /// DAY 20
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem20(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 20, Problem 1: ");
            Console.WriteLine("Day 20, Problem 2: ");
        }

        /// <summary>
        /// DAY 21
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem21(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 21, Problem 1: ");
            Console.WriteLine("Day 21, Problem 2: ");
        }

        /// <summary>
        /// DAY 22
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem22(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 22, Problem 1: ");
            Console.WriteLine("Day 22, Problem 2: ");
        }

        /// <summary>
        /// DAY 23
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem23(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 23, Problem 1: ");
            Console.WriteLine("Day 23, Problem 2: ");
        }

        /// <summary>
        /// DAY 24
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem24(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 24, Problem 1: ");
            Console.WriteLine("Day 24, Problem 2: ");
        }

        /// <summary>
        /// DAY 25
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem25(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 25, Problem 1: ");
            Console.WriteLine("Day 25, Problem 2: ");
        }
    }
}
