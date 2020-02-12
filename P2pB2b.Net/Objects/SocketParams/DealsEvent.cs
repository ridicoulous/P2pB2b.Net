using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    [JsonConverter(typeof(ArrayConvert))]

    public class DealsEvent
    {
        public DealsEvent()
        {

        }
        [ArrayProperty(0)]
        public string Symbol { get; set; }
        [ArrayProperty(1)]
        public List<P2pDeal> Deals { get; set; } = new List<P2pDeal>();      

    }
}
