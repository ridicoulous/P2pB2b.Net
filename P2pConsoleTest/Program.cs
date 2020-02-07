using P2pB2b.Net;
using P2pB2b.Net.Interfaces;
using System;
using System.Threading.Tasks;

namespace P2pConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var s = new P2pSocketClient();
            await s.Test<dynamic>(OnData);
            Console.ReadLine();
        }
        static void OnData(dynamic dynamic)
        {
            string res = Convert.ToString(dynamic);
            if(res.Contains("depth.update"))
            {
                Console.WriteLine(res);

            }
        }
    }
}
