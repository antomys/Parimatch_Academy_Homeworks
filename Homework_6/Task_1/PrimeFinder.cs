using System;
using System.Diagnostics;
using System.Linq;

namespace Task_1
{
    public class PrimeFinder
    {
        public PrimeFinder(int from, int to)
        {
            From = from;
            To = to;
        }
        private int From { get;}
        private int To { get; }

        public string NumberOfPrimesLinq()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var primesLinq = Enumerable
                .Range(From, To - From)
                .Count(x => Enumerable.Range(2, (int) Math.Sqrt(x) - 1)
                    .All(y => x % y != 0));
            
            stopwatch.Stop();

            return $"Found {primesLinq} primes with LINQ in {stopwatch.ElapsedMilliseconds} ms";
        }

        public string NumberOfPrimesPlinq()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var primesLinq = Enumerable
                .Range(From, To - From)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .Count(x => Enumerable.Range(2, (int) Math.Sqrt(x) - 1)
                    .All(y => x % y != 0));

            return $"Found {primesLinq} primes with PLINQ in {stopwatch.ElapsedMilliseconds} ms";
        }
    }
}