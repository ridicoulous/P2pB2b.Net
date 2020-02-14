using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using P2pB2b.Net.Interfaces;
using P2pB2b.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace P2pB2b.Net
{
    public class P2pClient : RestClient, IP2pClient
    {
        public P2pClient() : base(new P2pClientOptions(), null)
        {

        }
        public P2pClient(P2pClientOptions options) : base(options, null)
        {

        }
        public P2pClient(P2pClientOptions exchangeOptions, P2pAuthenticationProvider authenticationProvider) : base(exchangeOptions, authenticationProvider)
        {
           
        }

        private const string AccountBalanceEndpoint = "account/balance";
        private const string AccountBalancesEndpoint = "account/balances";
        private const string AccountOrdersHostoryEndpoint = "account/order_history";
        private const string GetOrderDealsEndpoint = "account/order";
        private const string GetExecutionsByPairEndpoint = "account/executed_history";
        private const string GetOpenOrderEndpoint = "orders";
        private const string CreateOrderEndpoint = "order/new";
        private const string CancelOrderEndpoint = "order/cancel";
        private const string OrderBookDepthEndpoint = "public/depth/result";



        public CallResult<Dictionary<string, AccountDetails>> GetAccountBalances() => GetAccountBalancesAsync().Result;

        public async Task<CallResult<Dictionary<string, AccountDetails>>> GetAccountBalancesAsync(CancellationToken ct = default)
        {
            var result = await SendRequest<P2pResponse<Dictionary<string, AccountDetails>>>(GetUrl(AccountBalancesEndpoint), HttpMethod.Post, ct, null, true, true).ConfigureAwait(false);
            return new CallResult<Dictionary<string, AccountDetails>>(result.Data?.Result, result.Error);
        }
        public CallResult<AccountDetails> GetAccountBalance(string currency) => GetAccountBalanceAsync(currency).Result;

        public async Task<CallResult<AccountDetails>> GetAccountBalanceAsync(string currency, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>() { { "currency", currency } };
            var result = await SendRequest<P2pResponse<AccountDetails>>(GetUrl(AccountBalanceEndpoint), HttpMethod.Post, ct, parameters, true, true).ConfigureAwait(false);
            return new CallResult<AccountDetails>(result.Data?.Result, result.Error);
        }

        public CallResult<Dictionary<string, List<P2pOrder>>> GetOrdersHistory() => GetOrdersHistoryAsync().Result;

        public async Task<CallResult<Dictionary<string, List<P2pOrder>>>> GetOrdersHistoryAsync(CancellationToken ct = default)
        {
            var result = await SendRequest<P2pResponse<Dictionary<string, List<P2pOrder>>>>(GetUrl(AccountOrdersHostoryEndpoint), HttpMethod.Post, ct, null, true);
            return new CallResult<Dictionary<string, List<P2pOrder>>>(result.Data?.Result, result.Error);
        }

        public CallResult<List<P2pOrderDeal>> GetOrderDeals(long orderId, int limit = 50, int offset = 0) => GetOrderDealsAsync(orderId, limit, offset).Result;

        public async Task<CallResult<List<P2pOrderDeal>>> GetOrderDealsAsync(long orderId, int limit = 50, int offset = 0, CancellationToken ct = default)
        {
            if (offset > 10_000)
            {
                offset = 10_000;
            }
            if (limit < 1)
            {
                limit = 1;
            }
            if (limit > 10_000)
            {
                limit = 10_000;
            }
            if (offset < 0)
            {
                offset = 0;
            }
            var parameters = new Dictionary<string, object>()
            {
                { "orderId",orderId},
                {"limit",limit },
                {"offset",offset }
            };
            var result = await SendRequest<P2pResponse<P2pOrderDeals>>(GetUrl(GetOrderDealsEndpoint), HttpMethod.Post, ct, parameters, true);
            return new CallResult<List<P2pOrderDeal>>(result.Data?.Result.Records, result.Error);
        }
        public CallResult<List<P2pExecutionHistory>> Getexecutions(string pair, int limit = 50, int offset = 0) => GetexecutionsAsync(pair, limit, offset).Result;

        public async Task<CallResult<List<P2pExecutionHistory>>> GetexecutionsAsync(string market, int limit = 50, int offset = 0, CancellationToken ct = default)
        {
            if (offset > 10_000)
            {
                offset = 10_000;
            }
            if (limit < 1)
            {
                limit = 1;
            }
            if (limit > 10_000)
            {
                limit = 10_000;
            }
            if (offset < 0)
            {
                offset = 0;
            }
            var parameters = new Dictionary<string, object>()
            {
                { "market",market},
                {"limit",limit },
                {"offset",offset }
            };
            var result = await SendRequest<P2pResponse<List<P2pExecutionHistory>>>(GetUrl(GetExecutionsByPairEndpoint), HttpMethod.Post, ct, parameters, true);
            return new CallResult<List<P2pExecutionHistory>>(result.Data?.Result, result.Error);
        }


        public CallResult<List<P2pOrder>> GetOpenOrders(string pair, int limit = 50, int offset = 0) => GetOpenOrdersAsync(pair, limit, offset).Result;

        public async Task<CallResult<List<P2pOrder>>> GetOpenOrdersAsync(string market, int limit = 50, int offset = 0, CancellationToken ct = default)
        {
            if (offset > 10_000)
            {
                offset = 10_000;
            }
            if (limit < 1)
            {
                limit = 1;
            }
            if (limit > 10_000)
            {
                limit = 10_000;
            }
            if (offset < 0)
            {
                offset = 0;
            }
            var parameters = new Dictionary<string, object>()
            {
                { "market",market},
                {"limit",limit },
                {"offset",offset }
            };
            var result = await SendRequest<P2pResponse<List<P2pOrder>>>(GetUrl(GetOpenOrderEndpoint), HttpMethod.Post, ct, parameters, true);
            return new CallResult<List<P2pOrder>>(result.Data?.Result, result.Error);
        }
        public CallResult<P2pOrder> PlaceOrder(string market, P2pOrderSide side, decimal amount, decimal price) => PlaceOrderAsync(market, side, amount, price).Result;

        public async Task<CallResult<P2pOrder>> PlaceOrderAsync(string market, P2pOrderSide side, decimal amount, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market",market},
                {"side",side.ToString().ToLower()},
                {"amount",amount.ToString(CultureInfo.InvariantCulture)},
                {"price",price.ToString(CultureInfo.InvariantCulture)}
            };
            var request = await SendRequest<P2pResponse<P2pOrder>>(GetUrl(CreateOrderEndpoint), HttpMethod.Post, ct, parameters, true);
            return new CallResult<P2pOrder>(request.Data?.Result, request.Error);
        }
        public CallResult<P2pOrder> CancelOrder(string market, long orderId) => CancelOrderAsync(market, orderId).Result;

        public async Task<CallResult<P2pOrder>> CancelOrderAsync(string market, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market",market},
                {"orderId",orderId}
            };
            var result = await SendRequest<P2pResponse<P2pOrder>>(GetUrl(CancelOrderEndpoint), HttpMethod.Post, ct, parameters, true);
            return new CallResult<P2pOrder>(result.Data?.Result, result.Error);
        }
        public CallResult<P2pOrderBook> GetOrderBook(string market, int limit = 20, int interval = 0) => GetOrderBookAsync(market, limit, interval).Result;

        public async Task<CallResult<P2pOrderBook>> GetOrderBookAsync(string market, int limit = 20, int interval = 0, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "market",market},
                {"limit",limit},
                {"interval",interval}
            };
            var result = await SendRequest<P2pResponse<P2pOrderBook>>(GetUrl(OrderBookDepthEndpoint), HttpMethod.Get, ct, parameters, false);
            return new CallResult<P2pOrderBook>(result.Data?.Result, result.Error);

        }


        #region Helpers
        private Uri GetUrl(string endpoint, string version = "v2")
        {
            return new Uri($"{BaseAddress}/{version}/{endpoint}");
        }
        protected override Error ParseErrorResponse(JToken error)
        {
            if ((bool)error["success"] == false)
            {
                return new P2pServerError((int)error["errorCode"], (string)error["message"], error);
            }
            return null;
        }




        #endregion
    }
}
