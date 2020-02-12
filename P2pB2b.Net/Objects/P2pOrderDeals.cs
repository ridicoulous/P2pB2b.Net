using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pOrderDeals
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("records")]
        public List<P2pOrderDeal> Records { get; set; }
    }
    public class P2pOrderDeal:P2pDeal
    {     

        [JsonProperty("fee")]        
        public decimal Fee { get; set; }
        [JsonProperty("dealOrderId")]
        public long DealOrderId { get; set; }

        [JsonProperty("role")]
        public long Role { get; set; }

        [JsonProperty("deal")]
        public decimal Deal { get; set; }
    }
    public class P2pDeal
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("time"), JsonConverter(typeof(P2pTimestampConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonIgnore]
        public string Symbol { get; set; }
    }
}
