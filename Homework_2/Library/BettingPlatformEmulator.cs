using System;
using System.Collections.Generic;

namespace Library
{
    public class BettingPlatformEmulator
    {
        BetService betService = new BetService();
        
        PaymentService _paymentService = new PaymentService();
        private List<Player> Players { get; set; }
        private Player ActivePlayer { get; set; }
        private Account _Account { get; set; }
        

        public BettingPlatformEmulator()
        {
            _Account = new Account("USD");
            Players = new List<Player>();
        }

        public void Start()
        {
            
            var value = 0.0;
            if (ActivePlayer == null)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Stop");
                Double.TryParse(Console.In.ReadLine(),out value);
                switch (value)
                {
                    case 1: Register();
                        break;
                    case 2: Login();
                        break;
                    case 3: Exit();
                        break;
                    default:
                        Console.WriteLine("Wrong command.");
                        Start();
                        break;
                }
            }
            else
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("\t2.1 Deposit");
                Console.WriteLine("\t2.2 Withdraw");
                Console.WriteLine("\t2.3 GetOdds");
                Console.WriteLine("\t2.4 Bet");
                Console.WriteLine("\t2.5 Logout");
                Console.WriteLine("3. Stop");
                Double.TryParse(Console.In.ReadLine(),out value);
                switch (value)
                {
                    case 1: Register();
                        break;
                    case 2:
                        Console.WriteLine($"You are already logged as " +
                                          $"\"{ActivePlayer.Email}\"");
                        Start();
                        break;
                    case 3: Exit();
                        break;
                    case 2.1:
                        Deposit();
                        break;
                    case 2.2: Withdraw();
                        break;
                    case 2.3:
                        Console.WriteLine($"\t\nActual Coefficient is " +
                                          $"{betService.GetOdds()}");
                        Start();
                        break;
                    case 2.4:
                        Bet();
                        break;
                    case 2.5: Logout();
                        break;
                    default:
                        Console.WriteLine("Wrong command.");
                        Start();
                        break;
                }
            }
        }
        private void Exit()
        {
            Environment.Exit(1);
        }

        private void Bet()
        {
            try
            {
                int input;
                Console.WriteLine($"Current coefficient is {betService.Odd}");
                if (ActivePlayer.Account.Amount <= 0)
                {
                    Console.WriteLine("You are not able to play with 0 balance. Please deposit");
                    Deposit();
                }
                Console.WriteLine("Please Enter amount of bet");
                Int32.TryParse(Console.In.ReadLine(), out input);
                if (input <= 0 || input > ActivePlayer.Account.Amount)
                {
                    Console.WriteLine("Try again. Not enough or negative bet");
                    Bet();
                }
                ActivePlayer.Withdraw(input,ActivePlayer.Account.Currency);
                var result = betService.Bet(input);
                if(result<=0)
                    Console.WriteLine("You lost");
                else
                {
                    Console.WriteLine($"You won {result} {ActivePlayer.Account.Currency}");
                }
                ActivePlayer.Deposit(result,ActivePlayer.Account.Currency);
                Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }

        private void Register()
        {
            Console.WriteLine("Registration Form:\n");
            Console.WriteLine("Enter your name, please");
            var playerFirstName = Console.ReadLine();
            Console.WriteLine("Enter your Last name, please");
            var playerLastName = Console.ReadLine();
            Console.WriteLine("Enter your E-mail or login please");
            var email = Console.ReadLine();
            Console.WriteLine("Please enter password");
            var pass = Console.ReadLine();
            var currency = "";
            do
            {
                Console.WriteLine("Please enter your Currency");
                currency = Console.ReadLine()?.ToUpper();
            } while (currency != "USD" && currency != "EUR" && currency != "UAH");

            bool isCreated;
            do
            {

                try
                {
                    Player newPlayer = new Player(playerFirstName, playerLastName, email,
                        pass, currency);
                    Players.Add(newPlayer);
                    isCreated = true;
                }
                catch (Exception exception)
                {
                    isCreated = false;
                    Console.WriteLine(exception.Message);
                }

            } while (!isCreated);
            Console.WriteLine("Success");
            Start();
        }

        private void Login()
        {
            Console.WriteLine("Please Enter Login or E-mail");
            var login = Console.ReadLine();
            Console.WriteLine("Please enter password");
            var pass = Console.ReadLine();
            bool isLogined = false;
            
            foreach (var player in Players)
            {
                if (player.Email == login)
                    if (player.IsPasswordValid(pass))
                    {
                        Console.WriteLine("Login successful!");
                        ActivePlayer = player;
                        isLogined = true;
                        break;
                    }
            }

            if (!isLogined)
            {
                Console.WriteLine("Please try again");
                Login();
            }
            Start();
        }

        void Logout()
        {
            Console.WriteLine("Logging out...");
            ActivePlayer = null;
            Start();
        }

        private void Deposit()
        {
            try
            {
                Console.WriteLine("Please enter currency. Available: UAH,USD,EUR");
                var currency = Console.ReadLine().ToUpper();
                if (currency != "USD" && currency != "EUR" && currency != "UAH")
                {
                    Console.WriteLine("Try again.");
                    Deposit();
                }
                Console.WriteLine("Please enter amount");
                decimal amount = 0m;
                Decimal.TryParse(Console.ReadLine(), out amount);
                _paymentService.StartDeposit(amount,currency);
                ActivePlayer.Deposit(amount,currency);
                Console.WriteLine($"My balance: {ActivePlayer.Account.Amount} {ActivePlayer.Account.Currency}");
                _Account.Deposit(amount,currency);
                Console.WriteLine($"Platform balance: {_Account.Amount} {_Account.Currency}");
                Console.WriteLine("Success");
                Start();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Start();
            }
            
        }

        private void Withdraw()
        {
            try
            {
                Console.WriteLine("Withdraw Method"); //todo remove
                Console.WriteLine("Please enter currency. Available: UAH,USD,EUR");
                var currency = Console.ReadLine().ToUpper();
                if (currency != "USD" && currency != "EUR" && currency != "UAH")
                {
                    Console.WriteLine("Try again.");
                    Withdraw();
                }
                Console.WriteLine("Please enter amount");
                decimal amount = 0m;
                Decimal.TryParse(Console.ReadLine(), out amount);
                if (ActivePlayer.Account.Amount < amount)
                {
                    Console.WriteLine("There is insufficient funds on your account"); //todo exception
                    Start();
                } else if (_Account.Amount < amount)
                {
                    //todo exception
                    Console.WriteLine("There is some problem on the platform side. Please try it later"); //todo exception
                    Start();
                }
                else
                {
                    _paymentService.StartWithdraw(amount,currency);
                    ActivePlayer.Withdraw(amount,currency);
                    _Account.Withdraw(amount,currency);
                    Start();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
           
        }

    }
}