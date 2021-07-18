using System;
using System.Collections.Generic;
using Library.Exceptions;

namespace Library
{
    internal class AccountDetails
    {
        public decimal Amount;
        public string Currency;
    }
    
    public sealed class Account
    {
        private readonly Dictionary<int, AccountDetails> _dictionary;

        private List<int> randomList = new List<int>();
        
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
        

        public Account(string сurrency)
        {
            Id = GenerateUniqueRandom(Id);
            try
            {
                if (сurrency == "EUR" || сurrency == "USD" || сurrency == "UAH")
                    Currency = сurrency;
                else
                {
                    throw new NotSupportedException(nameof(сurrency));
                }
            }
            catch
            {
                Console.WriteLine($"Exception in constructor of Class_Amount. Not supported currency");
            }
            Amount = 0;
            _dictionary=new Dictionary<int, AccountDetails> { { Id, new AccountDetails { Amount = Amount, Currency = Currency } } };
        }

        public int Id { get; set; }
        public string Currency { get;}

        public decimal Amount { get; set; }

        public void Deposit(decimal amount, string сurrency)
        {
            if(сurrency != Currency)
                switch (сurrency)
                {
                    case "EUR":
                        if (Currency == "USD")
                            amount = Decimal.Multiply(amount,1.19m);
                        else if (Currency == "UAH")
                            amount = Decimal.Multiply(amount, 33.63m);
                        break;
                    case "USD":
                        if (Currency == "UAH")
                            amount = Decimal.Multiply(amount, 28.36m);
                        else if (Currency == "EUR")
                            amount = Decimal.Divide(amount, 1.19m);
                        break;
                    case "UAH":
                        if (Currency == "EUR")
                            amount = Decimal.Divide(amount, 33.63m);
                        else if (Currency == "USD")
                            amount = Decimal.Divide(amount, 28.36m);
                        break;
                }
            _dictionary[Id].Amount += amount;
            Amount =Math.Round(Amount + amount, 2);
        }

        public void Withdraw(decimal amount, string сurrency)
        {
            var convertedCurrency = OutsideToInside(amount,сurrency);
            if(_dictionary[Id].Amount - convertedCurrency <=0)
                throw new InvalidOperationException($"Not enough money on {_dictionary[Id]} account!");
            _dictionary[Id].Amount = _dictionary[Id].Amount - convertedCurrency;
            Amount = Amount - convertedCurrency;
        }

        public decimal GetBalance(string сurrency)
        {
            var convertCurrency = Converter(_dictionary[Id].Amount,сurrency);
            return convertCurrency;
        }
        private decimal OutsideToInside(decimal outsideAmount,string outsideCurrency)
        {
            if(outsideCurrency != _dictionary[Id].Currency)
                switch (_dictionary[Id].Currency)
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

        private decimal Converter(decimal amount,string currency)
        {
            if(currency != Currency)
                switch (Currency)
                {
                    case "EUR":
                        if (currency == "USD")
                            return Math.Round(Decimal.Multiply(amount,1.19m));
                        else if (currency == "UAH")
                            return Math.Round(Decimal.Multiply(amount, 33.63m));
                        break;
                    case "USD":
                        if (currency == "UAH")
                            return Math.Round(Decimal.Multiply(amount, 28.36m));
                        else if (currency == "EUR")
                            return Decimal.Divide(amount, 1.19m);
                        break;
                    case "UAH":
                        if (currency == "EUR")
                            return Math.Round(Decimal.Divide(amount, 33.63m));
                        else if (currency == "USD")
                            return Math.Round(Decimal.Divide(amount, 28.36m));
                        break;
                }

            return amount;
        }
    }
    public class AccountManager
    {
        private readonly Account[] _accounts;

        public AccountManager(Account[] accounts)
        {
            _accounts = accounts;
        }
        public Account[] GetSortedAccounts()
        {
            Account[] arr = _accounts;
            int n = arr.Length; 
            for (int i = 0; i < n - 1; i++) 
            for (int j = 0; j < n - i - 1; j++) 
                if (arr[j].Id > arr[j + 1].Id) 
                { 
                    // swap temp and arr[i] 
                    int temp = arr[j].Id; 
                    arr[j].Id = arr[j + 1].Id; 
                    arr[j + 1].Id = temp; 
                }

            return arr;
        }

        public Account[] GetSortedAccountsByQuickSort()
        {
            Account[] arr = _accounts;
            int low = 0;
            int high = _accounts.Length - 1;
            quickSort(arr, low, high);
            return arr;
        }

        static void quickSort(Account[] arr, int low, int high) 
        { 
            if (low < high) 
            {
                int pi = partition(arr, low, high);
                quickSort(arr, low, pi-1); 
                quickSort(arr, pi+1, high); 
            } 
        } 
        static int partition(Account[] arr, int low, int high)
        {
            int pivot = arr[high].Id;  
            
            int i = (low - 1);  
            for (int j = low; j < high; j++) 
            {
                if (arr[j].Id < pivot) 
                { 
                    i++; 
                    
                    int temp = arr[i].Id; 
                    arr[i].Id = arr[j].Id; 
                    arr[j].Id = temp; 
                } 
            } 
            
            int temp1 = arr[i+1].Id; 
            arr[i+1].Id = arr[high].Id; 
            arr[high].Id = temp1; 
  
            return i+1; 
        }

        public void GetAccount(int key)
        {
            var tries = 0;
            Account[] accounts = _accounts;
            int left = 0;
            int right = accounts.Length - 1;

            while (left <= right)
            {
                tries++;
                var middle = (left + right) / 2;

                if (accounts[middle].Id == key)
                {
                    Console.WriteLine($"{key} was found at index {middle} by {tries} tries");
                }
                if (key < accounts[middle].Id)
                    right = middle - 1;
                else
                {
                    left = middle + 1;
                }
            }
        }
    }
}