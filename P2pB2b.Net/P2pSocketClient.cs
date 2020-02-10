using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P2pB2b.Net.Interfaces;
using P2pB2b.Net.Objects;
using P2pB2b.Net.Objects.SocketParams;
using System;
using System.Threading.Tasks;

namespace P2pB2b.Net
{
    public class P2pSocketClient : SocketClient, IP2pSocketClient
    {
        private int counter = 1;
        public P2pSocketClient():base(new P2pSocketClientOptions(), null)
        {

        }
        public P2pSocketClient(P2pSocketClientOptions exchangeOptions, P2pAuthenticationProvider authenticationProvider) : base(exchangeOptions, authenticationProvider)
        {
        }
        public  async Task<CallResult<UpdateSubscription>> Test<T>(string pair, Action<T> onData)
        {
            // var tre = new P2pSocketSubscribeRequest() { Id = 1, Method = "depth.subscribe", Params = new System.Collections.Generic.List<object>() {"ETH_BTC", 20, "0" } };
            var req = SocketRequestFactory.Create(new OrderBookSubscribeParam(pair), "depth.subscribe");
            Console.WriteLine(JsonConvert.SerializeObject(req)); 
            return await Subscribe(JsonConvert.SerializeObject(req), null, false, onData).ConfigureAwait(false);
        }
        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

   

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            Console.WriteLine("HandleSubscriptionResponse");
            callResult = new CallResult<object>(message, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            //throw new NotImplementedException();
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            return Task.FromResult(true);
        }

        public void Ping()
        {
            //throw new NotImplementedException();
        }

        public async Task PingAsync()
        {
            //throw new NotImplementedException();
        }
    }
}
