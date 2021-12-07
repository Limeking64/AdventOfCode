using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day7
{
    public static class Day7Challange
    {
        public static int GetAnswerPart1()
        {
            var crabPositions = GetInputFromFile();

            var potentialFuelCosts = new List<int>();           

            for (int i = crabPositions[0]; i <= crabPositions.Last(); i++)
            {
                potentialFuelCosts.Add(crabPositions.Select(x => Math.Abs(x - i)).Sum());
            }

            return potentialFuelCosts.Min();
        }

        public static int GetAnswerPart2()
        {
            var crabPositions = GetInputFromFile();

            var potentialFuelCosts = new List<int>();            

            for (int i = crabPositions[0]; i <= crabPositions.Last(); i++)
            {
                potentialFuelCosts.Add(crabPositions.Select(x => CalculateFuelCostInSequence(Math.Abs(x - i))).Sum());
            }

            return potentialFuelCosts.Min();
        }

        private static int CalculateFuelCostInSequence(int absDifference)
        {
            return (int)((0.5 * absDifference) * (1 + absDifference));
        }

        private static List<int> GetInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day7/Day7Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries[0].Split(",").Select(x => int.Parse(x)).OrderBy(x => x).ToList();
        }
    }
}
