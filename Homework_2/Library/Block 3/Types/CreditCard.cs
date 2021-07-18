using System;
using System.Text.RegularExpressions;

namespace Library.Block_3.Types
{
    public class CreditCard: PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard()
        {
            Name = "CreditCard";
        }
        
        public void StartDeposit(decimal amount, string currency)
        {
            var input = "";
            var date = "";
            var cvv = "";
            
            Console.WriteLine("Please enter 16-digit Card number");
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            do
            {
                input = Console.In.ReadLine();
                if(!expression.IsMatch(input))
                    Console.WriteLine("Invalid number. Please try again");
            } while (!expression.IsMatch(input));

            Console.WriteLine("Please enter expiry date");
            expression = new Regex(@"^(0[1-9]|1[0-2]|[1-9])\/(1[4-9]|[2-9][0-9]|20[1-9][1-9])$");
            do
            {
                date = Console.In.ReadLine();
                if(!expression.IsMatch(date))
                    Console.WriteLine("Invalid number. Please try again");
            }while (!expression.IsMatch(date));
            
            Console.WriteLine("Please enter CVV");
            expression = new Regex(@"^[0-9]{3}$");
            do
            {
                cvv = Console.In.ReadLine();
                if(!expression.IsMatch(cvv))
                    Console.WriteLine("Invalid number. Please try again");
            }while (!expression.IsMatch(cvv));

            Console.WriteLine("Success");
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            var input = "";
            Console.WriteLine("Please enter 16-digit Card number");
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            do
            {
                input = Console.In.ReadLine();
                if(!expression.IsMatch(input))
                    Console.WriteLine("Invalid number. Please try again");
            } while (!expression.IsMatch(input));
            Console.WriteLine("Success");
        }
    }
}