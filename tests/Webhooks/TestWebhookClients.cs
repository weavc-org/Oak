using System.Collections.Generic;
using NUnit.Framework;
using Oak.Tests.Mocks;
using Oak.Webhooks.Clients;
using Oak.Webhooks.Clients.Implementations;
using System.Linq;
using System.Threading.Tasks;
using Oak.Events;
using Oak.Tests.Events;

namespace Oak.Tests.Webhooks
{
    public class TestWebhookClients
    {
        private IEnumerable<IWebhookClient> _clients;
        private MockHttpClients _httpClients;
        private string _testUrl = "https://test.com/api/hook1";

        [SetUp]
        public void Setup()
        {
            this._httpClients = new MockHttpClients();
            var clients = new List<IWebhookClient>();
            clients.Add(new PostJsonWebhookClient(this._httpClients.Default()));
            
            this._clients = clients;
        }
        
        [Test]
        public void Test_Types()
        {
            var types = _clients.Select(s => s.Type);
            foreach(var t in types)
                Assert.IsTrue(!string.IsNullOrEmpty(t));
            
            var distinct = types.Distinct();
            Assert.AreEqual(distinct.Count(), types.Count());
        }

        [Test]
        public async Task Test_Sends()
        {
            int t = 0;
            foreach(var c in this._clients)
            {
                var result1 = await c.Send<string>(this._testUrl, "hello world");
                Assert.IsTrue(result1.Success);

                var result2 = await c.Send<object>(this._testUrl, new { a = "hello world", testObject = "Object" });
                Assert.IsTrue(result2.Success);

                var result3 = await c.Send<IEvent>(this._testUrl, new MockEvent(this, (s) => { t+=1; }));
                Assert.IsTrue(result3.Success);
            }
        }
    }
}