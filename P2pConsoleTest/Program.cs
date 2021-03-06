﻿using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P2pB2b.Net;
using P2pB2b.Net.Interfaces;
using P2pB2b.Net.Objects;
using P2pB2b.Net.Objects.SocketParams;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var p2pClient = new P2pClient(new P2pClientOptions()
            {
                ApiCredentials = cred,
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
            //    LogWriters = new System.Collections.Generic.List<System.IO.TextWriter>(),
            }, auth);
            //var tr = p2pClient.GetOpenOrders("EEX_BTC");
            var tr2 = p2pClient.GetOrdersHistory();
            var res = tr2.Data["EEX_BTC"][0];
            var deals = p2pClient.GetOrderDeals(res.Id);

            //var place = await p2pClient.PlaceOrderAsync("BTC_USDT", P2pOrderSide.Sell, 0.001m, 20_000m);
            //if (place)
            //{
            //    var cancel = await p2pClient.CancelOrderAsync(place.Data.Market, place.Data.OrderId);
            //    while(!cancel)
            //    {
            //        cancel = await p2pClient.CancelOrderAsync(place.Data.Market, place.Data.OrderId);
            //    }
            //}

            P2pSymbolOrderBook ob = new P2pSymbolOrderBook("ETH_BTC", new P2pSymbolOrderBookOptions("P2p-ETH_BTC"));
            //ob.OnOrderBookUpdate += Ob_OnOrderBookUpdate;
            //ob.OnBestOffersChanged += Ob_OnBestOffersChanged;
            //ob.Start();

            var socket = new P2pSocketClient(new P2pSocketClientOptions()
            {
                AutoReconnect = true,
                ReconnectInterval = TimeSpan.FromSeconds(2),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Info,
                LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter("p2psocketlogger.log") }
            }, null);
            await socket.SubscribeDeals("ETH_BTC",OnData);
            Console.ReadLine();
        }

        private static void Ob_OnOrderBookUpdate(IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry> arg1, IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry> arg2)
        {
            Console.WriteLine(DateTime.UtcNow);
        }

        private static void OnData(P2pSocketEvent<DealsEvent> obj)
        {
            Console.WriteLine(obj.Data.Deals[0].Time);
        }

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(arg2.Price);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t"+arg1.Price+"\n");
            
        }

        static void OnData(P2pSocketEvent<P2pOrderBookUpdate> dynamic)
        {
            var res = dynamic;
        }
    }

}
