using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace P2pB2b.Net
{
    public class P2pAuthenticationProvider : AuthenticationProvider
    {
        private readonly object encryptLock = new object();
        private static readonly object nonceLock = new object();
        private static long lastNonce;
        HMACSHA512 encryptor;
        internal static string Nonce
        {
            get
            {
                lock (nonceLock)
                {
                    var nonce = (long)Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
                    if (nonce == lastNonce)
                        nonce += 1;

                    lastNonce = nonce;
                    return lastNonce.ToString(CultureInfo.InvariantCulture);
                }
            }
        }
        public P2pAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            encryptor = new HMACSHA512(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return new Dictionary<string, string>();

            var result = new Dictionary<string, string>();
            parameters.Add("request", uri.Split(new[] { ".io" }, StringSplitOptions.None)[1]);
            parameters.Add("nonce", Nonce);                                   
            var json = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
            string payload = Base64Encode(json);
            var signedData = Sign(payload);
            result.Add("X-TXC-APIKEY", Credentials.Key.GetString());
            result.Add("X-TXC-PAYLOAD",payload);
            result.Add("X-TXC-SIGNATURE", signedData.ToLower());

            return result;
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public override string Sign(string toSign)
        {           
            return ByteArrayToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(toSign)));
        }  
        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
