using System;

namespace Library.Exceptions
{
    public class GiftVoucherUsedVoucher : Exception
    {
        public GiftVoucherUsedVoucher(string message) :
            base(message)
        {
            Console.WriteLine($"This voucher {message} has already been used!");
        }
    }
}