using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IWebhookClientFactory, DefaultWebhookClientFactory>();
            services.AddTransient<IWebhookDispatcher, DefaultWebhookDispatcher>();
            services.AddAsyncEvent<DynamicWebhookEventHandler, PostEmitEvent>();
        }

        public static void AddWebhook<TWebhook, T>(this IServiceCollection services) where TWebhook : class, IWebhook<T>
        {
            services.AddTransient<IWebhook<T>, TWebhook>();
        }

        public static void AddWebhook<T>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json)
        {
            services.AddTransient(s => Webhook<T>.CreateWebhook(s, url, type));
        }
    }
}