using System;

namespace Library.Exceptions
{
    public class InsufficientFundsException : PaymentServiceException
    {
        public decimal AccBalance { get; }
        public InsufficientFundsException(string message, decimal accBalance) : 
            base(message)
        {
            AccBalance = accBalance;
            Console.WriteLine($"Exception occured because of lack of funds! Available {AccBalance} ");
        }
    }
}