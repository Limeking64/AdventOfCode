using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day3
{
    public static class Day3Challange
    {
        public static int GetAnswerPart1()
        {
            var input = ReadInputFromFile();
            var binaryLength = input[0].Length;

            string gammaBinary = "";
            string epsilonBinary = "";
            for (int i = 0; i < binaryLength; i++)
            {
                var numberOf0Bits = input.Count(x => x[i] == '0');
                var numberOf1Bits = input.Count(x => x[i] == '1');

                gammaBinary += numberOf0Bits > numberOf1Bits ? "0" : "1";
                epsilonBinary += numberOf0Bits > numberOf1Bits ? "1" : "0";
            }

            return Convert.ToInt32(gammaBinary, 2) * Convert.ToInt32(epsilonBinary, 2);
        }

        public static int GetAnswerPart2()
        {
            var input = ReadInputFromFile();

            var oxygenRating = GetOxygenBinary(input);

            var co2Rating = GetCO2Binary(input);
           
            return Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(co2Rating, 2);
        }

        private static string GetOxygenBinary(List<string> input)
        {
            var binaryLength = input[0].Length;

            var oxygenRating = "";
            var potOxygenRating = new List<string>();
            potOxygenRating.AddRange(input);

            for (int i = 0; i < binaryLength; i++)
            {
                var numberOf0Bits = potOxygenRating.Count(x => x[i] == '0');
                var numberOf1Bits = potOxygenRating.Count(x => x[i] == '1');

                if (numberOf1Bits >= numberOf0Bits)
                {
                    potOxygenRating = potOxygenRating.Where(x => x[i] == '1').ToList();
                }
                else
                {
                    potOxygenRating = potOxygenRating.Where(x => x[i] == '0').ToList();
                }

                if (potOxygenRating.Count() == 1)
                {
                    oxygenRating = potOxygenRating[0];
                }
            }

            return oxygenRating;
        }

        private static string GetCO2Binary(List<string> input)
        {
            var binaryLength = input[0].Length;

            var co2Rating = "";
            var potCO2Rating = new List<string>();
            potCO2Rating.AddRange(input);

            for (int i = 0; i < binaryLength; i++)
            {
                var numberOf0Bits = potCO2Rating.Count(x => x[i] == '0');
                var numberOf1Bits = potCO2Rating.Count(x => x[i] == '1');

                if (numberOf1Bits >= numberOf0Bits)
                {                  
                    potCO2Rating = potCO2Rating.Where(x => x[i] == '0').ToList();
                }
                else
                {                    
                    potCO2Rating = potCO2Rating.Where(x => x[i] == '1').ToList();
                }

                if (potCO2Rating.Count() == 1)
                {
                    co2Rating = potCO2Rating[0];
                }
            }

            return co2Rating;
        }

        private static List<string> ReadInputFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day3/Day3Input.txt");
            var entries = File.ReadAllLines(filePath);

            return entries.Select(x => x).ToList();
        }
    }
}
