using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net
{
    public class P2pSocketClientOptions : SocketClientOptions
    {
        public P2pSocketClientOptions(string baseAddress= "wss://p2pb2b.io/trade_ws") : base(baseAddress)
        {
            this.AutoReconnect = true;
            this.ReconnectInterval = TimeSpan.FromSeconds(2);
            this.SocketNoDataTimeout = TimeSpan.FromSeconds(5);            
        }
    }
}
