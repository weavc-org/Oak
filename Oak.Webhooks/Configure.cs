using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oak.Events;
using Oak.Webhooks.Clients;
using Oak.Webhooks.Clients.Implementations;
using Oak.Webhooks.Dispatcher;
using Oak.Webhooks.Dispatcher.Implementations;
using Oak.Webhooks.Events.Implementations;

namespace Oak.Webhooks
{
    public static class Configure
    {
        public static void AddOakWebhooks(this IServiceCollection services)
        {
            services.AddHttpClient<IWebhookClient, PostJsonWebhookClient>();
            services.TryAddScoped<IWebhookClientFactory, DefaultWebhookClientFactory>();
            services.TryAddTransient<IWebhookDispatcher, DefaultWebhookDispatcher>();
            services.TryAddAsyncEvent<DynamicWebhookEventHandler, PostEmitEvent>();
        }

        public static void AddWebhook<TWebhook, T>(this IServiceCollection services) where TWebhook : class, IWebhook<T>
        {
            services.AddOakWebhooks();
            services.AddTransient<TWebhook, TWebhook>();
        }

        public static void AddWebhook<T>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json)
        {
            services.AddOakWebhooks();
            services.AddTransient(s => Webhook<T>.CreateWebhook(s, url, type));
        }
    }
}