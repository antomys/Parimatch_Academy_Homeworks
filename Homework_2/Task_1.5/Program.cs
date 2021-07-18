using System;
using Library;

namespace Task_1._5
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "John Doe";
            var lastname = "Betman";
            var fakepass = "sadasfsd";
            var origpass = "TheP@$$w0rd";
            var login = "john777@gmail.com";
            Console.WriteLine("\n1.\n");
            Player player = new Player(name,lastname,login,origpass,"USD");

            
            Console.WriteLine("\n2.\n");
            Console.WriteLine($"Login with login {login} and password {fakepass} " +
                              $"is {player.IsPasswordValid(fakepass)}");
            Console.WriteLine("\n3.\n");
            Console.WriteLine($"Login with login {login} and password {fakepass} " +
                              $"is {player.IsPasswordValid("TheP@$$w0rd")}");
            Console.WriteLine("\n4.\n");
            player.Deposit(100,"USD");
            Console.WriteLine(player.GetBalance("USD"));
            Console.WriteLine("\n5.\n");
            player.Withdraw(50,"EUR");
            Console.WriteLine(player.GetBalance("USD"));
            Console.WriteLine("\n6.\n");
            try
            {
                player.Withdraw(1000,"USD");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("\n7.\n");
            try
            {
                var plnplayer = new Player(name,lastname,login,origpass,"PLN");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            
            


        }
    }
}