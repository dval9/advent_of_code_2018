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
            Problem4(@"..\..\problem4.txt");
            Problem5(@"..\..\problem5.txt");
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
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem4(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 4, Problem 1: ");
            Console.WriteLine("Day 4, Problem 2: ");
        }

        /// <summary>
        /// DAY 5
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem5(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 5, Problem 1: ");
            Console.WriteLine("Day 5, Problem 2: ");
        }

        /// <summary>
        /// DAY 6
        /// </summary>
        /// <param name="__input">File name to read the input</param>
        static void Problem6(string __input)
        {
            var line = File.ReadAllLines(__input);

            Console.WriteLine("Day 6, Problem 1: ");
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
