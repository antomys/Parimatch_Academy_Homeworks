using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_1.Services.Interfaces;

namespace Task_1.Services
{
    public class PrimeAlgorithm : IPrimeAlgorithm
    {
        /// <summary>
        /// Returns async list of primes found in a range
        /// </summary>
        /// <returns>List of primes</returns>
        public async Task<Result> GetPrimes(string stringPrimeFrom,string stringPrimeTo)
        {
            var task = await FindPrimes(stringPrimeFrom,stringPrimeTo);
            if (task is null)
            {
              throw new ArgumentNullException($"Error. Task is null");
            }
            return task;

        }
        /// <summary>
        /// Checks if given number is a prime
        /// </summary>
        /// <returns>boolean</returns>
        public Task<bool> IsPrime(int number)
        {
            if (number < 2)
                throw new ArgumentOutOfRangeException($"Exception. Number {number} is less than 2");
            
            var isPrime =
                Enumerable.Range(2, (int)Math.Sqrt(number) - 1)
                    .All(divisor => number % divisor != 0);
            return Task.FromResult(isPrime);
        }
        /// <summary>
        /// internal method to find primes
        /// </summary>
        /// <returns></returns>
        private static async Task<Result> FindPrimes(string stringPrimeFrom, string stringPrimeTo)
        {
            return await Task.Run(() =>
            {
                var time = DateTime.UtcNow;
                var primes = new List<int>();
                
                try
                {
                    var convertingPrimeFrom = int.TryParse(stringPrimeFrom, out var primeFrom);
                    var convertingPrimeTo = int.TryParse(stringPrimeTo, out var primeTo);
                    if (!convertingPrimeFrom && !convertingPrimeTo)
                    {
                        throw new ArgumentOutOfRangeException(
                            $"Error: Unable to parse variables. Wrong input format");
                
                    }
                    if (primeFrom <= 0)
                        primeFrom = 1;
                    
                    if (primeTo <= 0
                              ||primeFrom > primeTo)
                    {
                        throw new ArgumentOutOfRangeException($"Out of range exception at Prime" +
                                                     $"\nAlgorithm method. Line 28");
                    }
                    
                    for (var number = primeFrom; number <= primeTo; number++)
                    {
                        var counter = 0;

                        for (var i = 2; i <= number / 2; i++)
                        {
                            if (number % i != 0) continue;
                            counter++;
                            break;
                        }

                        if (counter == 0 && number != 1)
                            primes.Add(number);
                    }

                    var elapsedTime = DateTime.Now.Subtract(time).ToString();

                    return new Result(true, String.Empty, elapsedTime, primes);

                }
                catch (Exception exception)
                {
                    return new Result(false, exception.Message, string.Empty, null);
                }
            });
        }
    }
}