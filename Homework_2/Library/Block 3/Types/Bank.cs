using System;

namespace Library.Types
{
    public abstract class Bank : PaymentMethodBase, ISupportDeposit,ISupportWithdrawal
    {
        protected string[] AvailableCards { get; set; }
        public void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login");
            var login = Console.In.ReadLine();
            Console.WriteLine("Please, enter your password");
            var password = Console.In.ReadLine();
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvailableCards[i]}");
            }

            Int16.TryParse(Console.In.ReadLine(), out short value);
            Console.WriteLine($"You’ve withdraw {amount} {currency} " +
                              $"from your {AvailableCards[value]} card successfully");
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login");
            var login = Console.In.ReadLine();
            Console.WriteLine("Please, enter your password");
            var password = Console.In.ReadLine();
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvailableCards.Length; i++)
            {
                Console.WriteLine($"{i}. {AvailableCards[i]}");
            }

            Int16.TryParse(Console.In.ReadLine(), out short value);
            Console.WriteLine($"You’ve deposit {amount} {currency} " +
                              $"from your {AvailableCards[value]} card successfully");
        }
    }
}