using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("files\\input.txt").Select(x => int.Parse(x)).ToList();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            MatchTwo(inputs);
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time for Two Matches:{elapsed}ms");


            stopwatch.Start();
            MatchThree(inputs);
            stopwatch.Stop();
            elapsed = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time for Three Matches: {elapsed}ms");


        }

        // One line Linq but not as quick as foreach loops
        private static void MatchTwo(IReadOnlyCollection<int> inputs)
        {
            var answer = (from input in inputs
                          let answerFound = inputs.Where(x => x != input)
                          .FirstOrDefault(potentialInput => input + potentialInput == 2020)
                          where answerFound != 0
                          select input * answerFound).FirstOrDefault();

            Console.WriteLine($"Answer for Two Matches:{answer}");
        }

        // Not the prettiest code to read but its quick
        private static void MatchThree(IReadOnlyCollection<int> inputs)
        {
            var potentiallyValidInputs = inputs.ToList();
            var answer = 0;

            foreach (var input in inputs)
            {
                if (answer != 0)
                    break;

                var firstIndex = 0;
                var secondIndex = 1;

                foreach (var potentialInput in potentiallyValidInputs.TakeWhile(potentialInput => answer == 0))
                {
                    while (firstIndex != potentiallyValidInputs.Count && answer == 0)
                    {
                        if (secondIndex == potentiallyValidInputs.Count)
                        {
                            firstIndex++;
                            secondIndex = 2;
                            continue;
                        }

                        if (input + potentiallyValidInputs[firstIndex] + potentiallyValidInputs[secondIndex] != 2020 || firstIndex == secondIndex)
                        {
                            secondIndex++;
                            continue;
                        }

                        var number1 = potentiallyValidInputs[firstIndex];
                        var number2 = potentiallyValidInputs[secondIndex];
                        answer = potentialInput * number1 * number2;
                    }
                }
                potentiallyValidInputs.Remove(input);
            }

            Console.WriteLine($"Answer for Three Matches:{answer}");
        }

    }
}
