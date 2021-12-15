using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day14
{
    public static class Day14Challange
    {
        public static long GetAnswerPart1()
        {
            var polymerTemplate = GetPolymerTemplateFromFile();
            var rules = GetPairInsertionRulesFromFile();

            var updatedPolyerTemplate = GetUpdatedSequence(in polymerTemplate, rules, 10);

            long mostCommonElementOccurance = updatedPolyerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Count();
            long leastCommonElementOccurance = updatedPolyerTemplate.GroupBy(x => x).OrderBy(x => x.Count()).First().Count();           

            return mostCommonElementOccurance - leastCommonElementOccurance;
        }

        public static long GetAnswerPart2()
        {
            var polymerTemplate = GetPolymerTemplateFromFile();
            var rules = GetPairInsertionRulesFromFile();
            var charCounter = new Dictionary<string, long>();

            var groupedResult = GetUpdatedSequenceByGrouping(in polymerTemplate, rules, 40, ref charCounter);           

            return charCounter.Max(x => x.Value) - charCounter.Min(x => x.Value);
        }

        private static Dictionary<string, long> GetUpdatedSequenceByGrouping(in string polymerTemplate, Dictionary<string, string> rules, int numberOfStepsToSimulate, ref Dictionary<string, long> charCounter)
        {
            var initalPairsInSequence = new List<string>();

            for (int i = 0; i < polymerTemplate.Length - 1; i++)
            {
                initalPairsInSequence.Add(polymerTemplate.Substring(i, 2));                
            }

            for (int i = 0; i < polymerTemplate.Length; i++)
            {
                if (charCounter.ContainsKey(polymerTemplate[i].ToString()))
                {
                    charCounter[polymerTemplate[i].ToString()] = charCounter[polymerTemplate[i].ToString()] + 1;
                }
                else
                {
                    charCounter.Add(polymerTemplate[i].ToString(), 1);
                }
            }

            var groups = initalPairsInSequence.GroupBy(x => x).ToDictionary(x => x.Key, y => (long)y.Count());

            var updatedGroup = groups;
           
            for (int i = 0; i < numberOfStepsToSimulate; i++)
            {
                updatedGroup = GetGroup(updatedGroup, rules, ref charCounter);
            }

            return updatedGroup;
        }

        private static Dictionary<string, long> GetGroup(Dictionary<string, long> initalGrouping, Dictionary<string, string> rules, ref Dictionary<string, long> charCount)
        {
            var updatedGrouping = new Dictionary<string, long>();

            foreach (var group in initalGrouping)
            {
                if (rules.ContainsKey(group.Key))
                {
                    var firstNewKey = group.Key[0] + rules[group.Key];

                    if (updatedGrouping.ContainsKey(firstNewKey))
                    {
                        updatedGrouping[firstNewKey] = updatedGrouping[firstNewKey] + group.Value;
                    }
                    else
                    {
                        updatedGrouping.Add(firstNewKey, group.Value);
                    }
                   
                    var secondNewKey = rules[group.Key] + group.Key[1];                   

                    if (updatedGrouping.ContainsKey(secondNewKey))
                    {
                        updatedGrouping[secondNewKey] = updatedGrouping[secondNewKey] + group.Value;
                    }
                    else
                    {
                        updatedGrouping.Add(secondNewKey, group.Value);
                    }

                    if (charCount.ContainsKey(rules[group.Key]))
                    {
                        charCount[rules[group.Key]] = charCount[rules[group.Key]] + group.Value;
                    }
                    else
                    {
                        charCount.Add(rules[group.Key], group.Value);
                    }
                }
                else
                {
                    updatedGrouping.Add(group.Key, group.Value);
                }
            }

            return updatedGrouping;
        }

        private static string GetUpdatedSequence(in string polymerTemplate, Dictionary<string, string> rules, int numberOfStepsToSimulate)
        {
            var updatedPolymerTemplate = polymerTemplate;

            for (int i = 0; i < numberOfStepsToSimulate; i++)
            {
                updatedPolymerTemplate = GetUpdatedSequence(updatedPolymerTemplate, rules);    
            }

            return updatedPolymerTemplate;
        }

        private static string GetUpdatedSequence(string sequence, Dictionary<string, string> rules)
        {
            var updatedSequence = sequence;

            for (int i = 0; i < updatedSequence.Length - 1; i++)
            {
                if (rules.ContainsKey(updatedSequence.Substring(i, 2)))
                {
                    var rulesTransformation = rules[updatedSequence.Substring(i, 2)];
                    updatedSequence = updatedSequence.Insert(i + 1, rulesTransformation);
                    i++;
                }
            }

            return updatedSequence;
        }

        private static string GetPolymerTemplateFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day14/Day14Input.txt");
            var fileLines = File.ReadAllLines(filePath);

            return fileLines[0];
        }

        private static Dictionary<string, string> GetPairInsertionRulesFromFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Day14/Day14Input.txt");
            var fileLines = File.ReadAllLines(filePath);
            var rules = new Dictionary<string, string>();

            for (int i = 2; i < fileLines.Length; i++)
            {
                var rule = fileLines[i].Split(" -> ");
                rules.Add(rule[0], rule[1]);
            }

            return rules;
        }
    }
}
