using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day9
{
    public static class Day9Challange
    {
        public static int GetAnswerPart1()
        {
            var map = ReadInputFromFile();
            var lowestPoints = GetLowestPoints(map);

            return lowestPoints.Values.Sum(x => x + 1);
        }

        public static int GetAnswerPart2()
        {
            var map = ReadInputFromFile();
            var lowestPoints = GetLowestPoints(map);

            var basins = GetBasins(map, lowestPoints);

            var biggestBasins = basins.OrderByDescending(x => x.Count).Take(3).ToList();

            return biggestBasins[0].Count * biggestBasins[1].Count * biggestBasins[2].Count;
        }

        private static List<Dictionary<(int, int), int>> GetBasins(Dictionary<(int, int), int> map, Dictionary<(int, int), int> lowestPoint)
        {
            var numberOfColumns = map.Keys.Max(x => x.Item1);
            var numberOfRows = map.Keys.Max(x => x.Item2);

            var basins = new List<Dictionary<(int, int), int>>();

            foreach (var point in lowestPoint.Where(x => x.Value != 9))
            {
                var basin = new Dictionary<(int, int), int>();
                CheckAdjacentPoints(point, basin, map);
                basins.Add(basin);
            }

            return basins;
        }

        private static void CheckAdjacentPoints(KeyValuePair<(int, int), int> point, Dictionary<(int, int), int> basin, Dictionary<(int, int), int> map)
        {
            if (!basin.TryGetValue(point.Key, out int value) && point.Value != 9)
            {
                var x = point.Key.Item1;
                var y = point.Key.Item2;

                var checkTop = y != 0;
                var checkRight = x != map.Keys.Max(x => x.Item1);
                var checkBottom = y != map.Keys.Max(x => x.Item2);
                var checkLeft = x != 0;

                basin.Add((x, y), point.Value);

                if (checkTop && map[(x, y - 1)] > map[(x, y)])
                {                   
                    var nextPointToCheck = new KeyValuePair<(int, int), int>((x, y - 1), map[(x, y - 1)]);
                    CheckAdjacentPoints(nextPointToCheck, basin, map);
                }

                if (checkRight && map[(x + 1, y)] > map[(x, y)])
                {                    
                    var nextPointToCheck = new KeyValuePair<(int, int), int>((x + 1, y), map[(x + 1, y)]);
                    CheckAdjacentPoints(nextPointToCheck, basin, map);
                }

                if (checkBottom && map[(x, y + 1)] > map[(x, y)])
                {                    
                    var nextPointToCheck = new KeyValuePair<(int, int), int>((x, y + 1), map[(x, y + 1)]);
                    CheckAdjacentPoints(nextPointToCheck, basin, map);
                }

                if (checkLeft && map[(x - 1, y)] > map[(x, y)])
                {                  
                    var nextPointToCheck = new KeyValuePair<(int, int), int>((x - 1, y), map[(x - 1, y)]);
                    CheckAdjacentPoints(nextPointToCheck, basin, map);
                }
            }
        }

        private static Dictionary<(int, int), int> GetLowestPoints(Dictionary<(int, int), int> map)
        {
            var numberOfColumns = map.Keys.Max(x => x.Item1);
            var numberOfRows = map.Keys.Max(x => x.Item2);

            var lowestPoints = new Dictionary<(int, int), int>();

            for (int i = 0; i <= numberOfRows; i++)
            {
                for (int j = 0; j <= numberOfColumns; j++)
                {
                    var checkTop = i != 0;
                    var checkRight = j != numberOfColumns;
                    var checkBottom = i != numberOfRows;
                    var checkLeft = j != 0;

                    if (checkTop && map[(j, i - 1)] <= map[(j, i)])
                        continue;

                    if (checkRight && map[(j + 1, i)] <= map[(j, i)])
                        continue;

                    if (checkBottom && map[(j, i + 1)] <= map[(j, i)])
                        continue;

                    if (checkLeft && map[(j - 1, i)] <= map[(j, i)])
                        continue;

                    lowestPoints.Add((j, i), map[(j, i)]);
                }
            }

            return lowestPoints;
        }


        private static Dictionary<(int, int), int> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day9/Day9Input.txt");
            var fileLines = File.ReadAllLines(filePath);
            var locations = new Dictionary<(int, int), int>();

            for (int i = 0; i < fileLines.Length; i++)
            {
                var tubes = fileLines[i].ToCharArray();
                var heightOfTubes = tubes.Select(x => int.Parse(x.ToString())).ToList();

                for (int j = 0; j < heightOfTubes.Count(); j++)
                {
                    locations[(j, i)] = heightOfTubes[j];
                }
            }

            return locations;
        }
    }
}
