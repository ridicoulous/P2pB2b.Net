using CryptoExchange.Net.Objects;
using P2pB2b.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2pB2b.Net.Interfaces
{
    public interface IP2pClient
    {
        CallResult<Dictionary<string, AccountDetails>> GetAccountBalances();
        Task<CallResult<Dictionary<string, AccountDetails>>> GetAccountBalancesAsync(CancellationToken ct = default);
        CallResult<AccountDetails> GetAccountBalance(string currency);
        Task<CallResult<AccountDetails>> GetAccountBalanceAsync(string currency, CancellationToken ct = default);

        CallResult<Dictionary<string,List<P2pOrder>>> GetOrdersHistory();
        Task<CallResult<Dictionary<string, List<P2pOrder>>>> GetOrdersHistoryAsync(CancellationToken ct = default);

        CallResult<List<P2pOrderDeal>> GetOrderDeals(long orderId, int limit=50, int offset=0);
        Task<CallResult<List<P2pOrderDeal>>> GetOrderDealsAsync(long orderId, int limit = 50, int offset = 0,CancellationToken ct = default);

        CallResult<List<P2pExecutionHistory>> Getexecutions(string pair, int limit = 50, int offset = 0);
        Task<CallResult<List<P2pExecutionHistory>>> GetexecutionsAsync(string pair, int limit = 50, int offset = 0, CancellationToken ct = default);

        CallResult<List<P2pOrder>> GetOpenOrders(string pair, int limit = 50, int offset = 0);
        Task<CallResult<List<P2pOrder>>> GetOpenOrdersAsync(string pair, int limit = 50, int offset = 0, CancellationToken ct = default);


        CallResult<P2pOrder> PlaceOrder(string pair, P2pOrderSide side, decimal amount, decimal price);
        Task<CallResult<P2pOrder>> PlaceOrderAsync(string pair, P2pOrderSide side, decimal amount, decimal price, CancellationToken ct = default);

        CallResult<P2pOrder> CancelOrder(string pair, long orderId);
        Task<CallResult<P2pOrder>> CancelOrderAsync(string pair, long orderId, CancellationToken ct = default);





    }
}
