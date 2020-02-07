using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{
    public class P2pSocketSubscribeRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("params")]
        public List<object> Params { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
