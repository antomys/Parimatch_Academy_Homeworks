using System;

namespace Library.Exceptions
{
    public class OverLimitCreditCard: Exception
    {
        public OverLimitCreditCard(string message) :
            base(message)
        {
            Console.WriteLine($"Limit of 3000 UAH per transaction was exceeded! Tried to get {message}");
        }
    }
}