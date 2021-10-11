using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks.Implementations
{
    public class DefaultWebhookDispatcher : IWebhookDispatcher
    {
        private readonly IWebhookClientFactory clientFactory;

        public DefaultWebhookDispatcher(IWebhookClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IWebhook<T> CreateWebhook<T>(string url, WebhookType type)
        {
            return new Webhook<T>(this.clientFactory) { Type = type, Url = url };
        }

        public async Task<Result> Send<T>(string url, WebhookType type, T data)
        {
            return await this.CreateWebhook<T>(url, type).Send(data);
        }
    }
}