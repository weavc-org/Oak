using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oak.Shared;

namespace Oak.Webhooks
{
    public class WebhookClientFactory : IWebhookClientFactory, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        private List<IServiceScope> scopes = new List<IServiceScope>();

        public WebhookClientFactory(
            IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            foreach (var scope in this.scopes)
                scope.Dispose();
        }

        public IWebhookClient GetWebhookClient(WebhookType type)
        {
            var scope = this._serviceProvider.CreateScope();
            var clients = scope.ServiceProvider.GetServices<IWebhookClient>();
            scopes.Add(scope);
            return clients.FirstOrDefault(s => s.Type == type);
        }
    }
}