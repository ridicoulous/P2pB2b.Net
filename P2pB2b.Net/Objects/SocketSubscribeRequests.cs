using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pSocketSubscribeRequest<T>
    {
        /// <summary>
        /// Note that <T> params fields must have [ArrayProperty(n)] attribute for deserializing
        /// </summary>
        /// <param name="params"></param>
        public P2pSocketSubscribeRequest()
        {

        }
        public P2pSocketSubscribeRequest(int id, string method, T @params)
        {
            Params = @params;
            Id = id;
            Method = method;
        }
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("params")]
        [JsonConverter(typeof(ArrayConverter))]
        public T Params { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
