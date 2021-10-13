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
        /// <summary>
        /// Try to register webhook services with <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        public static void AddOakWebhooks(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IWebhookClient, PostJsonWebhookClient>();
            serviceCollection.TryAddScoped<IWebhookClientFactory, DefaultWebhookClientFactory>();
            serviceCollection.TryAddTransient<IWebhookDispatcher, DefaultWebhookDispatcher>();
            serviceCollection.TryAddAsyncEvent<DynamicWebhookEventHandler, OnPostEmitEvent>();
        }

        /// <summary>
        /// Register a transient webhook in <see cref="IServiceCollection"/>. 
        /// These can be picked up by <see cref="IWebhookDispatcher"/> and 
        /// <see cref="DynamicWebhookEventHandler"/>.
        /// </summary>
        /// <typeparam name="TWebhook">Type to register with DI. Must implement <see cref="IWebhook{T}"/>.</typeparam>
        /// <typeparam name="T">Type parameter of implemented <see cref="IWebhook{T}"/>.</typeparam>
        public static void AddWebhook<TWebhook, T>(this IServiceCollection serviceCollection) where TWebhook : class, IWebhook<T>
        {
            serviceCollection.AddOakWebhooks();
            serviceCollection.AddTransient<TWebhook, TWebhook>();
        }

        /// <summary>
        /// Register a transient webhook in <see cref="IServiceCollection"/>. 
        /// This will be dynamically created with the provided url and type, and
        /// registed as type <see cref="Webhook{T}"/>. 
        /// </summary>
        /// <param name="serviceCollection">See <see cref="IServiceCollection"/>.</param>
        /// <param name="url">
        /// Url that will be called when the <see cref="IWebhook{T}.Send(T)"/> is called.
        /// </param>
        /// <param name="type">Type of webhook to use. This defines the type of call that will 
        /// be used when the webhook is sent. See <see cref="WebhookType"/> for available types.</param>
        /// <typeparam name="T"></typeparam>
        public static void AddWebhook<T>(this IServiceCollection serviceCollection, string url, string type = WebhookTypes.PostJson)
        {
            serviceCollection.AddOakWebhooks();
            serviceCollection.AddTransient(s => Webhook<T>.CreateWebhook(s, url, type));
        }
    }
}