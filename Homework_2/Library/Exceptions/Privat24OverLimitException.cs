using System;

namespace Library.Exceptions
{
    public class Privat24OverLimitException : Exception
    {
        public Privat24OverLimitException(string message) :
            base(message)
        {
            Console.WriteLine($"Limit of 10000 UAH per transaction from all cards was exceeded! " +
                              $"Tried to get {message}");
        }
    }
}