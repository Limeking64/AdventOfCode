using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Day5;
using AdventOfCode.Day6;
using AdventOfCode.ExampleChallange;
using AdventOfCode.ExampleChallangeDay5;
using NUnit.Framework;
using System;

namespace AdventOfCodeTests
{
    public class Tests
    {
        [Test]
        public void ExampleChallange_Test()
        {
            var target = 2020;
            var result = ExampleChallange.GetAnswer(target);

            Console.WriteLine($"The answer to the example challange is {result}");
        }

        [Test]
        public void ExampleChallangeDay5_Part1Test()
        {
            var result = ExampleChallangeDay5.GetAnswer_Part1();

            Console.WriteLine($"The answer to the example challange day 5, part 1, is {result}");
        }

        [Test]
        public void ExampleChallangeDay5_Part2Test()
        {
            var result = ExampleChallangeDay5.GetAnswer_Part2();
            
            Console.WriteLine($"The answer to the example challange day 5, part 2, is {result}");
        }

        [Test]
        public void Day1_Part1Test()
        {
            var result = Day1Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 1, part 1, is {result}");
        }

        [Test]
        public void Day1_Part2Test()
        {
            var result = Day1Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 1, part 2, is {result}");
        }

        [Test]
        public void Day2_Part1Test()
        {
            var result = Day2Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 2, part 1, is {result}");
        }

        [Test]
        public void Day2_Part2Test()
        {
            var result = Day2Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 2, part 2, is {result}");
        }

        [Test]
        public void Day3_Part1Test()
        {
            var result = Day3Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 3, part 1, is {result}");
        }

        [Test]
        public void Day3_Part2Test()
        {
            var result = Day3Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 3, part 2, is {result}");
        }

        [Test]
        public void Day4_Part1Test()
        {
            var result = Day4Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 4, part 1, is {result}");
        }

        [Test]
        public void Day4_Part2Test()
        {
            var result = Day4Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 4, part 2, is {result}");
        }

        [Test]
        public void Day5_Part1Test()
        {
            var result = Day5Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 5, part 1, is {result}");
        }

        [Test]
        public void Day5_Part2Test()
        {
            var result = Day5Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 5, part 2, is {result}");
        }

        [Test]
        public void Day6_Part1Test()
        {
            var result = Day6Challange.GetAnswerPart1();

            Console.WriteLine($"The answer to day 6, part 1, is {result}");
        }

        [Test]
        public void Day6_Part2Test()
        {
            var result = Day6Challange.GetAnswerPart2();

            Console.WriteLine($"The answer to day 6, part 2, is {result}");
        }
    }
}