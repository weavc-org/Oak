using System;
using System.Threading.Tasks;
using Oak.Events;
using Oak.Webhooks.Clients;

namespace Oak.Webhooks.Events.Implementations
{
    /// <summary>
    /// <see cref="DynamicWebhookEventHandler"/> hooks into <see cref="OnPostEmitEvent"/> and looks for any emitted 
    /// events that implement <see cref="IWebhookEvent{T}"/>. If the event does implement <see cref="IWebhookEvent{T}"/>
    /// a webhook is triggered using the details provided in the model.
    /// </summary>
    public class DynamicWebhookEventHandler : IAsyncEventHandler<OnPostEmitEvent>
    {
        private readonly IWebhookClientFactory _webhookClientFactory;

        public DynamicWebhookEventHandler(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public Task HandleEventAsync(OnPostEmitEvent args)
        {
            if (args.EmittedEvent.GetType() != typeof(IWebhookEvent<>))
                return Task.CompletedTask;

            var url = (string)args.GetType()?.GetProperty("Url")?.GetValue(args);
            var type = (string)args.GetType()?.GetProperty("Type")?.GetValue(args);
            var data = (object)args.GetType()?.GetProperty("Data")?.GetValue(args);

            if (string.IsNullOrEmpty(url))
                throw new Exception("Url value is null");

            if (string.IsNullOrEmpty(type))
                throw new Exception("Type value is null");

            if (data == null || data == default)
                throw new Exception("Data value is null");

            return this._send(url, type, data);
        }

        private Task<Result> _send(string url, string type, object data)
        {
            return this._webhookClientFactory.GetWebhookClient(type).Send(url, data);
        }
    }
}