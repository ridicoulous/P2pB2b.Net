using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    public class P2pSymbolOrderBookOptions : OrderBookOptions
    {
        public readonly int Limit;
       

        public P2pSymbolOrderBookOptions(string name, bool sequencesAreConsecutive = false, int limit = 20) : base(name, sequencesAreConsecutive)
        {
            Limit = limit;         
        }
    }
}
