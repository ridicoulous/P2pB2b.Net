using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiter;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net
{
    public class P2pClientOptions : RestClientOptions
    {     

        public P2pClientOptions(string baseAddress = "https://api.p2pb2b.io/api") : base(baseAddress)
        {            
            RateLimiters.Add(new RateLimiterTotal(5, TimeSpan.FromSeconds(1)));
            RateLimiters.Add(new RateLimiterTotal(100, TimeSpan.FromMinutes(1)));
            RateLimiters.Add(new RateLimiterPerEndpoint(10, TimeSpan.FromSeconds(1)));
        }
    }
}
