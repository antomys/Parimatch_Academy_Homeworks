using System;
using Library;

namespace Tast_1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Account[] accounts = new Account[1000000];
            for (var i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new Account("UAH");
            }
            var accManager = new AccountManager(accounts);
            
            accManager.GetSortedAccountsByQuickSort();
            
            
            Console.WriteLine("First ten accounts are: \n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ID:{accounts[i].Id}");
            }
            
            Console.WriteLine("Last ten accounts are: \n");
            for (int i = accounts.Length-10; i < accounts.Length; i++)
            {
                Console.WriteLine($"ID:{accounts[i].Id}");
            }
        }
    }
}