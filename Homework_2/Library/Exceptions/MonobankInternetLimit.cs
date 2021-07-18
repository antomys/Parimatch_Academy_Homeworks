using System;

namespace Library.Exceptions
{
    public class MonobankInternetLimit : Exception
    {
        public MonobankInternetLimit(string message) :
            base(message)
        {
            Console.WriteLine($"Limit of 3000 UAH per transaction was fault! Tried to get {message}\n" +
                              $"Please increase internet limit");
        }
    }
}