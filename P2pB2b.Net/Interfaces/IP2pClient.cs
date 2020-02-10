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

    }
}
