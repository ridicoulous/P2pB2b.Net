using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using P2pB2b.Net.Objects;
using P2pB2b.Net.Objects.SocketParams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P2pB2b.Net
{
    public class P2pSymbolOrderBook : SymbolOrderBook
    {
        private static P2pSymbolOrderBookOptions DefaultOptions = new P2pSymbolOrderBookOptions("P2pSymbolOrderBook");
        private readonly P2pClient restClient;
        private readonly P2pSocketClient socketClient;
        private bool initialSnapshotDone;
        private int limit;
        public P2pSymbolOrderBook(string symbol) : base(symbol, DefaultOptions)
        {
            limit = DefaultOptions.Limit;
            restClient = new P2pClient();
            socketClient = new P2pSocketClient(new P2pSocketClientOptions()
            {
                AutoReconnect = true,
                ReconnectInterval = TimeSpan.FromSeconds(2),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Info,
                LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter($"p2psocketlogger-{symbol}.log") }
            }, null);

        }
        public P2pSymbolOrderBook(string symbol, P2pSocketClient socketClient) : base(symbol, DefaultOptions)
        {
            limit = DefaultOptions.Limit;
            restClient = new P2pClient();
            this.socketClient = socketClient;
        }
        public P2pSymbolOrderBook(string symbol, P2pSymbolOrderBookOptions options) : base(symbol, options)
        {
            limit = options.Limit;
            restClient = new P2pClient();
            socketClient = new P2pSocketClient(new P2pSocketClientOptions()
            {
                AutoReconnect = true,
                ReconnectInterval = TimeSpan.FromSeconds(2),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Info,
                LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter($"p2psocketlogger-{symbol}.log") }
            }, null);
        }

        protected override void DoReset()
        {
            initialSnapshotDone = false;
        }

        protected override async Task<CallResult<bool>> DoResync()
        {
            return await WaitForSetOrderBook(10000).ConfigureAwait(false);
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            var result = await socketClient.SubscribeToOrderBookUpdates(Symbol, limit, ProcessUpdate).ConfigureAwait(false);
            if (!result)
                return result;

            Status = OrderBookStatus.Syncing;

            var setResult = await WaitForSetOrderBook(10000).ConfigureAwait(false);
            return setResult ? result : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        private void ProcessUpdate(P2pSocketEvent<P2pOrderBookUpdate> data)
        {
            if (!initialSnapshotDone && data.Data.IsFull)
            {
                SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.OrderBook.Bids, data.Data.OrderBook.Asks);
                initialSnapshotDone = true;
            }
            else
            {
                UpdateOrderBook(DateTime.UtcNow.Ticks, data.Data.OrderBook?.Bids ?? new List<P2pOrderBookEntry>(), data.Data.OrderBook?.Asks ?? new List<P2pOrderBookEntry>());
            }
        }
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            restClient?.Dispose();
            socketClient?.Dispose();
        }
    }
}
