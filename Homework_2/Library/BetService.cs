using System;

namespace Library
{
    public class BetService
    {
        public decimal min = 1.01m;
        public decimal max = 25.00m;
        public decimal Odd { get; private set; }

        public BetService()
        {
            Odd = GenerateInRange();
        }

        private decimal GenerateInRange()
        {
            return Math.Round((decimal)new Random().NextDouble()*(max-min) + min,2);
        }

        public float GetOdds()
        {
            Odd = GenerateInRange();
            return (float)Odd;
        }

        public bool isWon()
        {
            var percentage = 100m / Odd;
            var prediction = new Random().Next(1,100);
            if (prediction >= percentage)
                return false;
            return true;
        }

        public decimal Bet(decimal amount)
        {
            var odd = GetOdds();
            if (isWon())
                return amount * (decimal)odd;
            return 0;
        }
        
        
        
    }
}