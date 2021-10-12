using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Oak.Webhooks;
using Oak.Webhooks.Clients;
using Oak.Webhooks.Clients.Implementations;

namespace Oak.Tests.Webhooks
{
    public class TestWebhookClientFactory
    {
        private Mocks.MockClients mockClients;

        [SetUp]
        public void Setup()
        {
            this.mockClients = new Mocks.MockClients();
        }

        private IWebhookClientFactory getFactory(IServiceProvider provider = null) 
        {
            if (provider == null)
            {       
                provider = new MockServiceProvider().Provider((s) => 
                {
                    s.AddTransient(s => { return this.mockClients.Post_Json().Object; });
                });
            }

            return new DefaultWebhookClientFactory(provider);
        }

        [Test]
        public void Test_GetClient()
        {
            var factory = this.getFactory();
            var client = factory.GetWebhookClient(WebhookType.Post_Json);
            
            Assert.IsNotNull(client);
            Assert.AreEqual(WebhookType.Post_Json, client.Type);
        }

        [Test]
        public void Test_GetNonExistantClient()
        {
            var factory = this.getFactory(new MockServiceProvider().Provider());
            var client = factory.GetWebhookClient(WebhookType.Post_Json);
            
            Assert.IsNull(client);
        }
    }
}