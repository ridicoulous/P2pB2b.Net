using System;
using System.Collections.Generic;
using System.Text;

namespace P2pB2b.Net.Objects.SocketParams
{
    public static class SocketRequestFactory
    {
        private static int counter = 1;

        public static P2pSocketSubscribeRequest<T> Create<T>(T data, string method)
        {
            counter++;
            return new P2pSocketSubscribeRequest<T>(counter, method, data);

        }
    }
}
