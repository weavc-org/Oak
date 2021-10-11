using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Oak.Webhooks.Implementations
{
    public class DefaultWebhookClientFactory : IWebhookClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultWebhookClientFactory(
            IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IWebhookClient GetWebhookClient(WebhookType type)
        {
            var clients = this._serviceProvider.GetServices<IWebhookClient>();
            return clients.FirstOrDefault(s => s.Type == type);
        }
    }
}