using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace Task_2
{
    internal static class Program
    {
        private static void Main()
        {
            var time = DateTime.Now;
            var settings = LoadDataFromJson(time);
            if (settings == null)
            {
                return;
            }
            PrimeThreads(settings, time);
        }

        private static List<Settings> LoadDataFromJson(DateTime time)
        {
            try
            {
                var jsonString = File.ReadAllText(@"settings.json");
                var deserializer = JsonConvert.DeserializeObject<List<Settings>>(jsonString);
                if (deserializer == null )
                {
                    throw new Exception();
                }

                if (deserializer
                    .Any(setting => 
                        setting.PrimesFrom <= 0 || setting.PrimesTo <= 0
                        || setting.PrimesFrom > setting.PrimesTo))
                {
                    throw new Exception();
                }
                return deserializer;
            }
            catch
            {
                var endTime = DateTime.Now;
                var duration = (endTime - time);
                const string error = "settings.json are missing or corrupted";
                SaveResultInJson(false, error, duration, null);
                return null;
            }
        }

        private static void SaveResultInJson(bool success, string error, 
            TimeSpan duration, List<int> primes)
        {
            var result = new Result
            {
                Success = success, Error = error, Duration = duration.ToString(), Primes = primes
            };
            //var serialized = JsonSerializer.Serialize(result);
            var serialized = JsonConvert.SerializeObject(result);
            File.WriteAllText(@"result.json", serialized);
        }

        private static void PrimeThreads(List<Settings> settings, DateTime time)
        {
            
            var values = new SynchronizedList<int>();
            var result = new List<int>();
            foreach (var t in settings)
            {
                var thread = new Thread(() =>
                {
                    values = PrimeAlgorithm(t);
                    result.AddRange(values.Clone());
                });
                thread.Start();
                thread.Join();
            }
            var primes = result.Distinct().ToList();
            var duration = DateTime.Now.Subtract(time);
            SaveResultInJson(true, null, duration, primes);
        }

        private static SynchronizedList<int> PrimeAlgorithm(object setting)
        {
            var settings = (Settings) setting;
            var primes = new SynchronizedList<int>();
            try
            {
                if(settings == null)
                {
                    throw new Exception();
                }
                for (var number = settings.PrimesFrom; number < settings.PrimesTo; number++)
                {
                    var counter = 0;
                    for (var i = 2; i <= number / 2; i++)
                    {
                        if (number % i != 0) continue;
                        counter++;
                        break;
                    }

                    if (counter == 0 && number != 1)
                        primes.Add(number);
                }
                return primes;
            }
            catch
            {
                return null;
            }
        }
    }
}