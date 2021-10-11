using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    public class Webhook<T> : IWebhook<T>
    {
        private readonly IWebhookClientFactory _webhookClientFactory;

        public Webhook(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public string Url { get; set; }
        public WebhookType Type { get; set; }

        public Task<Result> Send(T data)
        {
            return this._webhookClientFactory.GetWebhookClient(this.Type).Send(this.Url, data);
        }
    }
}