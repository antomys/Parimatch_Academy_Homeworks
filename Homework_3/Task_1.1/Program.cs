using System;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Task_1._1
{
    class Program
    {
        private int[] ConvertedArray;
        static void Main()
        {
            Console.WriteLine("Array statistics using LINQ, Volokhovych");
            Help();
            ArrayStatistics arrayStatistics = new ArrayStatistics();
            arrayStatistics.ConvertToString();
            arrayStatistics.arrayStatistics();
            arrayStatistics.SortArray();

        }
        public static void Help()
        {
            Console.WriteLine("This task is made to take your array" +
                              "and give you out statistics about it.");
        }
    }
    internal class ArrayStatistics
    {
        private string Input { get; set; }
        private int[] ConvertedArray { get;set; }

        public int[] ConvertToString()
        {
            try
            {
                Input = Console.ReadLine();
                if (String.IsNullOrEmpty(Input)) //todo:refactor
                {
                    throw new Exception(); //todo: Custom exception
                }

                /*int[] convertedArray = Input.Split(new char[] {','},
                    StringSplitOptions.RemoveEmptyEntries).ToArray().Select(int.Parse).ToArray();
                    */
                ConvertedArray = Input.Split(new char[] {','},
                    StringSplitOptions.RemoveEmptyEntries).ToArray().Select(int.Parse).ToArray();
                return ConvertedArray;

            }
            catch
            {
                Console.WriteLine("Wrong input. Try again");
            }
            ConvertToString();
            return new[] {0};
        }

        

        public void arrayStatistics()
        {
            //ConvertedArray = ConvertToString();
            Console.WriteLine("Array statistics");
            Console.WriteLine($"Minimal element: {ConvertedArray.Min()}");
            Console.WriteLine($"Maximal element: {ConvertedArray.Max()}");
            Console.WriteLine($"Sum of elements: {ConvertedArray.Sum()}");
            var mean = ConvertedArray.Average();
            Console.WriteLine($"Average: {mean}");
            var squaredDiffs = Math.Sqrt(ConvertedArray.Select(x => (x - mean) * (x - mean)).Sum() / ConvertedArray.Length);
            Console.WriteLine($"Standard Deviation: {squaredDiffs}");
        }

        public void SortArray()
        {
            //ConvertedArray = ConvertToString();
            var sortedunique = (from VAR in ConvertedArray
                    .OrderBy(x => x)
                select VAR).Distinct();

            Console.WriteLine("Sorted by Ascending array:");
            foreach (var VARIABLE in sortedunique)
            {
                Console.WriteLine(VARIABLE);
            }
        }
    }
    
}