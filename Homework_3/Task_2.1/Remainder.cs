namespace Task_2._1
{
    public class Remainder
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public int Balance { get; set; }

        public Remainder(string id, string location, int balance)
        {
            Id = id;
            Location = location;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"[{Id}, {Location}, {Balance}]";
        }
    }
}