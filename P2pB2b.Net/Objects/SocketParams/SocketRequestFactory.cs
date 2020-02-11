using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    public static class SocketRequestFactory
    {
        private static int counter = 1;

        public static P2pSocketEvent<T> Create<T>(T data, string method)
        {
            counter++;
            return new P2pSocketEvent<T>(counter, method, data);

        }
        public static P2pSocketEvent Create(string method)
        {
            Console.WriteLine($"Creating {method} request");
            counter++;
            return new P2pSocketEvent( method, counter);

        }
    }
}
