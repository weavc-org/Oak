using Moq;
using Oak.Webhooks.Clients;

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