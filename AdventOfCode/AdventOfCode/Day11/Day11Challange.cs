using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day11
{
    public static class Day11Challange
    {
        public static int GetAnswerPart1()
        {
            var input = ReadInputFromFile();
            var result = GetNumberOfFlashesOverNumberOfSteps(input, 100);

            return result;
        }

        public static int GetAnswerPart2()
        {
            var input = ReadInputFromFile();
           

            return GetNumberOfFlashesToSyncronise(input);
        }

        private static int GetNumberOfFlashesToSyncronise(Dictionary<(int, int), int> octopuses)
        {
            int numberOfFlashes = 0;
            int stepNumber = 0;

            while (numberOfFlashes != octopuses.Count())
            {
                numberOfFlashes = 0;
                stepNumber++;
                UpdateOctopusEnergy(octopuses);
                var flashedOctopuses = new List<(int, int)>();
                SimulateFlashing(octopuses, flashedOctopuses, ref numberOfFlashes);

                foreach (var flashedOctopus in flashedOctopuses)
                {
                    octopuses[flashedOctopus] = 0;
                }
            }

            return stepNumber;
        }

        private static int GetNumberOfFlashesOverNumberOfSteps(Dictionary<(int, int), int> octopuses, int numberOfStepsToStimulate)
        {
            int numberOfFlashes = 0;

            for (int i = 0; i < numberOfStepsToStimulate; i++)
            {
                var flashedOctopuses = new List<(int, int)>();
                UpdateOctopusEnergy(octopuses);
                SimulateFlashing(octopuses, flashedOctopuses, ref numberOfFlashes);

                foreach (var flashedOctopus in flashedOctopuses)
                {
                    octopuses[flashedOctopus] = 0;
                }

            }

            return numberOfFlashes;
        }

        private static void SimulateFlashing(Dictionary<(int, int), int> octopuses, List<(int, int)> flashedOctopuses, ref int numberOfFlashes)
        {
            if (octopuses.Where(x => !flashedOctopuses.Contains(x.Key)).Where(x => x.Value > 9).Any())
            {
                foreach (var octopus in octopuses.Keys.ToList())
                {
                    if (octopuses[octopus] > 9 && !flashedOctopuses.Contains(octopus))
                    {
                        flashedOctopuses.Add(octopus);
                        numberOfFlashes += 1;

                        var x = octopus.Item1;
                        var y = octopus.Item2;

                        var updateTop = y != 0;
                        var updateRight = x != octopuses.Keys.Max(x => x.Item1);
                        var updateBottom = y != octopuses.Keys.Max(x => x.Item2);
                        var updateLeft = x != 0;
                        var updateTopLeft = updateTop && updateLeft;
                        var updateTopRight = updateTop && updateRight;
                        var updateBottomRight = updateBottom && updateRight;
                        var updateBottomLeft = updateBottom && updateLeft;

                        if (updateTopLeft)
                            octopuses[(x - 1, y - 1)] = octopuses[(x - 1, y - 1)] + 1;

                        if (updateTop)
                            octopuses[(x, y - 1)] = octopuses[(x, y - 1)] + 1;

                        if (updateTopRight)
                            octopuses[(x + 1, y - 1)] = octopuses[(x + 1, y - 1)] + 1;

                        if (updateRight)
                            octopuses[(x + 1, y)] = octopuses[(x + 1, y)] + 1;

                        if (updateBottomRight)
                            octopuses[(x + 1, y + 1)] = octopuses[(x + 1, y + 1)] + 1;

                        if (updateBottom)
                            octopuses[(x, y + 1)] = octopuses[(x, y + 1)] + 1;

                        if (updateBottomLeft)
                            octopuses[(x - 1, y + 1)] = octopuses[(x - 1, y + 1)] + 1;

                        if (updateLeft)
                            octopuses[(x - 1, y)] = octopuses[(x - 1, y)] + 1;

                    }
                }

                SimulateFlashing(octopuses, flashedOctopuses, ref numberOfFlashes);
            }
        }

        private static void UpdateOctopusEnergy(Dictionary<(int, int), int> octopuses)
        {
            foreach (var octopus in octopuses.Keys.ToList())
            {
                octopuses[octopus] = octopuses[octopus] + 1;
            }
        }

        private static Dictionary<(int, int), int> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day11/Day11Input.txt");
            var fileLines = File.ReadAllLines(filePath);
            var octopuses = new Dictionary<(int, int), int>();

            for (int y = 0; y < fileLines.Length; y++)
            {
                for (int x = 0; x < fileLines[y].Length; x++)
                {
                    octopuses[(x, y)] = int.Parse(fileLines[y][x].ToString());
                }
            }

            return octopuses;
        }
    }
}
