using Microsoft.Extensions.DependencyInjection;
using Oak.Events;
using Oak.Webhooks.Implementations;

namespace Oak.Webhooks
{
    public static class Configure
    {
        public static void AddOakWebhooks(this IServiceCollection services)
        {
            services.AddHttpClient<IWebhookClient, PostJsonWebhookClient>();
            services.AddScoped<IWebhookClientFactory, DefaultWebhookClientFactory>();
            services.AddTransient<IWebhookDispatcher, DefaultWebhookDispatcher>();
        }

        public static void AddWebhook<TWebhook, T>(this IServiceCollection services) where TWebhook : class, IWebhook<T>
        {
            services.AddTransient<IWebhook<T>, TWebhook>();
        }

        public static void AddWebhookEvent<TEvent>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json) where TEvent : class, IEvent
        {
            services.AddTransient(s => WebhookEventHandler<TEvent>.CreateAsyncEventHandler(s, url, type));
        }

        public static void AddWebhook<T>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json)
        {
            services.AddTransient(s => Webhook<T>.CreateWebhook(s, url, type));
        }

        // IWebhookDispatcher<T>.Send(url, type, T)

    }
}