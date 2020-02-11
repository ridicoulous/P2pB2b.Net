using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }        

    }
    public class P2pResponse<T>: P2pResponse
    {      
        [JsonProperty("result")]
        public T Result { get; set; }

    }
    public class P2pResponseWithTotals<T>
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("result")]
        public T Result { get; set; }

    }
}
