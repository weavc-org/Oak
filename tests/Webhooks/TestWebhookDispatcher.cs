using System;
using System.Reflection;
using NUnit.Framework;
using Oak.Webhooks;
using Oak.Webhooks.Dispatcher;
using Oak.Webhooks.Dispatcher.Implementations;

namespace Oak.Tests.Webhooks
{
    public class TestWebhookDispatcher
    {
        private Mocks.MockClientFactory mockClientFactory;
        private string _testUrl = "https://test.com/api/hook1";

        [SetUp]
        public void Setup()
        {
            this.mockClientFactory = new Mocks.MockClientFactory();
        }

        private IWebhookDispatcher getDispatcher(IServiceProvider provider = null) 
        {
            if (provider == null)
            {       
                provider = new MockServiceProvider().Provider((s) => 
                {
                    s.AddWebhook<Mocks.MockWebhook1, string>();
                    s.AddWebhook<object>(this._testUrl);
                });
            }

            return new DefaultWebhookDispatcher(this.mockClientFactory.Default().Object, provider);
        }

        
        [Test]
        public void Test_CreateWebhook()
        {
            var dispatcher = this.getDispatcher();
            var webhook = dispatcher.CreateWebhook<string>(this._testUrl, WebhookType.Post_Json);
            this.testWebhook(webhook, this._testUrl);
        }

        [Test]
        public void Test_GetWebhook()
        {
            var dispatcher = this.getDispatcher();

            var webhook1 = dispatcher.GetWebhook<Mocks.MockWebhook1, string>();
            this.testWebhook<string>(webhook1, this._testUrl, typeOf: typeof(Mocks.MockWebhook1));
            
            var webhook2 = dispatcher.GetWebhook<Webhook<object>, object>();
            this.testWebhook<object>(webhook2, this._testUrl);
        }

        private void testWebhook<T>(IWebhook<T> webhook, string url, WebhookType type = WebhookType.Post_Json, Type typeOf = null)
        {
            if (typeOf == null)
                typeOf = typeof(Webhook<T>);

            Assert.IsNotNull(webhook);
            Assert.AreEqual(url, webhook.Url);
            Assert.AreEqual(type, webhook.Type);
            Assert.AreEqual(typeOf, webhook.GetType());
            Assert.Contains(typeof(IWebhook<T>), 
                webhook.GetType().FindInterfaces(new TypeFilter((Type type, object criteria) => {
                    if (type == typeof(IWebhook<T>)) return true;
                    return false;
                }), true));
        }


    }
}