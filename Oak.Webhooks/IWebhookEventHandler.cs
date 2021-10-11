using Oak.Events;

namespace Oak.Webhooks
{
    public interface IWebhookEventHandler<T> : IEventHandler<T>, IWebhook<T> where T : IEvent
    { 

    }
}