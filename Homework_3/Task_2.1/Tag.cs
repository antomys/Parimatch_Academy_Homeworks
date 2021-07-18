namespace Task_2._1
{
    public class Tag
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public Tag(string id, string value)
        {
            Id = id;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Id},{Value}]";
        }
    }
}