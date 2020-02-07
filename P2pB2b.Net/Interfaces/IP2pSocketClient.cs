using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P2pB2b.Net.Interfaces
{
    public interface IP2pSocketClient
    {
        void Ping();
        Task PingAsync();
    }
}
