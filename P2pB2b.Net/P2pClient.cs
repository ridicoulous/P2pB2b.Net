using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using P2pB2b.Net.Interfaces;
using P2pB2b.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace P2pB2b.Net
{
    public class P2pClient : RestClient, IP2pClient
    {
        public P2pClient(P2pClientOptions exchangeOptions, P2pAuthenticationProvider authenticationProvider) : base(exchangeOptions, authenticationProvider)
        {
            this.log.UpdateWriters(new System.Collections.Generic.List<System.IO.TextWriter>() { new ThreadSafeFileWriter("log.log") });

        }
        private const string AccountBalanceEndpoint = "account/balance";
        private const string AccountBalancesEndpoint = "account/balances";
        private const string AccountOrdersHostory = "account/order_history";

        


        public CallResult<Dictionary<string, AccountDetails>> GetAccountBalances() => GetAccountBalancesAsync().Result;

        public async Task<CallResult<Dictionary<string, AccountDetails>>> GetAccountBalancesAsync(CancellationToken ct = default)
        {
            var result=  await SendRequest<P2pResponse<Dictionary<string, AccountDetails>>>(GetUrl(AccountBalancesEndpoint), HttpMethod.Post, ct, null, true, true).ConfigureAwait(false);
            return new CallResult<Dictionary<string, AccountDetails>>(result.Data?.Result, result.Error) ;
        }
        public CallResult< AccountDetails> GetAccountBalance(string currency) => GetAccountBalanceAsync(currency).Result;

        public async Task<CallResult<AccountDetails>> GetAccountBalanceAsync(string currency, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>() { { "currency",currency} };
            var result = await SendRequest<P2pResponse<AccountDetails>>(GetUrl(AccountBalanceEndpoint), HttpMethod.Post, ct, parameters, true, true).ConfigureAwait(false);
            return new CallResult<AccountDetails>(result.Data?.Result, result.Error);
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
                return new P2pServerError((int)error["errorCode"],(string)error["message"], error);
            }
            return null;
        }

   
        #endregion
    }
}
