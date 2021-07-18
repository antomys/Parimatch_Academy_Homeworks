using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Task_1.Services
{
    public record Result
    {
        //Got from PM_HW_9 rep
        
        /// <summary>
        /// boolean Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; init; }
        
        /// <summary>
        /// string Error
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; init; }
        
        /// <summary>
        /// string Duration
        /// </summary>
        [JsonPropertyName("duration")]
        public string Duration { get; init; }
        
        /// <summary>
        /// List of int Primes
        /// </summary>
        [JsonPropertyName("primes")]
        public List<int> Primes { get; init; }

        /*public override string ToString()
        {
            return $"Success: {Success}\n" +
                   $"Duration: {Duration}\n";
        }*/

        public Result(bool success, string error, string duration, List<int> primes)
            => (Success, Error,Duration,Primes) = 
            (success, error, duration, primes);
    }
}