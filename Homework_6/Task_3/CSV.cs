using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public static class Csv
    {
        private static readonly Random Random = new Random();
        public static void FillCsvWithLogin()
        {
            var csv = new StringBuilder();
            for (var i = 0; i < 1000; i++)
            {
                var login = Guid.NewGuid();
                var password = GenerateRandomString();
                var newLine = $"{login},{password}\n";
                csv.Append(newLine); 
                File.WriteAllText(@"logins.csv",csv.ToString());
            }
        }

        public static Task FillCsvWithLoginAsync()
        {
            return Task.WhenAll(Enumerable.Range(0, 100)
                .Select(FillCsv)); //todo: Now i really do not know how to fix this crap
        }

        private static async Task FillCsv(int x)
        {
            if (File.Exists(@"logins.csv"))
            {
                File.Delete(@"logins.csv");
            }
            for (var i = 0; i < x; i++)
            {
                var newLine = new StringBuilder($"{Guid.NewGuid()},{GenerateRandomString()}\n");
                await File.AppendAllTextAsync(@"logins.csv",newLine.ToString()); //todo: Now i really do not know how to fix this crap
            }
        }

        public static string GenerateRandomString()
        {
            var length = Random.Next(7,30);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[Random.Next(x.Length)]).ToArray());
        }
    }
}