using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net
{
    public class P2pAuthenticationProvider : AuthenticationProvider
    {
        public P2pAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }
    }
}
