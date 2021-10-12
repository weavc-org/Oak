using Oak.Events;

namespace Oak.Webhooks.Events
{
    /// <summary>
    /// WebhookEvent interface for integration with <see cref="IEventDispatcher"/>.
    /// Used to trigger webhooks from events via <see cref="DynamicWebhookEventHandler"/>.
    /// </summary>
    /// <typeparam name="T">Type of model to send to the webhook.</typeparam>
    public interface IWebhookEvent<T> : IEvent
    {
        string Url { get; }
        WebhookType Type { get; }
        T Data { get; }
    }
}