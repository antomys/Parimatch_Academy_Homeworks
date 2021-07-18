using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Level1_4
{
    internal static class Program
    {
        private static void Main()
        {
            Calculate();
        }

        private static void Calculate()
        {
            Console.WriteLine("Finding prime, Volokhovych\n");
            
            while(true)
            {
                Console.WriteLine("ENTER RANGE IN FORMAT:\nNUMBER,NUMBER");
            
                var input = Console.ReadLine();
                if(string.IsNullOrEmpty(input))
                    continue;
                
                var tokens = input.Split(',');
                var range = Array.ConvertAll(tokens, int.Parse);

                if(!IsRightInput(range))
                {
                    continue;
                }

                PrimeStart(range);
                Console.WriteLine("\nPlease enter any key to exit......");
                Console.ReadKey();
                    break;
            }   
        }
        
        private static ArrayList prime_num(IReadOnlyList<int> range)
        {
            var primes = new ArrayList();
            for (long i = range[0]; i <= range[1]; i++)
            {
                var isPrime = true;
                for (long j = 2; j < i; j++)
                {
                    if (i % j != 0) continue;
                    isPrime = false;
                    break;
                }

                if (i == 1 || i == 0)
                    isPrime = false;
                if (isPrime)
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        private static void PrimeStart(IReadOnlyList<int> range)
        {
            var primes = prime_num(range);
            
            if (primes.Count < 1)
            {
                Console.WriteLine("There are no primes in this range.");
            }
            else
            {
                foreach(var prime in primes)
                    Console.Write($"Found primes: {prime} ");
            }
        }
        private static bool IsRightInput(IEnumerable<int> range)
        {
            return range.All(t => t > 0);
        }
    }
}