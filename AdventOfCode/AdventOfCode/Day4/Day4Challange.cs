using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day4
{
    public static class Day4Challange 
    {
        private static readonly int bingoCardColumns = 5;
        private static readonly int bingoCardRows = 5;

        public static int GetAnswerPart1()
        {
            var drawInput = ReadDrawInputFromFile();
            var bingoCards = ReadCardsFromFile();
            List<string> calledDraws;

            var winningCardPos = GetPostionOfWinningCard(drawInput, bingoCards, out calledDraws);

            return GetScore(ReadCardsFromFile()[winningCardPos], calledDraws);
        }

        public static int GetAnswerPart2()
        {
            var drawInput = ReadDrawInputFromFile();
            var bingoCards = ReadCardsFromFile();
            List<string> calledDraws;
            var lastCardToWinPos = GetPostionOfLastWinningCard(drawInput, bingoCards, out calledDraws);

            return GetScore(ReadCardsFromFile()[lastCardToWinPos], calledDraws);
        }

        private static int GetScore(string[][] winningCard, List<string> drawInput)
        {
            int winningSum = 0;

            foreach (var row in winningCard)
            {
                foreach (var entry in row)
                {
                    if (!drawInput.Contains(entry))
                    {
                        winningSum += int.Parse(entry);
                    }
                }
            }

            return winningSum * int.Parse(drawInput.Last());
        }

        private static int GetPostionOfWinningCard(List<string> drawInput, List<string[][]> bingoCards, out List<string> calledDraws)
        {
            var bingoCardCollection = new List<string[][]>();
            bingoCardCollection.AddRange(bingoCards);

            calledDraws = new List<string>();

            foreach (var draw in drawInput)
            {
                calledDraws.Add(draw);
                for (int i = 0; i < bingoCardCollection.Count(); i++)
                {
                    for (int j = 0; j < bingoCardRows; j++)
                    {
                        for (int k = 0; k < bingoCardColumns; k++)
                        {
                            if (bingoCardCollection[i][j][k] == draw)
                            {
                                bingoCardCollection[i][j][k] = "";

                                if (CardHasWon(bingoCardCollection[i]))
                                {
                                    return i;
                                }
                            }
                        }
                    }
                }
            }

            throw new Exception("No winning Card");
        }

        private static int GetPostionOfLastWinningCard(List<string> drawInput, List<string[][]> bingoCards, out List<string> calledDraws)
        {
            // I hate everything I've done here so much

            var bingoCardCollection = new List<string[][]>();
            bingoCardCollection.AddRange(bingoCards);
            calledDraws = new List<string>();
            var cardsStillInGame = Enumerable.Range(0, bingoCards.Count()).ToList();

            foreach (var draw in drawInput)
            {
                calledDraws.Add(draw);
                for (int i = 0; i < bingoCardCollection.Count(); i++)
                {
                    for (int j = 0; j < bingoCardRows; j++)
                    {
                        for (int k = 0; k < bingoCardColumns; k++)
                        {
                            if (bingoCardCollection[i][j][k] == draw)
                            {
                                bingoCardCollection[i][j][k] = "";

                                if (CardHasWon(bingoCardCollection[i]) && cardsStillInGame.Count(x => x >= 0) != 1)
                                {
                                    cardsStillInGame[i] = -1;
                                }

                                if (cardsStillInGame.Count(x => x >= 0) == 1 && CardHasWon(bingoCardCollection[cardsStillInGame.Where(x => x > 0).First()]))
                                {
                                    return cardsStillInGame.Where(x => x > 0).First();
                                }
                            }
                        }
                    }
                }
            }

            throw new Exception("No winning Card");
        }

        private static bool CardHasWon(string[][] card)
        {

            foreach (var row in card)
            {
                if (row.All(x => x == ""))
                {
                    return true;
                }
            }

            for (int i = 0; i < bingoCardColumns; i++)
            {
                if (card.All(x => x[i] == ""))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<string> ReadDrawInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day4/Day4DrawInput.txt");
            var entries = File.ReadAllLines(filePath);

            return entries[0].Split(',').ToList();
        }

        private static List<string[][]> ReadCardsFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day4/Day4CardInput.txt");

            var bingoCardCollection = new List<string[][]>();

            var bingoCard = new string[bingoCardColumns][];
            var currentBingoCardRowCard = 0;

            foreach (var line in File.ReadLines(filePath))
            {

                if (currentBingoCardRowCard >= 0)
                    bingoCard[currentBingoCardRowCard] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                currentBingoCardRowCard++;

                if (currentBingoCardRowCard == bingoCardRows)
                {
                    bingoCardCollection.Add(bingoCard);
                    bingoCard = new string[bingoCardColumns][];
                    currentBingoCardRowCard = -1;
                    continue;
                }


            }

            return bingoCardCollection;
        }
    }


}
