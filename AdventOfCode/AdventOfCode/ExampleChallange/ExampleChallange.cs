using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.ExampleChallange
{
    public static class ExampleChallange
    {
        public static int GetAnswer(int target = 2020)
        {
            var entries = ReadEntriesFromFile().OrderBy(x => x).ToArray();

            for (int i = 0; i < entries.Length; i++)
            {
                for (int k = i + 1; k < entries.Length; k++)
                {
                    var sum = entries[i] + entries[k];

                    if (sum == target)
                    {
                        return entries[i] * entries[k];
                    }
                }                              
            }

            throw new Exception("Unable to find two numbers that add to target");
        }

        private static int[] ReadEntriesFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "ExampleChallange/ExampleChallangeEntries.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.Select(x => int.Parse(x)).ToArray();
        }
    }
}
