using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Three sortings and one comparator, Volokhovych");
            Players players = new Players
            {
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(59, "Ivan", "Ivanov", PlayerRank.General),
                new Player(52, "Ivan", "Snezko", PlayerRank.Lieutenant),
                new Player(34, "Alex", "Zeshko", PlayerRank.Colonel),
                new Player(34, "Alevtina", "Alyoshko", PlayerRank.Colonel),
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(34, "Vasiliy", "Sokol", PlayerRank.Major),
                new Player(31, "Alex", "Alexeenko", PlayerRank.Major)
            };
            SortByNames(players);
            SortByAge(players);
            SortByRank(players);

        }

        static void SortByNames(Players players)
        {
            Console.WriteLine("\nSorted by LastName + FirstName");
            players.Sort(new Player.Sorting());
            var unique = players
                .Distinct(Player.Comparer)
                .ToList();
            foreach (var VARIABLE in unique)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
        }
        static void SortByAge(Players players)
        {
            Console.WriteLine("\nSorted by Age");
            players.Sort(new Player.SortByAge());
            var unique = players
                .Distinct(Player.Comparer)
                .ToList();
            foreach (var VARIABLE in unique)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
        }
        static void SortByRank(Players players)
        {
            Console.WriteLine("\nSorted by Rank");
            players.Sort(new Player.SortByRank());
            var unique = players
                .Distinct(Player.Comparer)
                .ToList();
            foreach (var VARIABLE in unique)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
        }
    }
}