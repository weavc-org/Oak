using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Oak.Shared;
using Oak.Webhooks.Clients;

namespace Oak.Webhooks
{
    /// <summary>
    /// Basic implementation of <see cref="IWebhook{T}"/>. 
    /// This is used to dynamically create and register webhook definitions when
    /// only the Url and Type is provided, see <see cref="Configure.AddWebhook{T}(IServiceCollection, string, WebhookType)"/>. 
    /// </summary>
    /// <typeparam name="T">Type of model to be sent with the webhook.</typeparam>
    public class Webhook<T> : IWebhook<T>
    {
        protected readonly IWebhookClientFactory _webhookClientFactory;

        public Webhook(IWebhookClientFactory webhookClientFactory)
        {
            this._webhookClientFactory = webhookClientFactory;
        }

        public virtual string Url { get; set; }
        public virtual string Type { get; set; }

        public virtual Task<Result> Send(T data)
        {
            return this._webhookClientFactory.GetWebhookClient(this.Type).Send(this.Url, data);
        }

        public static Webhook<T> CreateWebhook(IServiceProvider s, string url, string type = WebhookTypes.PostJson)
        {
            var clientFactory = s.GetRequiredService<IWebhookClientFactory>();
            return new Webhook<T>(clientFactory) { Url = url, Type = type };
        } 
    }
}