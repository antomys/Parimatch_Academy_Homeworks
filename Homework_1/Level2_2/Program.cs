namespace Level2_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    internal static class Program
    {
        private static int Main(string[] args)
        {
            
            if (args == null || args.Length == 0)
            {
                Greeting();
                Mode();
            }
            else
            {
                Greeting();
                return Mode(args);
            }
            return 0;
        }

        private static void Greeting()
        {
            Console.WriteLine("Calculating S of \n" +
                              "Round,Square,Triangle and Rect.\n" +
                              "Volokhovych");
            Console.WriteLine(
                "NOTE: Input format: [name of figure],[first_parameter],[second_parameter]\nExamples:\n" +
                "\"rect,2,5\"\n\"\"square,2\"\n\"round,5\"\n\"triangle,4,2,6\"\n\"rect,10,12\"\nNOTE:Excessive parameters will be ignored!");
        }
        
        private static List<int> InputToList(IEnumerable<string> args)
        {
            return (from argument in args where CheckNumberInput(argument) select int.Parse(argument)).ToList();
        }

        private static void Mode()
        {
            while (true)
            {
                try
                {
                    var tokens = Console.ReadLine()?.Trim().Split(',');
                    
                    if (tokens != null && tokens[0].ToLower().Equals("exit"))
                        return;
                    
                    if (tokens==null || tokens.Length < 2)
                        throw new ArgumentOutOfRangeException($"Tokens are null or wrong length");
                    
                    
                    var input = InputToList(tokens);

                    var result = (int) Calculate(tokens[0], input);

                    if (result == -1)
                    {
                        return;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"**Error in written form**\n");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("**Something is too big or small**\n");
                }
            }
        }
        
        private static int Mode(IReadOnlyList<string> args)
        {
            try
            {

                var tokens = args;
                    
                if (tokens != null && tokens[0].ToLower().Equals("exit"))
                    return -1;
                    
                if (tokens==null || tokens.Count < 2)
                    throw new ArgumentOutOfRangeException($"Tokens are null or wrong length");
                    
                    
                var input = InputToList(tokens);

                var result = (int) Calculate(tokens[0], input);

                if (result == -1)
                {
                    return -1;
                }

                return result;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"**Error in written form**\n");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("**Something is too big or small**\n");
            }

            return -500;
        }

        private static bool CheckNumberInput(string argument)
        {
            return Regex.Matches(argument, @"[a-zA-Z-?`\-+#1#{}|.<>]").Count <= 0 
                   || int.TryParse(argument, out var value) && value > 0 && value < int.MaxValue;
        }

        private static double Calculate(string operation, IReadOnlyList<int> arguments)
        {
            try
            {
                var output = -500.0;
                
                switch (operation)
                {
                    case "rect":
                        output = Rect(arguments[0], arguments[1]);
                        break;
                    case "round":
                        output = Round(arguments[0]);
                        break;
                    case "square":
                        output = Square(arguments[0]);
                        break;
                    case "triangle":
                        output = Triangle(arguments[0], arguments[1], arguments[2]);
                        if (double.IsNaN(output))
                            throw new Exception();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown Operation. Try again.");
                        break;
                }
                Console.WriteLine($"S of your {operation} is {output}");
                return output;
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured. No figure like that!");
                return -1;
            }
        }

        #region Figures
      
        private static double Round(double r)
        {
            //S=pi*r^2
            return Math.PI*Math.Pow(r,2);
        }
        private static double Square(double a)
        {
            //S=a^2
            return Math.Pow(a,2);
        }

        private static double Rect(double a, double b)
        {
            //S=ab
            return a*b;
        }

        private static double Triangle(double a, double b, double c)
        {
            //Heron's Formulae
            var p = (a + b + c) / 2.0;
            return Math.Sqrt(p*(p-a)*(p-b)*(p-c));
        }
        
        #endregion
    }
}