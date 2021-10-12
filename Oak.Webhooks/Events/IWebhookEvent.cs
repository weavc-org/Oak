using Oak.Events;

namespace Oak.Webhooks.Events
{
    public interface IWebhookEvent<T> : IEvent
    {
        string Url { get; }
        WebhookType Type { get; }
        T Data { get; }
    }
}