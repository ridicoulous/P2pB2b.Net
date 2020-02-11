using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    [JsonConverter(typeof(ArrayConvert))]
    public class P2pOrderBookEntry : ISymbolOrderBookEntry
    {      
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

    }
}
