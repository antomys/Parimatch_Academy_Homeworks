using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task_2._1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ERP Reports Bot, Volokhovych");
            Help();
            var bot = new Bot("Products.csv","Tags.csv","Inventory.csv");
            bot.Start();
        }

        static void Help()
        {
            Console.WriteLine("This is ERP bot.\n It helps you to find your data");
        }
    }

    internal class Bot
    {
        public Bot(string prodfilename, string tagsfilename, string remaindersfilename)
        {
            try
            {
                Products = ReadProducts(prodfilename);
                Tags = ReadTags(tagsfilename);
                Remainders = ReadRemainders(remaindersfilename);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(-1);
            }
            
        }

        private IList<Product> Products { get; set; }
        private IList<Tag> Tags { get; set; }
        private IList<Remainder> Remainders { get; set; }

        public void Start()
        {
            try
            {
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Goods");
                Console.WriteLine("3. Remainders");
                Int32.TryParse(Console.ReadLine(), out int value);
                switch (value)
                {
                    case 1:
                        Environment.Exit(0);
                        break;
                    case 2:
                        GoodsMenu();
                        break;
                    case 3:
                        RemainderMenu();
                        break;
                    default:
                        throw new Exception("Invalid input. Try again");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Start();
            }
        }

        void GoodsMenu()
        {
            try
            {
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Goods");
                Console.WriteLine("\t a. Back to main menu");
                Console.WriteLine("\t b. Good search");
                Console.WriteLine("\t c. Show all goods in ascending order");
                Console.WriteLine("\t d. Show all goods in descending order");
                Console.WriteLine("3. Remainders");
                var value = Console.In.ReadLine();
                switch (value)
                {
                    case "1":
                        Environment.Exit(0);
                        break;
                    case "2":
                        GoodsMenu();
                        break;
                    case "a":
                        Start();
                        break;
                    case "b":
                        GoodSearch();
                        break;
                    case "c":
                        SortGoodsAscending();
                        break;
                    case "d":
                        SortGoodsDescending();
                        break;
                    case "3":
                        RemainderMenu();
                        break;
                    default:
                        throw new Exception("Invalid input. Try again");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                GoodsMenu();
            }
        }

        private void GoodSearch()
        {
            Console.Write("Please input to search: ");
            var input = Console.In.ReadLine()?.ToLower();
            var query = Products
                .Where(x => x.Id.ToLower() == input).Distinct();
            Console.WriteLine("By Id:");
            if(query.Count()==0)
                Console.WriteLine("Not found");
            else
            {
                queryprint(query);
            }
            Console.WriteLine("By Model and(or) Brand");
            query = Products.Where(x =>
                x.Model.ToLower() == input || x.Brand.ToLower() == input).Distinct();
            if(query.Count()==0)
                Console.WriteLine("Not found");
            else
            {
                queryprint(query);
            }

            Console.WriteLine("By Tags");
            var id = Tags.Where(x => x.Value.ToLower() == input).Select(p => p.Id.ToString()).ToList();
            if(id.Count==0)
                Console.WriteLine("Not Found\n");
            else
            {
                foreach (var VARIABLE in id)
                {
                    query = Products.Where(x => x.Id == VARIABLE);
                    queryprint(query);
                }
            }
            Start();
        }

        private void queryprint(IEnumerable<Product> Qq)
        {
            foreach (var VARIABLE in Qq)
            {
                var query = Tags.Where(p =>
                        VARIABLE.Id.Contains(p.Id)).Select(p=>p.Value.ToString())
                    .Aggregate((current, next) 
                        => current + ", " + next);
                Console.WriteLine(VARIABLE.ToString() +$" [{query}]");
            }
        }

        void RemainderMenu() //done
        {
            try
            {
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Goods");
                Console.WriteLine("3. Remainders");
                Console.WriteLine("\t a. Back to main menu");
                Console.WriteLine("\t b. Missing goods");
                Console.WriteLine("\t c. Remaining Ascending");
                Console.WriteLine("\t d. Remaining Descending");
                Console.WriteLine("\t e. Remaining by ID");
                var value = Console.In.ReadLine();
                switch (value)
                {
                    case "1":
                        Environment.Exit(0);
                        break;
                    case "2":
                        GoodsMenu();
                        break;
                    case "a":
                        Start();
                        break;
                    case "b":
                        NotListedGoods();
                        break;
                    case "c":
                        SortRemaindersAscending();
                        break;
                    case "d":
                        SortRemaindersDescending();
                        break;
                    case "e":
                        RemainderById();
                        break;
                    case "3":
                        RemainderMenu();
                        break;
                    default:
                        throw new Exception("Invalid input. Try again");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                RemainderMenu();
            }
        }
        private static IList<Product> ReadProducts(string filename)
        {
            var query = File.ReadAllLines(filename).Skip(1).Select(x => x.Split(';'))
                .Select(x => 
                    new Product(x[0], x[1], x[2], Int32.Parse(x[3])));
            return query.ToList();
        }

        private static IList<Tag> ReadTags(string filename)
        {
            var query = File.ReadAllLines(filename).Skip(1).Select(x => x.Split(';'))
                .Select(x => 
                    new Tag(x[0], x[1]));
            return query.ToList();
        }
        
        private static IList<Remainder> ReadRemainders(string filename)
        {
            var query = File.ReadAllLines(filename).Skip(1).Select(x => x.Split(';'))
                .Select(x => 
                    new Remainder(x[0], x[1],Int32.Parse(x[2])));
            return query.ToList();
        }

        private void SortGoodsAscending()
        {
            var query =
                from pr in Products
                orderby pr.Price
                select pr;
            Products = query.ToList();
            PrintProducts();
            GoodsMenu();
        }
        
        private void SortRemaindersAscending()
        {
            var query =
                from pr in Remainders
                orderby pr.Balance
                select pr;
            Remainders = query.ToList();
            PrintRemainders();
            RemainderMenu();
        }

        private void NotListedGoods()
        {
            var id = Remainders.Where(x1 => x1.Balance.Equals(0)).Select(x => x.Id).Distinct().ToList();
            var query1 = Products.Where(x =>
                Remainders.All(x1 => x1.Id != x.Id )).ToList();

            foreach (var VARIABLE in id)
            {
                var query = Products.Where(x => x.Id.Equals(VARIABLE)).ToList();
                query1 = query1.Concat(query)
                    .ToList();
            }

            var result = from t in query1
                orderby t.Id
                select t;
            foreach (var VARIABLE in result)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
            Start();
        }

        private void RemainderById()
        {
            try
            {
                Console.Write("Please input ID: ");
                var input = Console.In.ReadLine();
                if (string.IsNullOrEmpty(input))
                    throw new Exception("Input is null or empty!");
                var query =
                    from x in Remainders
                        where x.Id == input
                        select x;
                var remainders = query.ToList();
                if (!remainders.Any())
                    throw new Exception("There is no such Product");
                query = from x in remainders
                    orderby x.Id descending
                    select x;
                foreach (var VARIABLE in query)
                {
                    Console.WriteLine($"{VARIABLE.Location}, {VARIABLE.Balance}");
                }
                Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                RemainderMenu();
            }
            
        }
        
        private void SortRemaindersDescending()
        {
            var query =
                from pr in Remainders
                orderby pr.Balance descending 
                select pr;
            Remainders = query.ToList();
            PrintRemainders();
            RemainderMenu();
        }

        private void SortGoodsDescending()
        {
            var query =
                from pr in Products
                orderby pr.Price descending
                select pr;
            Products = query.ToList();
            PrintProducts();
            GoodsMenu();
        }
        private void PrintProducts()
        {
            foreach (var VARIABLE in Products)
            {
                var query = Tags.Where(p =>
                    VARIABLE.Id.Contains(p.Id)).Select(p=>p.Value.ToString())
                    .Aggregate((current, next) 
                        => current + ", " + next);
                Console.WriteLine(VARIABLE.ToString() +$" [{query}]");
            }
            
        }
        private void PrintTags()
        {
            foreach (var VARIABLE in Tags)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
        }
        private void PrintRemainders()
        {
            foreach (var VARIABLE in Remainders)
            {
                Console.WriteLine(VARIABLE.ToString());
            }
        }
    }
}