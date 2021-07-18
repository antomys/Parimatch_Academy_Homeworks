using System;
using System.Net.Http;
using System.Threading.Tasks;
using Task_2.Models;

namespace Task_2
{
    internal static class Program
    {
        private static readonly HttpClient Client = new();

        private static async Task Main()
        {
            Client.BaseAddress = new Uri("http://localhost:5000/");
            var input = new Input();

            Console.WriteLine("*********************************************");
            await Input.TestLandingPage(Client);
            Console.WriteLine("*********************************************");
            await Input.TestIsPrime(Client);
            Console.WriteLine("*********************************************");
            await input.TestGetPrimes(Client);
        }
    }
}
