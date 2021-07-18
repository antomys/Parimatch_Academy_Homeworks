using System;
using Library.Exceptions;

namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new LimitExceededException("InsufficientException",0);
            }
            catch (InsufficientFundsException e)
            {
                Console.WriteLine(e);
            }
            catch (LimitExceededException e)
            {
                Console.WriteLine(e);
            }
            catch (PaymentServiceException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}