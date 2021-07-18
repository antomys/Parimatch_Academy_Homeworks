namespace Level2_4
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    internal static class Program
    {
        private const string StopWord = "exit";
        private static readonly Random Random = new();
        private static void Main()
        {
            Console.WriteLine("More or Less game, Volokhovych");
            var input = Input();
            //Calculate(range);
        }

        private static IEnumerable<int> Input()
        {
            long value;
            long value1;
            string? input;
            var tokens = Array.Empty<string>();
            
            do
            {
                while (true)
                {
                    Console.WriteLine("Please input range between 0 and 1.000.000 in format\n[NUMBER],[NUMBER]\nEnter: ");
                    input = Console.ReadLine();
                    if(input == null)
                        continue;
                    break;
                }
                switch (input.ToLower())
                 {
                     case StopWord:
                         Environment.Exit(0);
                         break;
                     case "rules":
                         Rules();
                         input = Console.ReadLine();
                         break;
                 }
                     
                 tokens = input?.Split(',');
                 
                 long.TryParse(tokens[0], out value);
                 long.TryParse(tokens[1], out value1);
                 
            }
            while (Regex.Matches(input ?? string.Empty, @"[a-zA-Z-?`\-+*/{}|.<>]").Count > 0 || tokens.Length > 2 || value < 0 || value > 1000000
                                  || value1 > 1000000 || value > value1);
            
            var range = Array.ConvertAll(tokens, int.Parse);
            Calculate(range);
            return range;
        }
        private static void Rules()
        {
            Console.WriteLine("More or Less is a quiz game in which players have to answer (numerical) questions.\n" +
                              " ... For each question, it is determined whether only guesses that are higher \n" +
                              "than the actual " +
                              "answer will count (more), or only guesses that \n" +
                              "are lower (less).\n");
        }

        private static void Calculate(IReadOnlyList<int> range)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var computerValue = Random.Next(range[0], range[1]);
            var sumRange = 0;
            var fails = 0;
            
            if (range[0] == 0)
                sumRange = range[1] + 1;
            else
            {
                sumRange = range[1] - range[0];
            }
            
            var input = 0;
            do
            {
                Console.WriteLine("\nInput your guess: ");
                
                var userInput = Console.In.ReadLine();
                if(userInput == null)
                    continue;
                
                switch (userInput.ToLower())
                {
                    case StopWord:
                        return;
                    case "rules":
                        Rules();
                        break;
                }
                
                if (Regex.Matches(userInput, @"[a-zA-Z-?`\-+*/{}|.<>]").Count > 0)
                {
                    Console.WriteLine("Wrong input!");
                }
                
                else
                {
                    int.TryParse(userInput, out input);
                    
                    if (input < computerValue)
                    {
                        fails++;
                        Console.WriteLine("Too few!");
                        //todo: score counter
                    }

                    if (input <= computerValue) continue;
                    
                    fails++;
                    Console.WriteLine("Too much!");
                }
            } 
            while (input != computerValue);
            
            stopwatch.Stop();

            var powerSum = PowerOfTwo(sumRange);
            var score = 100.0*(powerSum - fails)/powerSum;
            
            Console.WriteLine($"You guessed! It was {input}\n" +
                              $"Your scored {Math.Round(score,MidpointRounding.AwayFromZero)}\n" +
                              $"And it took you {fails} times to fail, {fails+1}th try is right!\n" +
                              $"Time in game: {stopwatch.ElapsedMilliseconds}ms.");
        }

        private static int PowerOfTwo(int range)
        {
            var power = 0;
            var t = double.MaxValue;

            for (var i = 0; i < range; i++)
            {
                var value = Math.Pow(2, i);
                var a = Math.Abs(Math.Abs(range) - Math.Abs(value));

                if (!(a < t)) continue;
                power = i;
                t = a;
            }

            return power;
        }
    }
}