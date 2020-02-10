using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects
{   
    public class AccountDetails
    {
        [JsonProperty("available")]
        public decimal Available { get; set; }
        [JsonProperty("freeze")]
        public decimal Freeze { get; set; }
    }
}
