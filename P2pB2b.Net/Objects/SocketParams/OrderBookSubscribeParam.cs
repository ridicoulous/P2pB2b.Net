using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    [JsonConverter(typeof(ArrayConverter))]
    public class OrderBookSubscribeParam
    {
        public OrderBookSubscribeParam()
        {

        }
        public OrderBookSubscribeParam(string symbol, int limit=20, string  xz="0")
        {
            Symbol = symbol;
            Limit = limit;
            Xz = xz;
        }
        [ArrayProperty(0)]
        public string Symbol { get; set; }
        [ArrayProperty(1)]
        public int Limit { get; set; }
        [ArrayProperty(2)]
        public string Xz { get; set; }

        ///"ETH_BTC", 20, "0"
    }
}
