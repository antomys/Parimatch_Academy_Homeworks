namespace PM_HW_7.Models.Impl
{
    using System.Text.Json.Serialization;
    
    internal class Response : IResponse
    {
        public Response(bool handled, int code, string content)
        {
            Handled = handled;
            Code = code;
            Content = content;
        }
        
        [JsonPropertyName("handled")]
        public bool Handled { get; set; }
        
        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        [JsonPropertyName("content")]
        public string Content { get; set; }

        public override string ToString()
        {
            return $"Status:{Handled}; Accepted code: {Code}; Content:\n{Content}";
        }
    }
}