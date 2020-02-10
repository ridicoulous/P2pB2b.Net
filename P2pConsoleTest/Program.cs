using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P2pB2b.Net;
using P2pB2b.Net.Interfaces;
using P2pB2b.Net.Objects;
using P2pB2b.Net.Objects.SocketParams;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace P2pConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cred = new ApiCredentials("", "");
            var auth = new P2pAuthenticationProvider(cred);
            var s = new P2pClient(new P2pClientOptions()
            {
                ApiCredentials = cred,
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
                LogWriters = new System.Collections.Generic.List<System.IO.TextWriter>(),
            }, auth);
          //  var order = JsonConvert.DeserializeObject<P2pOrderHistory>("{\"amount\": \"1\",\"price\": \"0.01\",\"type\": \"limit\",\"id\": 9740,\"side\": \"sell\",\"ctime\": 1533568890.583023,\"takerFee\": \"0.002\",\"ftime\": 1533630652.62185,\"market\": \"ETH_BTC\",\"makerFee\": \"0.002\",\"dealFee\": \"0.002\",\"dealStock\": \"1\",\"dealMoney\": \"0.01\"            }");
            var sss = s.GetAccountBalance("BTC");
            string t = "{ \"method\": \"depth.update\",  \"params\": [    false,    {      \"bids\": [        [          \"0.022622\",          \"0.171\"        ],        [          \"0.022619\",          \"0.001\"        ]      ],      \"asks\": [        [          \"0.022719\",          \"0.011\"        ],        [          \"0.023107\",          \"300.3\"        ]      ]    },    \"ETH_BTC\"  ],  \"id\": null}\"";
            
            var deser = JsonConvert.DeserializeObject<P2pSocketSubscribeRequest<OrderBookSocketUpdateParam>>(t);

            var socket = new P2pSocketClient();
            await socket.Test<P2pSocketSubscribeRequest<OrderBookSocketUpdateParam>>("ETH_BTC",OnData);
            Console.ReadLine();
        }
        static void OnData(P2pSocketSubscribeRequest<OrderBookSocketUpdateParam> dynamic)
        {
            var res = dynamic;
        }
    }
}
