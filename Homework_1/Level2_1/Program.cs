namespace Level2_1
{
    using System;
    using System.Collections.Generic;

    internal static class Program
    {
        private static readonly Dictionary<int, string> GameCache = new();
        private static readonly Dictionary<int, string> Options = new(3)
        {
            {1, "rock"},
            {2, "paper"},
            {3, "scissors"}
        };
        
        private static readonly Random Random = new();
        private static int _gameId;
        
        private static void Main()
        {
            Console.WriteLine("Rock,Paper,Scissors game. Vokokhovych");
            Calculate();
        }

        private static void Help()
        {
            Console.WriteLine("A player who decides to play rock will beat another player who has chosen scissors\n" +
                              "(\"rock crushes scissors\" or sometimes \"blunts scissors\"),but will lose to one who has\n" +
                              "played paper (\"paper covers rock\"); a play of paper will lose\n" +
                              "lose to a play of scissors (\"scissors cuts paper\").");
        }

        private static void Commands()
        {
            Console.WriteLine("Available commands:\n Exit - close programme;\n Help - prints game rules\n" +
                              "Game commands:\n rock - for rock\n paper - for paper\n scissors - for scissors.");
        }
        private static void Calculate()
        {
            var option = "";
            Commands();
            
            while (option !="exit")
            {
                option = Console.In.ReadLine();
                if (option == null)
                {
                    Console.WriteLine("\n**Error, try again**\n");
                    Commands();
                    Calculate();
                }
                else
                {
                    option = option.ToLower();
                }
                
                string computerOption;
                var output = "";
                
                switch (option)
                {
                    case "exit":
                        foreach(var (key, value) in GameCache)
                            Console.WriteLine($"Game {key} {value}");
                        Environment.Exit(0);
                        break;
                    case "help":
                        Help();
                        break;
                    case "rock":
                        computerOption = Options[Random.Next(1, 4)];
                        _gameId++;
                        output = computerOption switch
                        {
                            "rock" => $"User:{option} = {computerOption} : Computer. DRAW",
                            "paper" => $"User:{option} < {computerOption} : Computer. Computer Wins",
                            _ => $"User:{option} > {computerOption} : Computer. User Wins"
                        };
                        GameCache.Add(_gameId,output);
                        break;
                    
                    case "paper":
                        computerOption = Options[Random.Next(1, 4)];
                        _gameId++;
                        output = computerOption switch
                        {
                            "paper" => $"User:{option} = {computerOption} : Computer. DRAW",
                            "rock" => $"User:{option} > {computerOption} : Computer. User Wins",
                            _ => $"User:{option} < {computerOption} : Computer. Computer Wins"
                        };
                        GameCache.Add(_gameId,output);
                        break;
                    case "scissors":
                        computerOption = Options[Random.Next(1, 4)];
                        _gameId++;
                        output = computerOption switch
                        {
                            "scissors" => $"User:{option} = {computerOption} : Computer. DRAW",
                            "paper" => $"User:{option} > {computerOption} : Computer. User Wins",
                            _ => $"User:{option} < {computerOption} : Computer. Computer Wins"
                        };
                        GameCache.Add(_gameId,output);
                        break;
                    
                    default:
                        Console.WriteLine("Error! Try again");
                        Calculate();
                        break;
                }
                Console.WriteLine(output);
            }
        }
    }
}