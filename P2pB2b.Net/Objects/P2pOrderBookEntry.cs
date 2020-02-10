using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class P2pOrderBookEntry : ISymbolOrderBookEntry
    {
        [ArrayProperty(0)]
        public decimal Quantity { get; set; }
        [ArrayProperty(1)]
        public decimal Price { get; set; }

    }
}
