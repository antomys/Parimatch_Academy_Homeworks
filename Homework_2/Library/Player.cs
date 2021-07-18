using System;
using System.Collections.Generic;
using Library.Exceptions;

namespace Library
{
    
    public class Player
    {
        public List<int> randomList = new List<int>();
        
        public Random a = new Random();
        private int GenerateUniqueRandom(int id)
        {
            int Generated = 0;
            do
            {
                Generated = a.Next(100000, 100000000);
                if (!randomList.Contains(Generated))
                {
                    randomList.Add(Generated);
                    return Generated;
                }
            } while (!randomList.Contains(Generated));

            throw new LimitExceededException("No more unique from", randomList.Count);
        }
        
        private readonly Dictionary<int, Player> _dictionary;
        public Player(string firstName, string lastName, string email, string password, string currency)
        {
            Account = new Account(currency);
            Id = GenerateUniqueRandom(Id);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            _dictionary=new Dictionary<int, Player> { { Id, this } };
        }
        private int Id { get;}
        private string FirstName { get;}
        private string LastName { get; }
        public string Email { get; }
        private string Password { get; }
        public Account Account { get; }

        public bool IsPasswordValid(string passord)
        {
            if (Password == passord)
                return true;
            return false;
        }

        public void Deposit(decimal amount, string currency)
        {
            var playerCurrency = Account.Currency;
            var playerAmount = Account.Amount;
            if (playerCurrency == currency)
                _dictionary[Id].Account.Amount = playerAmount + amount;
            else
            {
                var converted = OutsideToInside(amount, currency);
                Account.Amount = converted + playerAmount;
            }
        }
        public void Withdraw(decimal amount, string currency)
        {
            var convertedCurrency = OutsideToInside(amount,currency);
            if((Account.Amount+0.00001m - convertedCurrency) <=0)
                throw new InvalidOperationException($"Not enough money on {Id} account!");
            Account.Amount -= convertedCurrency;
        }
        
        public decimal GetBalance(string сurrency)
        {
            var convertCurrency = Converter(Account.Amount,сurrency);
            return Math.Round(convertCurrency,2);
        }
        
        private decimal Converter(decimal amount,string currency)
        {
            if(currency != Account.Currency)
                switch (Account.Currency)
                {
                    case "EUR":
                        if (currency == "USD")
                            return Decimal.Multiply(amount,1.19m);
                        else if (currency == "UAH")
                            return Decimal.Multiply(amount, 33.63m);
                        break;
                    case "USD":
                        if (currency == "UAH")
                            return Decimal.Multiply(amount, 28.36m);
                        else if (currency == "EUR")
                            return Decimal.Divide(amount, 1.19m);
                        break;
                    case "UAH":
                        if (currency == "EUR")
                            return Decimal.Divide(amount, 33.63m);
                        else if (currency == "USD")
                            return Decimal.Divide(amount, 28.36m);
                        break;
                }

            return amount;
        }
        
        private decimal OutsideToInside(decimal outsideAmount,string outsideCurrency)
        {
            if(outsideCurrency != Account.Currency)
                switch (Account.Currency)
                {
                    case "EUR":
                        if (outsideCurrency == "USD")
                            return Decimal.Divide(outsideAmount,1.19m);
                        else if (outsideCurrency == "UAH")
                            return Decimal.Divide(outsideAmount, 33.63m);
                        break;
                    case "USD":
                        if (outsideCurrency == "UAH")
                            return Decimal.Divide(outsideAmount, 28.36m);
                        else if (outsideCurrency == "EUR")
                            return Decimal.Multiply(outsideAmount, 1.19m);
                        break;
                    case "UAH":
                        if (outsideCurrency == "EUR")
                            return Decimal.Multiply(outsideAmount, 33.63m);
                        else if (outsideCurrency == "USD")
                            return Decimal.Multiply(outsideAmount, 28.36m);
                        break;
                    default:
                        return 0;
                }
            return outsideAmount;
        }
    }
}