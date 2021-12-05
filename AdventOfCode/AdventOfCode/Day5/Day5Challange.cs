using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day5
{
    public static class Day5Challange
    {
        public static int GetAnswerPart1()
        {
            var lines = ReadInputFromFile();
            var markedMap = MarkPointsOnMap(lines, false);

            return markedMap.Count(x => x.Value > 1);
        }

        public static int GetAnswerPart2()
        {
            var lines = ReadInputFromFile();
            var markedMap = MarkPointsOnMap(lines, true);

            return markedMap.Count(x => x.Value > 1);
        }

        public static Dictionary<(int, int), int> MarkPointsOnMap(List<Line> lines, bool includeDiagonals)
        {
            var map = new Dictionary<(int, int), int>();

            foreach (var line in lines)
            {
                if (line.StartingY == line.EndingY)
                {
                    for (int i = Math.Min(line.StartingX, line.EndingX); i <= Math.Max(line.StartingX, line.EndingX); i++)
                    {
                        var pointToMark = (i, line.StartingY);
                        map.TryGetValue(pointToMark, out int numberOfTimeMarked);
                        map[pointToMark] = numberOfTimeMarked + 1;
                    }
                }
                else if (line.StartingX == line.EndingX)
                {
                    for (int i = Math.Min(line.StartingY, line.EndingY); i <= Math.Max(line.StartingY, line.EndingY); i++)
                    {
                        var pointToMark = (line.StartingX, i);
                        map.TryGetValue(pointToMark, out int numberOfTimeMarked);
                        map[pointToMark] = numberOfTimeMarked + 1;
                    }
                }
                else if (includeDiagonals)
                {
                    var isNegativeXDirection = line.StartingX > line.EndingX;
                    var isNegativeYDirection = line.StartingY > line.EndingY;

                    for (int i = 0; i <= Math.Abs(line.StartingX - line.EndingX); i++)
                    {
                        var xPos = isNegativeXDirection ? line.StartingX - i : line.StartingX + i;
                        var yPos = isNegativeYDirection ? line.StartingY - i : line.StartingY + i;

                        var pointToMark = (xPos, yPos);
                        map.TryGetValue(pointToMark, out int numberOfTimeMarked);
                        map[pointToMark] = numberOfTimeMarked + 1;
                    }
                }
            }

            return map;
        }

        public static List<Line> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day5/Day5Input.txt");
            var entries = File.ReadAllLines(filePath);
            var lines = new List<Line>();

            foreach (var entry in entries)
            {
                var coords = entry.Split(" -> ");
                var line = new Line()
                {
                    StartingX = int.Parse(coords[0].Split(',')[0]),
                    StartingY = int.Parse(coords[0].Split(',')[1]),
                    EndingX = int.Parse(coords[1].Split(',')[0]),
                    EndingY = int.Parse(coords[1].Split(',')[1]),
                };

                lines.Add(line);
            }

            return lines;
        }
    }
}
