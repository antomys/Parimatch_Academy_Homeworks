using System;
using System.Linq;
using static System.Int32;

namespace Task_1
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Volokhovych, prime counter LINQ and PLINQ");
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Write exit to exit");
                Console.WriteLine("*********************************");
                Console.WriteLine("Please enter range separated by comma\nExample: 2,5");
                
                
                var input = Console.ReadLine()?.Split(',');
                if(string.Join("",input!).ToLower().Equals("exit")) return;
                if (string.IsNullOrEmpty(input.ToString()) || input.Length < 2)
                {
                    PrintError();
                    continue;
                }

                var myInts = input!.Select(Parse).ToArray();
                if (myInts[0] < 1 || myInts[1] <= 1
                                  || myInts[1] < myInts[0])
                {
                    PrintError();
                    continue;
                }

                if (myInts[0] == 1)
                {
                    myInts[0]++;
                }

                var primeFinder = new PrimeFinder(myInts[0], myInts[1]);
                Console.WriteLine(" 1.LINQ\n 2.PLINQ\n 3.Exit");
                TryParse(Console.ReadLine(), out var choose);

                switch (choose)
                {
                    case 1:
                        PrintSuccess(primeFinder.NumberOfPrimesLinq());
                        break;
                    case 2:
                        PrintSuccess(primeFinder.NumberOfPrimesPlinq());
                        break;
                    case 3:
                        return;
                    default:
                        PrintError();
                        continue;
                }
            }
        }

        static void PrintError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Input. Try again!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintSuccess(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
