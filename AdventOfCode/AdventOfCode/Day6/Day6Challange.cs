using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day6
{
    public static class Day6Challange
    {
        public static int GetAnswerPart1()
        {
            var input = GetInputFromFile();
            var result = SimulateSpawning(input, 80);

            return result.Count();
        }

        public static long GetAnswerPart2()
        {
            var input = GetInputFromFile();
            var result = SimulateSpawningByGrouping(input, 256);

            return result;
        }

        private static List<int> SimulateSpawning(List<int> input, int numberOfDaysToStimulate)
        {
            for (int i = 0; i < numberOfDaysToStimulate; i++)
            {
                input = input.Select(x => x - 1).ToList();
                var numberOfFishToSpawn = input.Count(x => x == -1);
                for (int j = 0; j < input.Count(); j++)
                {
                    input[j] = (input[j] == -1) ? 6 : input[j];
                }
                input.AddRange(Enumerable.Range(0, numberOfFishToSpawn).Select(x => 8));
            }
           
            return input;
        }

        private static long SimulateSpawningByGrouping(List<int> input, int numberOfDaysToStimulate)
        {
            // Okay man, think            
            var numberOfFishWithTimer = new long[9];

            for (int i = 0; i < input.Count(); i++)
            {
                numberOfFishWithTimer[input[i]] += 1;
            }

            for (int i = 0; i < numberOfDaysToStimulate; i++)
            {
                var numberOfFishReadyToSpawn = numberOfFishWithTimer[0];

                Array.Copy(numberOfFishWithTimer, 1, numberOfFishWithTimer, 0, numberOfFishWithTimer.Length - 1);
                numberOfFishWithTimer[8] = numberOfFishReadyToSpawn;
                numberOfFishWithTimer[6] += numberOfFishReadyToSpawn;
            }

            return numberOfFishWithTimer.Sum();
        }

        private static List<int> GetInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day6/Day6Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries[0].Split(",").Select(x => int.Parse(x)).ToList();
        }
    }
}
