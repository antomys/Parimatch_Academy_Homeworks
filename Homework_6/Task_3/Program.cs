using System;

namespace Task_3
{
    internal static class Program
    {
        

        private static void Main()
        {
            
            Console.WriteLine("Volokhovych, service of unique logins");
            
            //Csv.FillCsvWithLoginAsync(); //Well there is async method that works like dog fart
            //To get NORMAL sync method, just uncomment below
            Csv.FillCsvWithLogin();
            
            int threadAmount;
            while (true)
            {
                Console.WriteLine("Please enter number of threads");
                if(!Int32.TryParse(Console.ReadLine(), out threadAmount) || threadAmount <= 0)
                {
                    PrintError();
                    continue;
                }
                break;
            }

            var loginClient = new LoginClient(threadAmount);
            loginClient.GetLoginsAsync();
            loginClient.GetResultAsync();
        }
        static void PrintError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Input. Try again!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
