using Newtonsoft.Json;

namespace Task_2
{
    internal class Settings
    {
        [JsonProperty("primesFrom")]
        public int PrimesFrom { get; set; }

        [JsonProperty("primesTo")]
        public int PrimesTo { get; set; }

    }
}