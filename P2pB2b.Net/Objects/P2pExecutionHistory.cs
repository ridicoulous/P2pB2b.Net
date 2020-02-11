using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pExecutionHistory
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("time"),JsonConverter(typeof(P2pTimestampConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("role")]
        public long Role { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("deal")]
        public decimal Deal { get; set; }
        [JsonProperty("fee")]  
        public decimal Fee { get; set; }
    }
}
