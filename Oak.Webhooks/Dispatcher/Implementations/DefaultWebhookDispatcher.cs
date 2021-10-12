using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Oak.Shared;
using Oak.Webhooks.Clients;

namespace Oak.Webhooks.Dispatcher.Implementations
{
    public class DefaultWebhookDispatcher : IWebhookDispatcher
    {
        private readonly IWebhookClientFactory _clientFactory;
        private readonly IServiceProvider _serviceProvider;

        public DefaultWebhookDispatcher(
            IWebhookClientFactory clientFactory,
            IServiceProvider serviceProvider)
        {
            this._clientFactory = clientFactory;
            this._serviceProvider = serviceProvider;
        }

        public IWebhook<T> CreateWebhook<T>(string url, string type)
        {
            return new Webhook<T>(this._clientFactory) { Type = type, Url = url };
        }

        public TWebhook GetWebhook<TWebhook, T>() where TWebhook : IWebhook<T>
        {
            return this._serviceProvider.GetService<TWebhook>();
        }

        public async Task<Result> Send<T>(string url, string type, T data)
        {
            return await this.CreateWebhook<T>(url, type).Send(data);
        }

        public async Task<Result> Send<T>(IWebhook<T> webhook, T data)
        {
            return await webhook.Send(data);
        }
    }
}