using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pServerError : Error
    {
        public P2pServerError(int code, string message, object data) : base(code, message, data)
        {
        }
    }
}
