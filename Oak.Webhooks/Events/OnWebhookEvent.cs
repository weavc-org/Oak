using Oak.Events;

namespace Oak.Webhooks.Events
{
    /// <summary>
    /// OnWebhookEvent definition. Triggered when a webhook is has been called. 
    /// </summary>
    /// <typeparam name="T">Type of model data sent through to webhook</typeparam>
    public class OnWebhookEvent<T> : IEvent
    {
        public OnWebhookEvent(
            object sender, string url, WebhookType type, T data)
        {
            this.Sender = sender;
            this.Url = url;
            this.Type = type;
            this.Data = data;
        }

        public object Sender { get; set; }
        public string Url { get; set; }
        public WebhookType Type { get; set; }
        public T Data { get; set; }
    }
}