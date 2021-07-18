using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Task_2.Models
{
    public class InputModel
    {
        [JsonProperty("landingtest")]
        public static Uri LandingTest { get; set; }
        
        [JsonProperty("isprime")]
        public static Dictionary<string,HttpStatusCode> IsPrime { get; set; }
        
        [JsonProperty("getprimes")]
        public static Dictionary<string,List<int>> GetPrimes { get; set; }
        
        public static InputModel Construct()
        {
            var deserialized = JsonConvert.DeserializeObject<InputModel>(File.ReadAllText("connectionUrl.json"));
 
            return deserialized;
        }
    }
}