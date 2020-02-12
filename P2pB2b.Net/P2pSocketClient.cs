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
using System.Linq;
using System.Threading.Tasks;

namespace P2pB2b.Net
{
    public class P2pSocketClient : SocketClient, IP2pSocketClient
    {
        public P2pSocketClient() : base(new P2pSocketClientOptions(), null)
        {
            //Console.WriteLine(ping);
          //  AddGenericHandler("Pong", (connection, token) => { });

            SendPeriodic(TimeSpan.FromSeconds(1), con => JsonConvert.SerializeObject(SocketRequestFactory.Create("server.ping")));
        }
        public P2pSocketClient(P2pSocketClientOptions exchangeOptions, P2pAuthenticationProvider authenticationProvider) : base(exchangeOptions, authenticationProvider)
        {            
          //  AddGenericHandler("Pong", (connection, token) => { });
            SendPeriodic(TimeSpan.FromSeconds(1), con => SocketRequestFactory.Create("server.ping"));
        }
        public async Task<CallResult<UpdateSubscription>> Test<T>(string pair, Action<T> onData)
        {
            // var tre = new P2pSocketSubscribeRequest() { Id = 1, Method = "depth.subscribe", Params = new System.Collections.Generic.List<object>() {"ETH_BTC", 20, "0" } };
            var req = SocketRequestFactory.Create(new OrderBookSubscribeParam(pair), "depth.subscribe");
            Console.WriteLine(JsonConvert.SerializeObject(req));
            return await Subscribe(JsonConvert.SerializeObject(req), null, false, onData).ConfigureAwait(false);
        }
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdates(string pair, int limit, Action<P2pSocketEvent<P2pOrderBookUpdate>> onData)
        {
            var req = SocketRequestFactory.Create(new OrderBookSubscribeParam(pair, limit), "depth.subscribe");
            return await Subscribe(JsonConvert.SerializeObject(req), null, false, onData).ConfigureAwait(false);
        }
        public async Task<CallResult<UpdateSubscription>> SubscribeDeals(string pair, Action<P2pSocketEvent<DealsEvent>> onData)
        {
            var req = SocketRequestFactory.Create(new string[] { pair }, "deals.subscribe");
            //   await Subscribe(JsonConvert.SerializeObject(req), null, false, onData).ConfigureAwait(false);
            //  await Task.Delay(400);
            log.Write(CryptoExchange.Net.Logging.LogVerbosity.Info, $"Subscribing to {req.Method}");
            return await Subscribe(JsonConvert.SerializeObject(req), null, false, onData).ConfigureAwait(false);
        }
        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {            
            Console.WriteLine(data.ToString());
            callResult = null;
            var cRequest = Deserialize<P2pSocketEvent>(request.ToString());
            if(cRequest)
            {
                if (cRequest.Data.Method.Contains("ping") && (string)data["result"] == "pong")
                {
                    return true;
                }
            }
            var idField = data["id"];
            if (idField == null)
                return false;

            if ((int)idField != cRequest.Data.Id)
                return false;

            if (data["error"].Type != JTokenType.Null)
            {
                callResult = new CallResult<T>(default, new ServerError((int)data["error"]["code"], (string)data["error"]["message"]));
                return true;
            }
            else
            {
                var desResult = Deserialize<T>(data["result"]);
                if (!desResult)
                {
                    callResult = new CallResult<T>(default, desResult.Error);
                    return true;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }
        }



        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {

            if (message["method"]?.Type != JTokenType.Null)
            {
                callResult = new CallResult<object>(null, new P2pServerError(42, "Socket subscribtion error", message));
                return false;
            }

            if ((string)message["result"]["status"] != null && (string)message["result"]["status"] != "success")
            {
                callResult = new CallResult<object>(null, new P2pServerError(42, "Socket subscribtion error", message));
                return false;
            }
            callResult = new CallResult<object>(message, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            try
            {   
                var hRequest = JsonConvert.DeserializeObject<P2pSocketEvent>(request.ToString());
                return (hRequest.Method.Replace(".subscribe", "") == ((string)message["method"])?.Replace(".update", "")) ||(message.ToString().Contains("pong")&&hRequest.Method=="server.ping");
                
            }
            catch (Exception ex)
            {
                log.Write(CryptoExchange.Net.Logging.LogVerbosity.Error, $"Exception handled at MessageMatchingHandler:\n{ex.ToString()}");
                return false;
            }
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            try
            {
                if (message.Type != JTokenType.Object)
                    return false;
                return identifier == "Pong" && message["result"].ToString().Contains("pong");
            }
            catch (Exception)
            {
                return false;
            }
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

        /// <summary>
        /// Pings the server
        /// </summary>
        /// <returns>True if server responded, false otherwise</returns>
        public CallResult<bool> Ping() => PingAsync().Result;
        /// <summary>
        /// Pings the server
        /// </summary>
        /// <returns>True if server responded, false otherwise</returns>
        public async Task<CallResult<bool>> PingAsync()
        {
            var pingRequest = JsonConvert.SerializeObject(SocketRequestFactory.Create("server.ping"));

            var result = await Query<dynamic>(pingRequest, false).ConfigureAwait(false);
            return new CallResult<bool>(result.Success, result.Error);
        }
    }
}
