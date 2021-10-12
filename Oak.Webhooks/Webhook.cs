using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Oak.Shared;
using Oak.Webhooks.Clients;

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

        public static Webhook<T> CreateWebhook(IServiceProvider s, string url, WebhookType type)
        {
            var clientFactory = s.GetRequiredService<IWebhookClientFactory>();
            return new Webhook<T>(clientFactory) { Url = url, Type = type };
        } 
    }
}