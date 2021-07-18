using System;

namespace Task_1._3
{
    public class Region:IRegion
    {
        public string Brand { get; }
        public string Country { get; }

        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Region);
        }

        private bool Equals(Region region)
        {
            Region other = this;
            return (region.Brand == other.Brand) && (region.Country == other.Country);
        }

        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Country.GetHashCode();
        }
    }
}