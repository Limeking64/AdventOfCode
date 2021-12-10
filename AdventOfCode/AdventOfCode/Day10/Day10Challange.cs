using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day10
{
    public static class Day10Challange
    {
        private static readonly Dictionary<char, int> _illegalCharacterPoints = new Dictionary<char, int>()
        {
                {')', 3 },
                {']', 57 },
                {'}', 1197 },
                {'>', 25137 }
        };

        private static readonly Dictionary<char, int> _incompleteSequencePoints = new Dictionary<char, int>()
        {
                {')', 1 },
                {']', 2 },
                {'}', 3 },
                {'>', 4 }
        };

        private static readonly char[] _openingCharacters = new char[] { '(', '[', '{', '<' };
        private static readonly char[] _closingCharacters = new char[] { ')', ']', '}', '>' };

        public static int GetAnswerPart1()
        {
            var input = ReadInputFromFile();
            var corruptedLines = GetCorruptedLines(input);

            return corruptedLines.Sum(x => _illegalCharacterPoints[x.Item2]);
        }

        public static long GetAnswerPart2()
        {
            var input = ReadInputFromFile();
            var corruptedLines = GetCorruptedLines(input);

            var incompleteLines = input.Except(corruptedLines.Select(x => x.Item1)).ToList();

            var missingCharacters = incompleteLines.Select(x => FindClosingCharacters(x)).ToList();

            var scores = missingCharacters.Select(x => GetIncompleteSequenceScore(x)).OrderBy(x => x).ToList();

            return scores[scores.Count() / 2];
        }

        private static long GetIncompleteSequenceScore(string missingCharacters)
        {
            long score = 0;

            foreach (var missingCharacter in missingCharacters)
            {
                score *= 5;
                score += _incompleteSequencePoints[missingCharacter];
            }

            return score;
        }

        private static string FindClosingCharacters(string lineToCheck)
        {
            var expectedClosingCharacters = new List<char>();
            for (int i = 0; i < lineToCheck.Length; i++)
            {
                if (_openingCharacters.Contains(lineToCheck[i]))
                {
                    expectedClosingCharacters.Insert(0, GetExpectedClosingCharacter(lineToCheck[i]));
                    continue;
                }

                if (_closingCharacters.Contains(lineToCheck[i]))
                {
                    if (expectedClosingCharacters[0] == lineToCheck[i])
                    {
                        expectedClosingCharacters.RemoveAt(0);
                        continue;
                    }
                }
            }

            return new string(expectedClosingCharacters.ToArray());
        }

        private static List<(string, char)> GetCorruptedLines(List<string> linesToCheck)
        {
            var corruptedLines = new List<(string, char)>();

            foreach (var line in linesToCheck)
            {
                if (TryGetCorruptedLine(line, out char? corruptedCharacter))
                    corruptedLines.Add((line, corruptedCharacter.Value));
            }

            return corruptedLines;
        }

        private static bool TryGetCorruptedLine(string lineToCheck, out char? corruptedCharacter)
        {
            var expectedClosingCharacters = new List<char>();
            corruptedCharacter = null;
            for (int i = 0; i < lineToCheck.Length; i++)
            {
                if (_openingCharacters.Contains(lineToCheck[i]))
                {
                    expectedClosingCharacters.Insert(0, GetExpectedClosingCharacter(lineToCheck[i]));
                    continue;
                }

                if (_closingCharacters.Contains(lineToCheck[i]))
                {
                    if (expectedClosingCharacters[0] == lineToCheck[i])
                    {
                        expectedClosingCharacters.RemoveAt(0);
                        continue;
                    }
                    else
                    {
                        corruptedCharacter = lineToCheck[i];
                        return true;
                    }
                }
            }

            return false;
        }

        private static char GetExpectedClosingCharacter(char openingCharacter)
        {
            switch (openingCharacter)
            {
                case '(':
                    return ')';
                case '[':
                    return ']';
                case '{':
                    return '}';
                case '<':
                    return '>';
                default:
                    throw new Exception("Invalid character");
            }
        }

        private static List<string> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day10/Day10Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.ToList();
        }
    }
}
