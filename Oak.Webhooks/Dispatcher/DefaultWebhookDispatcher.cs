using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    public class DefaultWebhookDispatcher : IWebhookDispatcher
    {
        public IWebhook<T> CreateWebhook<T>(string url, WebhookType type)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result> Send<T>(string url, WebhookType type)
        {
            throw new System.NotImplementedException();
        }
    }
}