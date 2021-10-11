using Microsoft.Extensions.DependencyInjection;
using Oak.Events;

namespace Oak.Webhooks
{
    public static class Configure
    {
        public static void AddOakWebhooks(this IServiceCollection services)
        {
            services.AddHttpClient<IWebhookClient, PostJsonWebhookClient>();

            services.AddScoped<IWebhookClientFactory, WebhookClientFactory>();
            services.AddTransient<IWebhookDispatcher, WebhookDispatcher>();
        }

        public static void AddWebhook<TWebhook, T>(this IServiceCollection services) where TWebhook : class, IWebhook<T>
        {
            services.AddTransient<IWebhook<T>, TWebhook>();
        }

        public static void AddWebhookEvent<TEvent>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json) where TEvent : class, IEvent
        {

            // Need to be able to configure WebhookEventHandler with type and url
            // see: https://github.dev/dotnet/runtime/blob/main/src/libraries/Microsoft.Extensions.Http/src/DependencyInjection/HttpClientFactoryServiceCollectionExtensions.cs
            services.AddEvent<WebhookEventHandler<TEvent>, TEvent>();
            services.AddAsyncEvent<WebhookEventHandler<TEvent>, TEvent>();
        }

        public static void AddWebhook<T>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json)
        {
            services.AddTransient<IWebhook<T>, Webhook<T>>();
        }

        // IWebhookDispatcher<T>.Send(url, type, T)

    }
}