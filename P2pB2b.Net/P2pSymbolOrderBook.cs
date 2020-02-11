using CryptoExchange.Net.Interfaces;
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
        P2pClient restClient;
        P2pSocketClient socketClient;
        private bool initialSnapshotDone;
        private int limit;

        public P2pSymbolOrderBook(string symbol, P2pSymbolOrderBookOptions options) : base(symbol, options)
        {
            limit = options.Limit;
            restClient = new P2pClient();
            socketClient = new P2pSocketClient();
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
            if (!initialSnapshotDone&&data.Data.IsFull)
            {
                SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.OrderBook.Bids, data.Data.OrderBook.Asks);
                initialSnapshotDone = true;
            }
            else
            {
                UpdateOrderBook(DateTime.UtcNow.Ticks, data.Data.OrderBook?.Bids?? new List<P2pOrderBookEntry>(), data.Data.OrderBook?.Asks ?? new List<P2pOrderBookEntry>());
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
