using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Oak.Shared;
using Oak.Webhooks;
using Oak.Webhooks.Clients;
using Oak.Webhooks.Clients.Implementations;

namespace Oak.Tests.Webhooks.Mocks
{
    public class MockClientFactory
    {
        public Mock<IWebhookClientFactory> Default(Mock<IWebhookClient> returnValue = null) 
        {
            if (returnValue == null)
                returnValue = new MockClients().Post_Json();

            var mock = new Mock<IWebhookClientFactory>();
            mock.Setup(m => m.GetWebhookClient(It.IsAny<string>())).Returns(returnValue.Object);

            return mock;
        }
    }
}