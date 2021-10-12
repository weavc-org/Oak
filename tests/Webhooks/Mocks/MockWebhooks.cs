using System.Threading.Tasks;
using Oak.Shared;
using Oak.Webhooks;
namespace Oak.Tests.Webhooks.Mocks
{
    public class MockWebhook1 : IWebhook<string>
    {
        public string Url => "https://test.com/api/hook1";
        public WebhookType Type => WebhookType.Post_Json;

        public Task<Result> Send(string data)
        {
            return Task.FromResult(new Result(success: true));
        }
    }
}