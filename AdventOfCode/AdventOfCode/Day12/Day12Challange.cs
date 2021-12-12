using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day12
{
    public static class Day12Challange
    {
        public static int GetAnswerPart1()
        {
            var cavePaths = ReadFileFromInput();
            var routes = GetAllRoutes(cavePaths);
            return routes.Count();
        }

        public static int GetAnswerPart2()
        {
            var cavePaths = ReadFileFromInput();
            var routes = GetAllRoutesAllowTwoSmallCaves(cavePaths);
            return routes.Count();
        }

        private static List<List<CavePath>> GetAllRoutesAllowTwoSmallCaves(List<CavePath> cavePaths)
        {
            var completePaths = new List<List<CavePath>>();

            foreach (var path in cavePaths.Where(x => x.Point1 == "start" || x.Point2 == "start"))
            {
                var startingPoint = path.Point1 == "start" ? path.Point1 : path.Point2;
                var nextPoint = path.Point1 != "start" ? path.Point1 : path.Point2;
                var startingPath = new List<CavePath>() { new CavePath() { Point1 = startingPoint, Point2 = nextPoint } };

                GetPathsAllowTwoSmallCaves(cavePaths, startingPath, nextPoint, ref completePaths, false);
            }

            return completePaths;
        }

        private static void GetPathsAllowTwoSmallCaves(List<CavePath> cavePaths, List<CavePath> path, string currentPosition, ref List<List<CavePath>> completePaths, bool twoSmallCavesVisited)
        {
            var potentialNextMoves = GetPotentialNextMovesAllowTwoSmallCaves(cavePaths, path, currentPosition, twoSmallCavesVisited);

            foreach (var move in potentialNextMoves)
            {
                var newPath = new List<CavePath>(path);
                newPath.Add(move);

                if (newPath.Last().Point1 == "end" || newPath.Last().Point2 == "end")
                {
                    completePaths.Add(newPath);
                    continue;
                }

                var nextPosition = move.Point1 == currentPosition ? move.Point2 : move.Point1;

                var nextPositionWillBeSmallCaveRevisit = false;

                if (nextPosition != "start" && nextPosition != "end" && Char.IsLower(nextPosition[0]))
                {
                    nextPositionWillBeSmallCaveRevisit = path.Any(x => x.Point1 == nextPosition) || path.Any(x => x.Point2 == nextPosition);
                }

                GetPathsAllowTwoSmallCaves(cavePaths, newPath, nextPosition, ref completePaths, nextPositionWillBeSmallCaveRevisit || twoSmallCavesVisited);
            }
        }

        private static List<List<CavePath>> GetAllRoutes(List<CavePath> cavePaths)
        {
            var completePaths = new List<List<CavePath>>();

            foreach (var path in cavePaths.Where(x => x.Point1 == "start" || x.Point2 == "start"))
            {
                var startingPoint = path.Point1 == "start" ? path.Point1 : path.Point2;
                var nextPoint = path.Point1 != "start" ? path.Point1 : path.Point2;
                var startingPath = new List<CavePath>() { new CavePath() { Point1 = startingPoint, Point2 = nextPoint } };

                GetPaths(cavePaths, startingPath, nextPoint, ref completePaths);
            }

            return completePaths;
        }

        private static void GetPaths(List<CavePath> cavePaths, List<CavePath> path, string currentPosition, ref List<List<CavePath>> completePaths)
        {
            var potentialNextMoves = GetPotentialNextMoves(cavePaths, path, currentPosition);

            foreach (var move in potentialNextMoves)
            {
                var newPath = new List<CavePath>(path);
                newPath.Add(move);

                if (newPath.Last().Point1 == "end" || newPath.Last().Point2 == "end")
                {
                    completePaths.Add(newPath);
                    continue;
                }

                var nextPosition = move.Point1 == currentPosition ? move.Point2 : move.Point1;
                GetPaths(cavePaths, newPath, nextPosition, ref completePaths);
            }
        }

        private static List<CavePath> GetPotentialNextMovesAllowTwoSmallCaves(List<CavePath> cavePaths, List<CavePath> path, string currentPosition, bool anySmallCaveVisitedTwice)
        {
            var potentialLocations = new List<CavePath>();
            var linkedLocations = cavePaths.Where(x => x.Point1 == currentPosition || x.Point2 == currentPosition).ToList();

            foreach (var location in linkedLocations)
            {
                var nextDestionation = location.Point1 == currentPosition ? location.Point2 : location.Point1;

                if (nextDestionation == "end")
                {
                    potentialLocations.Add(location);
                    continue;
                }
                else if (nextDestionation == "start")
                {
                    continue;
                }
                else if (Char.IsLower(nextDestionation[0]))
                {
                    if (anySmallCaveVisitedTwice)
                    {
                        if (path.Any(x => x.Point1 == nextDestionation) || path.Any(x => x.Point2 == nextDestionation))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        potentialLocations.Add(location);
                        continue;
                    }                    
                }

                potentialLocations.Add(location);
            }

            return potentialLocations;
        }

        private static List<CavePath> GetPotentialNextMoves(List<CavePath> cavePaths, List<CavePath> path, string currentPosition)
        {
            var potentialLocations = new List<CavePath>();
            var linkedLocations = cavePaths.Where(x => x.Point1 == currentPosition || x.Point2 == currentPosition).ToList();

            foreach (var location in linkedLocations)
            {
                var nextDestionation = location.Point1 == currentPosition ? location.Point2 : location.Point1;

                if (nextDestionation == "end")
                {
                    potentialLocations.Add(location);
                    continue;
                }
                else if (nextDestionation == "start")
                {
                    continue;
                }
                else if (Char.IsLower(nextDestionation[0]))
                {
                    if (path.Any(x => x.Point1 == nextDestionation) || path.Any(x => x.Point2 == nextDestionation))
                    {
                        continue;
                    }
                }
                potentialLocations.Add(location);
            }

            return potentialLocations;
        }

        private static List<CavePath> ReadFileFromInput()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day12/Day12Input.txt");
            var fileLines = File.ReadAllLines(filePath);
            var paths = new List<CavePath>();

            foreach (var line in fileLines)
            {
                var points = line.Split("-");
                var path = new CavePath()
                {
                    Point1 = points[0],
                    Point2 = points[1]
                };

                paths.Add(path);
            }

            return paths;
        }

    }
}
