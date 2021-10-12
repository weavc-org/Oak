using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Oak.Webhooks.Clients.Implementations
{
    public class DefaultWebhookClientFactory : IWebhookClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultWebhookClientFactory(
            IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IWebhookClient GetWebhookClient(string type)
        {
            return this._serviceProvider
                .GetServices<IWebhookClient>()
                .FirstOrDefault(s => s.Type == type);
        }
    }
}