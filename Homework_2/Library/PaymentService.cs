using System;
using System.Collections.Generic;
using Library.Block_3.Types;
using Library.Exceptions;
using Library.Types;

namespace Library
{
    public class PaymentService
    {
        class TransactionTracker
        {
            public string BankName;
            public decimal Amount;
        }
        private readonly PaymentMethodBase[] _availablePaymentMethod;
        private Dictionary<int, TransactionTracker> _dictionary = new Dictionary<int, TransactionTracker>();
        private int idx = 0;

        public PaymentService()
        {
            _availablePaymentMethod = new PaymentMethodBase[]
            {
                new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher()
            };
        }

        public void StartDeposit(decimal amount, string currency)
        {
            short input;
            Console.WriteLine("Please choose Payment Method:");
            for (int i = 0; i < _availablePaymentMethod.Length; i++)
            {
                if(_availablePaymentMethod[i] is ISupportDeposit)
                    Console.WriteLine($"{i+1}. {_availablePaymentMethod[i].Name}");
            }
            do
            {
                Int16.TryParse(Console.In.ReadLine(), out input);
                input -= 1;
                if(input < 0 || input > _availablePaymentMethod.Length)
                    Console.WriteLine("Invalid. Please try again");
            } while (input < 0 && input > _availablePaymentMethod.Length);
            
            var deposit = (ISupportDeposit)_availablePaymentMethod[input];
            var nameOfBank = _availablePaymentMethod[input].Name.ToLower();
            var convertedtoUkr = OutsideToInside(amount, currency);
            Exception_Handler(nameOfBank,convertedtoUkr);
            deposit.StartDeposit(amount,currency);
            _dictionary.Add(idx,new TransactionTracker {Amount = convertedtoUkr, BankName = nameOfBank});

        }
        public void StartWithdraw(decimal amount, string currency)
        {
            short input;
            Console.WriteLine("Please choose Payment Method:");
            for (int i = 0; i < _availablePaymentMethod.Length; i++)
            {
                if(_availablePaymentMethod[i] is ISupportWithdrawal)
                    Console.WriteLine($"{i+1}. {_availablePaymentMethod[i].Name}");
            }
            do
            {
                Int16.TryParse(Console.In.ReadLine(), out input);
                input -= 1;
                if(input < 0 || input > _availablePaymentMethod.Length)
                    Console.WriteLine("Invalid. Please try again");
            } while (input < 0 && input > _availablePaymentMethod.Length);
            var withdraw = (ISupportWithdrawal)_availablePaymentMethod[input];
            withdraw.StartWithdrawal(amount,currency);
        }
        private decimal OutsideToInside(decimal outsideAmount,string outsideCurrency)
        {
            if(outsideCurrency != "uah")
                switch (outsideCurrency)
                {
                    case "EUR":
                        return Decimal.Multiply(outsideAmount, 33.63m);
                    case "USD":
                        return Decimal.Multiply(outsideAmount, 28.36m);
                    default:
                        return outsideAmount;
                }
            return outsideAmount;
        }

        private void Exception_Handler(string nameofBank, decimal amount)
        {
            switch (nameofBank)
            {
                case "creditcard":
                    if(amount>=3000)
                        throw new OverLimitCreditCard(amount.ToString());
                    break;
                case "priver48":
                    int counter = 0; var sum = 0m;
                    foreach (var VARIABLE in _dictionary)
                    {
                        if (VARIABLE.Value.BankName == "priver48")
                        {
                            counter++;
                            sum += VARIABLE.Value.Amount;
                        }
                    }
                    if(counter>1 && sum>=10000 || amount>=10000)
                        throw new Privat24OverLimitException(amount.ToString());
                    break;
                case "stereobank":
                    counter = 0; sum = 0m;
                    foreach (var VARIABLE in _dictionary)
                    {
                        if (VARIABLE.Value.BankName == "stereobank")
                        {
                            counter++;
                            sum += VARIABLE.Value.Amount;
                        }
                    }
                    if(counter>2 && sum>=7000)
                        throw new MonobankOverLimitException(amount.ToString());
                    else if(amount>=3000)
                        throw new MonobankInternetLimit(amount.ToString());
                    break;
                default:
                    var percent = new Random().Next(0,100);
                    if(percent<=2)
                        throw new PaymentServiceException();
                    else
                    {
                        break;
                    }
                    
            }
            
        }
        
    }
}