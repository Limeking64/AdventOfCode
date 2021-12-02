using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day2
{
    public static class Day2Challange
    {
        public static int GetAnswerPart1()
        {
            var entries = ReadInputFromFile();

            var depth = 0;
            var horizonalPosition = 0;

            foreach (var entry in entries)
            {
                var command = entry.Split(" ");
                var direction = command[0];
                var length = int.Parse(command[1]);

                switch (direction)
                {
                    case "forward":
                        horizonalPosition += length;
                        break;

                    case "down":
                        depth += length;
                        break;

                    case "up":
                        depth -= length;
                        break;

                    default:
                        throw new Exception("Input error");
                }
            }

            return depth * horizonalPosition;
        }

        public static int GetAnswerPart2()
        {
            var entries = ReadInputFromFile();

            var depth = 0;
            var horizonalPosition = 0;
            var aim = 0;

            foreach (var entry in entries)
            {
                var command = entry.Split(" ");
                var direction = command[0];
                var length = int.Parse(command[1]);

                switch (direction)
                {
                    case "forward":
                        horizonalPosition += length;
                        depth += aim * length;
                        break;

                    case "down":
                        aim += length;
                        break;

                    case "up":
                        aim -= length;
                        break;

                    default:
                        throw new Exception("Input error");
                }
            }

            return depth * horizonalPosition;
        }

        private static List<string> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day2/Day2Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.ToList();
        }
    }
}
