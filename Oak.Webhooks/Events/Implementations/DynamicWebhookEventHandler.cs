using System;
using System.Threading.Tasks;
using Oak.Events;
using Oak.Shared;
using Oak.Webhooks.Clients;

namespace Oak.Webhooks.Events.Implementations
{
    public class DynamicWebhookEventHandler : IAsyncEventHandler<PostEmitEvent>
    {
        private readonly IWebhookClientFactory _webhookClientFactory;

        public DynamicWebhookEventHandler(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public Task HandleEventAsync(PostEmitEvent args)
        {
            if (args.EmittedEvent.GetType() != typeof(IWebhookEvent<>))
                return Task.CompletedTask;

            var url = (string)args.GetType()?.GetProperty("Url")?.GetValue(args);
            var type = (WebhookType?)args.GetType()?.GetProperty("Type")?.GetValue(args);
            var data = (object)args.GetType()?.GetProperty("Data")?.GetValue(args);

            if (string.IsNullOrEmpty(url))
                throw new Exception("Url value is null");

            if (type == null)
                throw new Exception("Type value is null");

            if (data == null || data == default)
                throw new Exception("Data value is null");

            return this._send(url, (WebhookType)type, data);
        }

        private Task<Result> _send(string url, WebhookType type, object data)
        {
            return this._webhookClientFactory.GetWebhookClient(type).Send(url, data);
        }
    }
}