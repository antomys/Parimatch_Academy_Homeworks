using System;
using Library;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.\n");
            Console.WriteLine("Creating first account (EUR):");
            var EURaccount = new Account("EUR");
            Console.WriteLine($"Created with {EURaccount.Id}");
            
            Console.WriteLine("Creating second account (USD):");
            var USDaccount = new Account("USD");
            Console.WriteLine($"Created with {USDaccount.Id}");
            
            Console.WriteLine("Creating third account (UAH):");
            var UAHaccount = new Account("UAH");
            Console.WriteLine($"Created with {UAHaccount.Id}");

            Console.WriteLine("\n2.\n");
            Console.WriteLine("Adding 10 EUR to EUR account");
            EURaccount.Deposit(10,"EUR");
            Console.WriteLine($"Account with {EURaccount.Id} has {EURaccount.GetBalance("EUR")}" +
                              $" EUR");

            Console.WriteLine("\n3.\n");
            Console.WriteLine("WIthdraw 3 UAH from EUR account");
            EURaccount.Withdraw(3,"UAH");
            Console.WriteLine($"Account with {EURaccount.Id} has {EURaccount.GetBalance("EUR")}" +
                              $" EUR");
            
            Console.WriteLine("\n4.\n");
            Console.WriteLine("Adding 121 USD to UAH account");
            UAHaccount.Deposit(121,"USD");
            Console.WriteLine($"Account with {UAHaccount.Id} has {UAHaccount.GetBalance("UAH")}" +
                              $" UAH");
            
            Console.WriteLine("\n5.\n");
            Console.WriteLine("Withdrawing 5 USD from USD account");
            try
            {
                USDaccount.Withdraw(5,"USD");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            Console.WriteLine("\n6.\n");
            Console.WriteLine("Creating account with PLN");
            try
            {
                Account account = new Account("PLN");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            Console.WriteLine("\n7.\n");
            Console.WriteLine("All balances:");
            USDaccount.GetBalance("USD");
            Console.WriteLine($"Account with currency {EURaccount.Currency} has " +
                              $"{EURaccount.GetBalance(EURaccount.Currency)} balance\n");
            Console.WriteLine($"Account with currency {USDaccount.Currency} has " +
                              $"{USDaccount.GetBalance(USDaccount.Currency)} balance\n");
            Console.WriteLine($"Account with currency {UAHaccount.Currency} has " +
                              $"{UAHaccount.GetBalance(UAHaccount.Currency)} balance\n");
        }
    }
}