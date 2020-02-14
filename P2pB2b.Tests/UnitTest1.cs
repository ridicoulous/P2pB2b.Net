using NUnit.Framework;
using P2pB2b.Net;
using System.Linq;

namespace P2pB2b.Tests
{
    public class Tests
    {
        public P2pClient _client = new P2pClient();
        [SetUp]
        public void Setup()
        {
            _client = new P2pClient();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [TestCase]
        public void ShouldGetOrderBook() 
        {
            var ob = _client.GetOrderBook("ETH_BTC");
            Assert.IsTrue(ob);
            Assert.AreEqual(ob.Data.Asks.Any(), ob.Data.Bids.Any());
        }

    }
}