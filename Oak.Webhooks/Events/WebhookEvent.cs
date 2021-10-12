using Oak.Events;

namespace Oak.Webhooks.Events
{
    public class WebhookEvent<T> : IWebhookEvent<T>, IEvent
    {
        public WebhookEvent(object sender, string url, WebhookType type, T data)
        {
            this.Url = url;
            this.Type = type;
            this.Data = data;
            this.Sender = sender;
        }

        public string Url { get; set; }
        public WebhookType Type { get; set; }
        public T Data { get; set; }
        public object Sender { get; set; }
    }
}