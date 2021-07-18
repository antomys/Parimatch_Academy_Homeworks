using System;
using Library;

namespace Task_4._2
{
    class Program
    {
        static PaymentService paymentService = new PaymentService();
        
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Exceptions(paymentService);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            } while (Console.In.ReadLine() != "exit");
            
        }

        static void Exceptions(PaymentService paymentService)
        {
            Console.WriteLine("Please enter currency. Available: UAH,USD,EUR");
            var currency = Console.ReadLine().ToUpper();
            if (currency != "USD" && currency != "EUR" && currency != "UAH")
            {
                Console.WriteLine("Try again.");
                Exceptions(paymentService);
            }
            Console.WriteLine("Please enter amount");
            decimal amount = 0m;
            Decimal.TryParse(Console.ReadLine(), out amount);
            paymentService.StartDeposit(amount,currency);
        }
    }
}