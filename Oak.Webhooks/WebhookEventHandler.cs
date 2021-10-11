using System.Threading.Tasks;
using Oak.Events;
using Oak.Shared;

namespace Oak.Webhooks
{
    public class WebhookEventHandler<T> : IWebhookEventHandler<T>, IAsyncEventHandler<T>, IEventHandler<T>, IWebhook<T> where T : IEvent
    {
        private readonly IWebhookClientFactory _webhookClientFactory;

        public WebhookEventHandler(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public string Url { get; set; }
        public WebhookType Type { get; set; }

        public void HandleEvent(T args)
        {
            this.Send(args).GetAwaiter().GetResult();
        }

        public async Task HandleEventAsync(T args)
        {
            await this.Send(args);
        }

        public Task<Result> Send(T data)
        {
            return this._webhookClientFactory.GetWebhookClient(this.Type).Send(this.Url, data);
        }
    }
}