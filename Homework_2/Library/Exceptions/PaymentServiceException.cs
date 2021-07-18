using System;

namespace Library.Exceptions
{
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException()
        {
            
        }
        public PaymentServiceException(string message) : 
            base(String.Format("Payment has failed! {0}",message))
        {
            
        }
    }
}