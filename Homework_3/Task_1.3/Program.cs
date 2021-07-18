using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Task_1._3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Dictionary with Key");
            Console.WriteLine("Please enter amount of elements in Dictionary:");
            Console.Write("N: ");
            Int32.TryParse(Console.ReadLine(), out int elementsDict);
            Task task = new Task(elementsDict);
            task.Start();
        }
        
    }

    internal class Task
    {
        public Task(int elements)
        {
            Dictionary = new Dictionary<IRegion, IRegionSettings>(ElementsInDictionary);
            ElementsInDictionary = elements;
        }
        
        private Dictionary<IRegion,IRegionSettings> Dictionary { get; set; }
        private int ElementsInDictionary { get; set; }

        public bool Add()
        {
            var brand = "";
            var country = "";
            var website = "";
            try
            {
                Console.WriteLine("Please enter Brand Name:");
                brand = Console.ReadLine();
                if(brand?.ToLower() == "exit")
                    Environment.Exit(0);
                Console.WriteLine("Please enter Country:");
                country = Console.ReadLine();
                if(country?.ToLower() == "exit")
                    Environment.Exit(0);
                Console.WriteLine("Please enter Website:");
                website = Console.ReadLine();
                if(website?.ToLower() == "exit")
                    Environment.Exit(0);
                foreach (var VARIABLE in Dictionary)
                {
                    if (VARIABLE.Key.Brand.Equals(brand) &&
                        VARIABLE.Key.Country.Equals(country))
                    {
                        throw new Exception("\nThis value is already in Dictionary!\n");
                    }
                }

                if (Dictionary.Count >= ElementsInDictionary)
                {
                    throw new Exception("Exceeded amount of elements in Dictionary!");
                }

                Console.WriteLine("Success. Returning to Main menu");
                Dictionary.Add(new Region(brand, country),
                    new RegionSettings(website));
                Start();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Add();
            }
            return true;
        }
        
         public void Start()
        {
            try
            {
                Console.WriteLine("1. Add new in Dictionary");
                Console.WriteLine("2. Show all from Dictionary");
                Console.WriteLine("3. Help");
                Console.WriteLine("exit: exits the program");
                var input = Console.In.ReadLine();
                while (input?.ToLower() !="exit")
                {
                    switch (input?.ToLower())
                    {
                        case "1":
                            if(Add());
                            break;
                        case "2":
                            Show();
                            break;
                        case "3":
                            Help();
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            throw new Exception("\nInvalid input. Try again\n");
                    }
                }
                
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Start();
            }
        }

         private void Help()
        {
            Console.WriteLine("\n\nThis simple program adds data to dictionary. Start method has options to choose.\n Command Exit is immutable to register and works everywhere.\n\n");
            Start();
            
        }

        private void Show()
        {
            foreach (var VARIABLE in Dictionary)
            {
                Console.WriteLine($"[{VARIABLE.Key.Brand}, " +
                                  $"{VARIABLE.Key.Country}] = " +
                                  $"[{VARIABLE.Value.WebSite}]");
            }
            Start();
        }
    }
}