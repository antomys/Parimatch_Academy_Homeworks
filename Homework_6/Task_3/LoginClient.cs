using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task_3
{
    public class LoginClient
    {
        private readonly Random _random = new();
        private readonly ConcurrentQueue<LoginCredentials> _loginCredentials = new();
        private int _loginSuccesses;
        private int _loginFails;

        protected internal LoginClient(int threadAmount)
        {
            ThreadAmount = threadAmount;
            ThreadPool.QueueUserWorkItem(CheckLogin);
        }
        private int ThreadAmount { get; }
        
        private string Login(string login, string password)
        {
            var randomized = _random.NextDouble();
            Thread.Sleep((int)_random.NextDouble()*1000);
            return randomized <= 0.5 ? new string(Csv.GenerateRandomString()) : null;
        }
        
        private void CheckLogin(object obj)
        {
            while(_loginCredentials.TryDequeue(out var credentials))
                if (string.IsNullOrEmpty(Login(credentials.GetLogin(), credentials.GetPassword())))
                {
                    Interlocked.Increment(ref _loginFails);
                }
                else
                {
                    Interlocked.Increment(ref _loginSuccesses);
                }

            //_countdownEvent.Signal();
        }
        
        public void GetLoginsAsync()
        {
            GetCredentialsPair().Wait();
        }
        
        public void GetResultAsync()
        {
            var threadAmount = ThreadAmount;
            var threads = new Thread[threadAmount];
            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(CheckLogin);
                threads[i].Start();//todo
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            SaveResultToJson(_loginSuccesses,_loginFails);
        }
        private async Task GetCredentialsPair()
        {
            const string fileName = @"logins.csv";

            using var reader = new StreamReader(fileName);
            while (!reader.EndOfStream)
            {
                var firstLineOfFile = 
                    await reader.ReadLineAsync().ConfigureAwait(false);
                var values = firstLineOfFile?.Split(',');
                _loginCredentials.Enqueue(new LoginCredentials(values?[0],values?[1]));
            }
        }

        private static void SaveResultToJson(int loginSuccesses, int loginFails)
        {
            var serialize = JsonConvert.SerializeObject(new Result(loginSuccesses,loginFails));
            File.WriteAllText("result.json",serialize);
        }
    }
}