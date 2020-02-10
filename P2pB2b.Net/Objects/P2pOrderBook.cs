using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pOrderBook
    {
        [JsonProperty("asks")]
        public List<P2pOrderBookEntry> Asks { get; set; }
        [JsonProperty("asks")]
        public List<P2pOrderBookEntry> Bids { get; set; }

    }
}
