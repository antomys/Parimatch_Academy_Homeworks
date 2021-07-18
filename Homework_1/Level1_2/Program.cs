using System;

namespace Level1_2
{
    internal static class Program
    {
        private static void Main()
        {
            Calculate();
        }

        private static void Calculate()
        {
            var coefficients = new double [3];
            
            Console.WriteLine("Calculation of margin, Volokhovych\n");
            
            Console.WriteLine("Enter first participant name: "); //TODO: CHAN
            var participant1 = Console.ReadLine();
            
            Console.WriteLine("Enter second participant name: ");
            var participant2 = Console.ReadLine();
            
            Console.WriteLine("Enter Coefficients (three of them, submit\n everyone with ENTER:");


            double sum = 0;
            for (var i = 0; i < coefficients.Length; i++)
            {
                var fromConsole = Convert.ToDouble(Console.In.ReadLine());
                coefficients[i] = 1.0/fromConsole*100.0;
                sum += 100/fromConsole;
            }
            
            var margin = Math.Round((sum - 100),2);
            for (var i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = coefficients[i] - margin / 3;
            }
            
            Console.WriteLine($"Win of \"{participant1}\" : {Math.Round(coefficients[0],2, MidpointRounding.ToEven)}% ");
            Console.WriteLine($"Win of \"{participant2}\" : {Math.Round(coefficients[2],2, MidpointRounding.ToEven)}% ");
            Console.WriteLine($"Draw : {Math.Round(coefficients[1],2, MidpointRounding.ToEven)}% ");
            Console.WriteLine($"Bookmaker's margin: {margin}% ");
        }
    }
}