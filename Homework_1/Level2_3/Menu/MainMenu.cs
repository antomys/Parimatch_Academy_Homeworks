namespace Level2_3.Menu
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ArrayOperations;
    internal class MainMenu : IMainMenu
    {
        private readonly StringBuilder _arguments = new ();

        public MainMenu(IEnumerable arguments)
        {
            _arguments.Append(arguments);
        }

        public int DialogMode()
        {
            while (true)
            {
                try
                {
                    Console.Write("Please enter length of array : ");
                    var tryParse = int.TryParse(Console.ReadLine(), out var arrayLength);

                    if (!tryParse || arrayLength <= 0)
                    {
                        throw new ArgumentNullException();
                    }
                    
                    Console.WriteLine($"Now let's declare all {arrayLength} elements!");

                    var arrayOptions = new ArrayOptions();

                    arrayOptions.InputRange(arrayLength);
                    
                    Console.WriteLine("\nNow performing operations..........\n");
                    
                    var returned = arrayOptions.Calculate();

                    if (returned == 0)
                        return returned;
                    return -1;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("***You did wrong by typing this!***\n***Try again!***\n");
                }
            }
        }
        public int ConsoleMode(string[] args)
        {
            var input = Array.ConvertAll(args, int.Parse);

            if (!CheckInput(input))
                return -1;
            var arrayLength = ArrayConverter(input);
            
            Console.WriteLine($"Now let's declare all {arrayLength} elements!");
            
            var arrayOptions = new ArrayOptions();

            arrayOptions.InputRange(arrayLength);
                    
            Console.WriteLine("\nNow performing operations..........\n");
                    
            var returned = arrayOptions.Calculate();

            if (returned == 0)
                return returned;
            return -1;
        }

        private static bool CheckInput(IReadOnlyCollection<int> input)
        {
            return input.Count > 0;
        }

        private static int ArrayConverter(IReadOnlyCollection<int> input)
        {
            return input
                .Select((t, i) => t * Convert.ToInt32(Math.Pow(10, input.Count - i - 1)))
                .Sum();
        }
    }
}