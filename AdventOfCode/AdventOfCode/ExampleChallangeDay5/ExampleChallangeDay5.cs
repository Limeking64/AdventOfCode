using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.ExampleChallangeDay5
{
    public static class ExampleChallangeDay5
    {
        private readonly static int _rowCharactersCount = 7;
        private readonly static int _columnCharactersCount = 3;
        private readonly static int _totalNumberOfRows = 128;
        private readonly static int _totalNumberOfColumns = 8;

        public static int GetAnswer_Part1()
        {            
            return GetExistingSeatIds().OrderBy(x => x).LastOrDefault();
        }

        public static int GetAnswer_Part2()
        {
            var existingSeatIds = GetExistingSeatIds().OrderBy(x => x);
            return Enumerable.Range(existingSeatIds.FirstOrDefault(), existingSeatIds.LastOrDefault() - existingSeatIds.FirstOrDefault()).Except(existingSeatIds).FirstOrDefault();
        }

        private static List<int> GetExistingSeatIds()
        {
            var otherBoardingPasses = ReadBoardingPassesFromFile();

            var existingRowNumbers = otherBoardingPasses.Select(x => GetRowNumber(x.Substring(0, 7))).ToList();
            var existingColumnNumbers = otherBoardingPasses.Select(x => GetColumnNumber(x.Substring(7, 3))).ToList();

            var existingSeats = new List<int>();

            for (int i = 0; i < otherBoardingPasses.Count(); i++)
            {
                existingSeats.Add(GetSeatNumber(existingRowNumbers[i], existingColumnNumbers[i]));
            }

            return existingSeats;
        }

        private static int GetSeatNumber(int row, int column)
        {
            return (row * 8) + column;
        }

        private static int GetRowNumber(string rowCharacters)
        {
            if (rowCharacters.Count() == _rowCharactersCount)
            {
                var minRowNumber = 0;
                var maxRowNumber = _totalNumberOfRows - 1;

                for (int i = 0; i < _rowCharactersCount; i++)
                {
                    var midPoint = (maxRowNumber + minRowNumber) / 2;

                    if (rowCharacters[i] == 'F')
                    {
                        maxRowNumber = midPoint;
                    }
                    else
                    {
                        minRowNumber = midPoint + 1;
                    }
                }

                return minRowNumber;
            }

            throw new Exception("The amount of row characters provided is invalid.");
        }

        private static int GetColumnNumber(string columnCharacters)
        {
            if (columnCharacters.Count() == _columnCharactersCount)
            {
                var minColumnNumber = 0;
                var maxColumnNumber = _totalNumberOfColumns - 1;

                for (int i = 0; i < _columnCharactersCount; i++)
                {
                    var midPoint = (maxColumnNumber + minColumnNumber) / 2;

                    if (columnCharacters[i] == 'L')
                    {
                        maxColumnNumber = midPoint;
                    }
                    else
                    {
                        minColumnNumber = midPoint + 1;
                    }
                }

                return minColumnNumber;
            }

            throw new Exception("The amount of column characters provided is invalid.");
        }

        private static List<string> ReadBoardingPassesFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "ExampleChallangeDay5/OtherBoardingPasses.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.ToList();
        }
    }
}