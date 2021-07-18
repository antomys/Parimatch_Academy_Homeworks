using System;
using Library;

namespace Task_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            paymentService.StartDeposit(50,"USD");
            paymentService.StartWithdraw(50,"USD");
            paymentService.StartDeposit(500,"USD");
            paymentService.StartWithdraw(500,"USD");
        }
    }
}