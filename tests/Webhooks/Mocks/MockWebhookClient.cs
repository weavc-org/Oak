using System;
using System.Threading.Tasks;
using Moq;
using Oak.Webhooks;
using Oak.Webhooks.Clients;

namespace Oak.Tests.Webhooks.Mocks
{
    public class MockClients
    {
        public Mock<IWebhookClient> Post_Json() 
        {
            var mock = new Mock<IWebhookClient>();

            mock.Setup(m => m.Type).Returns(WebhookTypes.PostJson);
            mock.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<It.IsAnyType>()))
                .Returns(Task.FromResult(new Result(success: true)));

            return mock;
        }

        public Mock<IWebhookClient> Post_Json_Error() 
        {
            var mock = new Mock<IWebhookClient>();

            mock.Setup(m => m.Type).Returns(WebhookTypes.PostJson);
            mock.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<It.IsAnyType>()))
                .Returns(Task.FromResult(new Result(success: false, message: "Not Found")));

            return mock;
        }

        public Mock<IWebhookClient> Post_Json_Exception() 
        {
            var mock = new Mock<IWebhookClient>();

            mock.Setup(m => m.Type).Returns(WebhookTypes.PostJson);
            mock.Setup(m => m.Send(It.IsAny<string>(), It.IsAny<It.IsAnyType>()))
                .Throws(new Exception("Error parsing json"));

            return mock;
        }
    }
}