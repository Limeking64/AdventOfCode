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
    }
}