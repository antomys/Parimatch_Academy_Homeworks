using System;
using System.Runtime.Serialization.Formatters;
using System.Security.Policy;
using Library;

namespace Task_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            BetService betService = new BetService();
            var odd = 0m;
            Console.WriteLine("1. 10 times getting odd");
            for (int i = 0; i < 10; i++)
            {
                odd = (decimal)betService.GetOdds();
                var result = betService.Bet(100);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {result}");
            }

            Console.WriteLine("2. Odd > 12");
            for (int i = 1; i <= 3; i++)
            {
                betService.min = 12;
                odd = (decimal)betService.GetOdds();
                var result = betService.Bet(100);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {result}");
            }
            Game();
        }

        public static void Game()
        {
            Console.WriteLine("3.");
            var sum = 10000m;
            BetService betService = new BetService();
            betService.min = 1.25m;
            betService.max = 2.3m;
            var betSum = 0;
            var result = 0m;
            do
            {
                betSum = new Random().Next(50, 5000);
                if(sum - betSum > 0)
                {
                    sum -= betSum;
                    result = betService.Bet(betSum);
                    sum += result;
                    Console.WriteLine($"Bet {betSum} , won {result}, sum {sum}");
                }

                if (sum < 50)
                {
                    result = betService.Bet(sum);
                    sum = 0;
                    sum += result;
                    Console.WriteLine($"Bet {betSum} , won {result}, sum {sum}");
                }
                
            } while (sum != 0 && sum < 150000);

            Console.WriteLine($"Game over. My balance is {sum}");
        }
    }
}