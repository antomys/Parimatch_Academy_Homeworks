using Newtonsoft.Json;

namespace Task_3
{
    public class Result
    {
        protected internal Result(int success, int fail)
        {
            Success = success;
            Fail = fail;
        }
        [JsonProperty("successful")] protected int Success { get; set; }
        [JsonProperty("failed")] protected int Fail { get; set; }

    }
}