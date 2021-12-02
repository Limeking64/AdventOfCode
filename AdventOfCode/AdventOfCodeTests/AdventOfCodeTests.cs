using AdventOfCode.Day1;
using AdventOfCode.Day2;
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
    }
}