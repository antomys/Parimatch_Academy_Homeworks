using System;

namespace Library.Exceptions
{
    public class MonobankOverLimitException : Exception
    {
        public MonobankOverLimitException(string message):
            base(message)
        {
            Console.WriteLine($"Limit of 7000UAH was exceeded! Tried to get {message} UAH");
        }
    }
}