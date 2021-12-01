using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day1
{
    public static class Day1Challange
    {
        public static int GetAnswerPart1()
        {
            var input = ReadInputFromFile();
            var numberOfIncreases = 0;

            for (int i = 1; i < input.Count(); i++)
            {
                if (input[i] > input[i - 1])
                    numberOfIncreases++;
            }

            return numberOfIncreases;
        }

        public static int GetAnswerPart2()
        {
            var input = ReadInputFromFile();
            var numberOfIncreases = 0;

            for (int i = 1; i < input.Count() - 2; i++)
            {
                var slidingWindow = input[i] + input[i + 1] + input[i + 2];
                var prevSlidingWindow = input[i - 1] + input[i] + input[i + 1];

                if (slidingWindow > prevSlidingWindow)
                    numberOfIncreases++;
            }

            return numberOfIncreases;
        }

        private static List<int> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day1/Day1Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.Select(x => int.Parse(x)).ToList();
        }
    }
}
