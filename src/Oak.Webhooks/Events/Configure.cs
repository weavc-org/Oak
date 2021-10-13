using Microsoft.Extensions.DependencyInjection;
using Oak.Events;
using Oak.Webhooks.Events.Implementations;

namespace Oak.Webhooks.Events
{
    public static class Configure
    {
        /// <summary>
        /// Register a transient webhook event in <see cref="IServiceCollection"/>. 
        /// This will be dynamically created with the provided url and type, and
        /// registed as type <see cref="IAsyncEventHandler{TEvent}"/>. 
        /// </summary>
        /// <param name="serviceCollection">See <see cref="IServiceCollection"/>.</param>
        /// <param name="url">
        /// Url that will be called when the <see cref="IWebhook{T}.Send(T)"/> is called.
        /// </param>
        /// <param name="type">
        /// Type of webhook to use. This defines the type of call that will be used when the webhook is sent. 
        /// See <see cref="WebhookTypes"/> for available types provided in this library.
        /// </param>
        /// <typeparam name="TEvent">Type of <see cref="IEvent"/> the <see cref="IAsyncEventHandler{TEvent}"/> 
        /// should handle.</typeparam>
        public static void AddWebhookEvent<TEvent>(this IServiceCollection serviceCollection, string url, string type = WebhookTypes.PostJson) where TEvent : class, IEvent
        {
            serviceCollection.AddTransient(s => WebhookEventHandler<TEvent>.CreateAsyncEventHandler(s, url, type));
        }
    }
}