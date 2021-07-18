using System;

namespace Level1_3
{
    internal static class Program
    {
        private const double Epsilon = 1.0 / 2000;
        private static void Main()
        {
            Console.WriteLine("Hello World!");
            Calculate();
        }

        private static void Calculate()
        {
            Console.WriteLine("Calculating number series, Volokhovych");
            Console.WriteLine($"Will be calculated:\n Sum from i=1 to infinity of 1/i*(i+1)\n Accuracy epsilon {Epsilon}");
            
            var result = 0.0;
            
            for (var i = 1; i < float.PositiveInfinity; i++)
            {
                var element = 1.0 / (i * (i + 1.0));
                if(element>Epsilon)
                    result += element;
                else
                    break;
            }

            Console.WriteLine($"Result is : {result}");
        }
    }
}