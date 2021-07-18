using System;

namespace Library.Exceptions
{
    public class LimitExceededException : PaymentServiceException
    {
        public decimal Limit { get; }
        public LimitExceededException(string message, decimal limit):
            base(message)
        {
            Limit = limit;
        }
        public LimitExceededException(string message):
            base(String.Format($"Limit has been exceded! {message}"))
        {
            
        }
    }
}