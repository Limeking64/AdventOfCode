using AdventOfCode.ExampleChallange;
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
    }
}