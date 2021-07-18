using System.Collections.Generic;

namespace Task_2._1
{
    public class Product
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }

        public Product(string id, string brand, string model, int price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public override string ToString()
        {
            return $"#{Id} {Brand} - {Model} - ${Price}";
        }
    }
    
}