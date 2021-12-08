using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day8
{
    public static class Day8Challange
    {
        public static int GetAnswerPart1()
        {
            var entries = ReadInputFromFile();

            var numberOfLinesRequiredForUniqueSegment = new int[] { 2, 3, 4, 7 };

            var lengthOfOutputCodes = entries.SelectMany(x => x.OutputValues).Select(y => y.Length).ToList();

            return lengthOfOutputCodes.Count(x => numberOfLinesRequiredForUniqueSegment.Contains(x));
        }
        public static int GetAnswerPart2()
        {
            var entries = ReadInputFromFile();

            return entries.Sum(x => CalaculateEntryOutPut(x));
        }

        private static int CalaculateEntryOutPut(Entry entry)
        {
            var displayCodes = new string[10];

            // Get what we know first 
            displayCodes[1] = SortString(entry.SignalPatterns.Where(x => x.Length == 2).FirstOrDefault());
            displayCodes[4] = SortString(entry.SignalPatterns.Where(x => x.Length == 4).FirstOrDefault());
            displayCodes[7] = SortString(entry.SignalPatterns.Where(x => x.Length == 3).FirstOrDefault());
            displayCodes[8] = SortString(entry.SignalPatterns.Where(x => x.Length == 7).FirstOrDefault());

            // The remaining signals can only be 0, 2, 3, 5 or 9
            foreach (var signal in entry.SignalPatterns)
            {
                var orderdedSignal = SortString(signal);

                if (!displayCodes.Contains(orderdedSignal))
                {
                    // 2, 3 and 5 have displays of length 5
                    if (signal.Length == 5)
                    {
                        // 3 shares a line with 7 
                        if (IsSegmentContained(signal, displayCodes[7]))
                        {
                            displayCodes[3] = orderdedSignal;
                            continue;
                        }
                        // We know what 4 is, 4 shares 3 lines with 5 but only 2 with 2 
                        else if (orderdedSignal.Intersect(displayCodes[4]).Count() == 3)
                        {
                            displayCodes[5] = orderdedSignal;
                            continue;
                        }
                        else
                        {
                            displayCodes[2] = orderdedSignal;
                            continue;
                        }
                    }
                    // 0 , 6 , 9
                    else
                    {
                        // 9 is the only number that fully contains 4 
                        if (IsSegmentContained(signal, displayCodes[4]))
                        {
                            displayCodes[9] = orderdedSignal;
                            continue;
                        }
                        // 0 uses all the lights 7 does, 6 doesn't
                        else if (IsSegmentContained(signal, displayCodes[7]))
                        {
                            displayCodes[0] = orderdedSignal;
                            continue;
                        }
                        else
                        {
                            displayCodes[6] = orderdedSignal;
                            continue;
                        }
                    }
                }                
            }

            var orderdedOutputValuess = entry.OutputValues.Select(x => SortString(x)).ToList();

            var result = orderdedOutputValuess.Select(x => Array.IndexOf(displayCodes, x));

            return int.Parse(string.Join("", result));
        }

        private static bool IsSegmentContained(string segment, string segmentToCheck)
        {
            var segmentArray = segment.ToArray();
            var segmentToCheckArray = segmentToCheck.ToArray();

            return !segmentToCheckArray.Except(segmentArray).Any();
        }

        private static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private static List<Entry> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day8/Day8Input.txt");
            var fileLines = File.ReadAllLines(filePath);
            var entries = new List<Entry>();

            foreach (var line in fileLines)
            {
                var lineInfo = line.Split(" | ");
                var entry = new Entry()
                {
                    SignalPatterns = lineInfo[0].Split(" ").ToList(),
                    OutputValues = lineInfo[1].Split(" ").ToList()
                };

                entries.Add(entry);
            }

            return entries;
        }
    }
}
