using Oak.Events;

namespace Oak.Webhooks.Events
{
    public class OnWebhookEvent<T> : IEvent
    {
        public OnWebhookEvent(IWebhookEvent<T> webhookEvent)
        {
            this.Sender = webhookEvent.Sender;
            this.WebhookEvent = webhookEvent;
        }

        public object Sender { get; set; }
        public IWebhookEvent<T> WebhookEvent { get; set; }
    }
}