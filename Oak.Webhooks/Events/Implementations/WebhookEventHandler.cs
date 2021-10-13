using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Oak.Events;
using Oak.Shared;
using Oak.Webhooks.Clients;

namespace Oak.Webhooks.Events.Implementations
{
    /// <summary>
    /// Generic implementation of <see cref="IAsyncEventHandler{T}"/>, <see cref="IEventHandler{T}"/> and <see cref="IWebhook{T}"/>.
    /// Will trigger a webhook when <see cref="IEvent"/> is emitted.
    /// </summary>
    /// <typeparam name="T">The implementation of <see cref="IEvent"/></typeparam>
    public class WebhookEventHandler<T> : IAsyncEventHandler<T>, IEventHandler<T>, IWebhook<T> where T : IEvent
    {
        private readonly IWebhookClientFactory _webhookClientFactory;

        public WebhookEventHandler(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public string Url { get; set; }
        public string Type { get; set; }

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

        public static IAsyncEventHandler<T> CreateAsyncEventHandler(IServiceProvider s, string url, string type = WebhookTypes.PostJson)
        {
            var clientFactory = s.GetRequiredService<IWebhookClientFactory>();
            return new WebhookEventHandler<T>(clientFactory) { Url = url, Type = type };
        }

        public static IEventHandler<T> CreateEventHandler(IServiceProvider s, string url, string type = WebhookTypes.PostJson)
        {
            var clientFactory = s.GetRequiredService<IWebhookClientFactory>();
            return new WebhookEventHandler<T>(clientFactory) { Url = url, Type = type };
        }
    }
}