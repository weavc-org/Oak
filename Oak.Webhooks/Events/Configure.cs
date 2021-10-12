using Microsoft.Extensions.DependencyInjection;
using Oak.Events;
using Oak.Webhooks.Events.Implementations;

namespace Oak.Webhooks.Events
{
    public static class Configure
    {
        public static void AddWebhookEvent<TEvent>(this IServiceCollection services, string url, WebhookType type = WebhookType.Post_Json) where TEvent : class, IEvent
        {
            services.AddTransient(s => WebhookEventHandler<TEvent>.CreateAsyncEventHandler(s, url, type));
        }
    }
}