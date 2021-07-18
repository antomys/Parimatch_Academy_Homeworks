namespace Level1_1
{
    using System;
    public static class Level11
    {
        private const  double BirthYear = 2000;
        private const double BirthMonth = 6;
        private const double BirthDay = 12;
        public static void Main()
        {
            Console.WriteLine("Expression counter, Volokhovych Ihor\n");
            Console.WriteLine($"Will be calculated: y = ((e^a+4*lg(c)/sqrt(b))" +
                              "*|arctg(d)|+5/sin(a)");

            while (true)
            {
                Console.WriteLine("Please write an A parameter");
                var tryParse = double.TryParse(Console.ReadLine(), out var a);
                
                if(!tryParse)
                    continue;
                
                Console.WriteLine(Count(a, BirthYear, BirthMonth, BirthDay));
                return;
            }
        }
        private static double Count(double a , double b, double c , double d)
        {
            return (Math.Exp(a) + 4 * Math.Log10(c)) / Math.Sqrt(b)
                * Math.Abs(Math.Atan(d)) + (5 / Math.Sin(a));
        }
    }
}