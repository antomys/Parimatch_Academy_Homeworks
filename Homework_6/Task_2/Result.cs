using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task_2
{
    internal class Result
    {
        [JsonProperty("success")]
        public bool Success { get; init; }
        [JsonProperty("error")]
        public string Error { get; init; }
        [JsonProperty("duration")]
        public string Duration { get; init; }
        [JsonProperty("primes")]
        public List<int> Primes { get; init; }
    }
}