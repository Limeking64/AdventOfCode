using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day13
{
    public static class Day13Challange
    {
        public static int GetAnswerPart1()
        {
            var dots = GetDotsFromFile();

            var postions = new Dictionary<(int, int), bool>();

            for (int y = 0; y <= dots.Max(k => k.Item2); y++)
            {
                for (int x = 0; x <= dots.Max(k => k.Item1); x++)
                {
                    var coord = (x, y);
                    postions.Add(coord, dots.Contains(coord));
                }
            }

            var instructions = GetInstructionsFromFile();

            var updatedPositions = FoldFromOneInstruction(postions, instructions.First());
            
            return updatedPositions.Count(x => x.Value);
        }

        public static void GetAnswerPart2()
        {
            var dots = GetDotsFromFile();

            var postions = new Dictionary<(int, int), bool>();

            for (int y = 0; y <= dots.Max(k => k.Item2); y++)
            {
                for (int x = 0; x <= dots.Max(k => k.Item1); x++)
                {
                    var coord = (x, y);
                    postions.Add(coord, dots.Contains(coord));
                }
            }

            var instructions = GetInstructionsFromFile();

            foreach (var instruction in instructions)
            {
                postions = FoldFromOneInstruction(postions, instruction);
            }

            WritePositionsToFile(postions);
        }


        private static Dictionary<(int, int), bool> FoldFromOneInstruction(Dictionary<(int, int), bool> orginalPositions, Instruction instruction)
        {
            var updatedMap = new Dictionary<(int, int), bool>();

            if (instruction.FoldHorizontal)
            {
                updatedMap = orginalPositions.Where(x => x.Key.Item2 < instruction.LineToFold).ToDictionary(x => x.Key, y => y.Value);

                var positionsToMove = orginalPositions.Where(x => x.Key.Item2 > instruction.LineToFold && x.Value).ToList();

                foreach (var position in positionsToMove)
                {
                    var verticalDistanceFromFoldingPoint = Math.Abs(position.Key.Item2 - instruction.LineToFold);
                    updatedMap[(position.Key.Item1, instruction.LineToFold - verticalDistanceFromFoldingPoint)] = true;
                }
            }
            else
            {

                updatedMap = orginalPositions.Where(x => x.Key.Item1 < instruction.LineToFold).ToDictionary(x => x.Key, y => y.Value);

                var positionsToMove = orginalPositions.Where(x => x.Key.Item1 > instruction.LineToFold && x.Value).ToList();

                foreach (var position in positionsToMove)
                {
                    var verticalDistanceFromFoldingPoint = Math.Abs(instruction.LineToFold - position.Key.Item1);
                    updatedMap[(instruction.LineToFold - verticalDistanceFromFoldingPoint, position.Key.Item2)] = true;
                }
            }

            return updatedMap;
        }

        private static List<(int, int)> GetDotsFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day13/Day13Input_Dots.txt");
            var fileLines = File.ReadAllLines(filePath);
            var dots = new List<(int, int)>();

            foreach (var line in fileLines)
            {
                var coords = line.Split(",");
                dots.Add((int.Parse(coords[0]), int.Parse(coords[1])));
            }

            return dots;
        }

        private static List<Instruction> GetInstructionsFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day13/Day13Input_Instructions.txt");
            var fileLines = File.ReadAllLines(filePath);
            var instructions = new List<Instruction>();

            foreach (var line in fileLines)
            {
                if (line.Contains("x"))
                {
                    instructions.Add(new Instruction() { FoldHorizontal = false, LineToFold = int.Parse(line.Split("x=")[1]) });
                }
                else
                {
                    instructions.Add(new Instruction() { FoldHorizontal = true, LineToFold = int.Parse(line.Split("y=")[1]) });
                }
            }

            return instructions;
        }

        private static void WritePositionsToFile(Dictionary<(int, int), bool> positions)
        {
            for (int y = 0; y <= positions.Max(k => k.Key.Item2); y++)
            {
                for (int x = 0; x <= positions.Max(k => k.Key.Item1); x++)
                {
                    Console.Write(positions[(x, y)] ? "#" : "  ");

                    if (x == positions.Max(k => k.Key.Item1))
                    {
                        Console.Write("\n");
                    }
                }
            }
        }
    }
}
