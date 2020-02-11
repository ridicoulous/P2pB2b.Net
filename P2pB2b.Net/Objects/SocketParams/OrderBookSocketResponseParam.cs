using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    [JsonConverter(typeof(ArrayConvert))]

    public class P2pOrderBookUpdate
    {
        public P2pOrderBookUpdate()
        {

        }
        [ArrayProperty(0)]
        public bool IsFull { get; set; }
        [ArrayProperty(1)]
        public P2pOrderBook OrderBook { get; set; } = new P2pOrderBook();
        [ArrayProperty(2)]
        public string Symbol { get; set; }

    }
}
