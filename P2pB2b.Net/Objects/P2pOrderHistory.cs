using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pOrderHistory
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        /// <summary>
        /// Order amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Order price.
        /// </summary>

        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("side")]
        public P2pOrderSide Side { get; set; }

        [JsonProperty("ctime"), JsonConverter(typeof(P2pTimestampConverter))]
        public DateTime Ctime { get; set; }

        [JsonProperty("takerFee")]
        public string TakerFee { get; set; }

        [JsonProperty("ftime"), JsonConverter(typeof(P2pTimestampConverter))]
        public DateTime Ftime { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("makerFee")]
        public decimal MakerFee { get; set; }

        [JsonProperty("dealFee")]
        public decimal DealFee { get; set; }

        [JsonProperty("dealStock")]

        public decimal DealStock { get; set; }

        [JsonProperty("dealMoney")]
        public decimal DealMoney { get; set; }
    }
}
