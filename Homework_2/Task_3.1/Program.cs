using System;
using Library.Block_3.Types;
using Library.Types;

namespace Task_3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreditCard creditCard = new CreditCard();
            Bank bank = new Privet48();
            creditCard.StartDeposit(50,"USD");
            creditCard.StartWithdrawal(50,"USD");
            bank.StartDeposit(50,"USD");
            bank = new Stereobank();
            bank.StartDeposit(50,"USD");
            var gift = new GiftVoucher();
            gift.StartDeposit(50,"USD");
            gift.StartDeposit(500,"USD");
        }
    }
}