using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day15
{
    public static class Day15Challange
    {
        public static int GetAnswerPart1()
        {
            // Dijkstra, what a lad 

            var positions = GetInputFromFile();
            var distancesFromStartingPoint = GetShortestPathUsingDijstra(in positions);

            var rowCount = positions.Max(x => x.Key.Item2);
            var columnCount = positions.Max(x => x.Key.Item1);

            return distancesFromStartingPoint[(columnCount, rowCount)];
        }

        // This is horrible, takes over 3 hours to run 
        public static int GetAnswerPart2()
        {
            var positions = GetInputFromFile();
            var updatedMapPositions = FormFullMap(positions);
            var distancesFromStartingPoint = GetShortestPathUsingDijstra(in updatedMapPositions);

            var rowCount = updatedMapPositions.Max(x => x.Key.Item2);
            var columnCount = updatedMapPositions.Max(x => x.Key.Item1);

            return distancesFromStartingPoint[(columnCount, rowCount)];
        }

        private static Dictionary<(int, int), int> FormFullMap(in Dictionary<(int, int), int> positions)
        {
            var updatedMap = new Dictionary<(int, int), int>(positions);
            var rowCount = positions.Max(x => x.Key.Item2);
            var columnCount = positions.Max(x => x.Key.Item1);

            for (int yy = 0; yy < 5; yy++)
            {
                for (int xx = 0; xx < 5; xx++)
                {
                    if (xx == 0 && yy == 0)
                        continue;

                    for (int y = 0; y <= positions.Max(x => x.Key.Item2); y++)
                    {
                        for (int x = 0; x <= positions.Max(x => x.Key.Item1); x++)
                        {
                            var newPos = (x + ((columnCount + 1) * xx), y + ((rowCount + 1) * yy));
                            var newValue = positions[(x, y)] + (1 * (xx + yy));
                            if (newValue > 9)
                            {
                                newValue = newValue % 9;
                            }

                            updatedMap[newPos] = newValue;
                        }
                    }

                }                
            }

            return updatedMap;
        }

        private static Dictionary<(int, int), int> GetShortestPathUsingDijstra(in Dictionary<(int, int), int> positions)
        {
            var distancesFromStartingPoint = SetUpDisatncesFromStartingPoint(positions);
            var queue = new Dictionary<(int, int), int>(positions);

            while (queue.Any())
            {
                var pos = distancesFromStartingPoint.OrderBy(x => x.Value).Where(y => queue.ContainsKey(y.Key)).First();

                queue.Remove(pos.Key);

                if (positions.ContainsKey((pos.Key.Item1, pos.Key.Item2 - 1)) && queue.ContainsKey((pos.Key.Item1, pos.Key.Item2 - 1)))
                {
                    var potenitalBestDistance = distancesFromStartingPoint[pos.Key] + positions[(pos.Key.Item1, pos.Key.Item2 - 1)];
                    if (potenitalBestDistance < distancesFromStartingPoint[(pos.Key.Item1, pos.Key.Item2 - 1)])
                    {
                        distancesFromStartingPoint[(pos.Key.Item1, pos.Key.Item2 - 1)] = potenitalBestDistance;
                    }
                }

                if (positions.ContainsKey((pos.Key.Item1 + 1, pos.Key.Item2)) && queue.ContainsKey((pos.Key.Item1 + 1, pos.Key.Item2)))
                {
                    var potenitalBestDistance = distancesFromStartingPoint[pos.Key] + positions[(pos.Key.Item1 + 1, pos.Key.Item2)];
                    if (potenitalBestDistance < distancesFromStartingPoint[(pos.Key.Item1 + 1, pos.Key.Item2)])
                    {
                        distancesFromStartingPoint[(pos.Key.Item1 + 1, pos.Key.Item2)] = potenitalBestDistance;
                    }
                }

                if (positions.ContainsKey((pos.Key.Item1, pos.Key.Item2 + 1)) && queue.ContainsKey((pos.Key.Item1, pos.Key.Item2 + 1)))
                {
                    var potenitalBestDistance = distancesFromStartingPoint[pos.Key] + positions[(pos.Key.Item1, pos.Key.Item2 + 1)];
                    if (potenitalBestDistance < distancesFromStartingPoint[(pos.Key.Item1, pos.Key.Item2 + 1)])
                    {
                        distancesFromStartingPoint[(pos.Key.Item1, pos.Key.Item2 + 1)] = potenitalBestDistance;
                    }
                }

                if (positions.ContainsKey((pos.Key.Item1 - 1, pos.Key.Item2)) && queue.ContainsKey((pos.Key.Item1 - 1, pos.Key.Item2)))
                {
                    var potenitalBestDistance = distancesFromStartingPoint[pos.Key] + positions[(pos.Key.Item1 - 1, pos.Key.Item2)];
                    if (potenitalBestDistance < distancesFromStartingPoint[(pos.Key.Item1 - 1, pos.Key.Item2)])
                    {
                        distancesFromStartingPoint[(pos.Key.Item1 - 1, pos.Key.Item2)] = potenitalBestDistance;
                    }
                }
            }

            return distancesFromStartingPoint;
        }

        private static Dictionary<(int, int), int> SetUpDisatncesFromStartingPoint(Dictionary<(int, int), int> positions)
        {
            var distancesFromStartingPoint = new Dictionary<(int, int), int>() { { (0, 0), 0 } };

            foreach (var pos in positions)
            {
                if (!distancesFromStartingPoint.Keys.Contains(pos.Key))
                {
                    // Invalid distance, as we do not know what the shortest distance is yet
                    distancesFromStartingPoint[(pos.Key.Item1, pos.Key.Item2)] = 10000;
                }
            }

            return distancesFromStartingPoint;
        }

        private static Dictionary<(int, int), int> GetInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day15/Day15Jack.txt");
            var fileLines = File.ReadAllLines(filePath);
            var positions = new Dictionary<(int, int), int>();

            for (int y = 0; y < fileLines.Length; y++)
            {
                for (int x = 0; x < fileLines[y].Length; x++)
                {
                    positions[(x, y)] = int.Parse(fileLines[y][x].ToString());
                }
            }

            return positions;
        }
    }
}
