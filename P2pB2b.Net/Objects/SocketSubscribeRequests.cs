using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using P2pB2b.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pSocketEvent
    {
        public P2pSocketEvent()
        {

        }
        public P2pSocketEvent(string method, int? id)
        {
            Method = method;
            Id = id;
        }
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]
        public object[] Data { get; set; } = new object[0];

    }
    public class P2pSocketEvent<T>: P2pSocketEvent
    {
        /// <summary>
        /// Note that <T> params fields must have [ArrayProperty(n)] attribute for deserializing
        /// </summary>
        /// <param name="params"></param>
        public P2pSocketEvent()
        {

        }
        public P2pSocketEvent(int id, string method, T @params)
        {
            Data = @params;
            Id = id;
            Method = method;
        }

        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]    
        public new T Data { get; set; }
  
    }
   
}
