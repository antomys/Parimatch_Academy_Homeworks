using System;
using Library;

namespace Task_1._3
{
    class Program
    {
        static void Main()
        {
            Account[] accounts = new Account[1000000];
            for (var i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new Account("UAH");
            }
            var accManager = new AccountManager(accounts);
            
            accManager.GetSortedAccounts();
            
            accManager.GetAccount(111178);
        }
    }
}