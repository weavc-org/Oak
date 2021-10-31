using System.Threading.Tasks;
using Oak.Events;
using Oak.Webhooks.Events;

namespace Oak.Webhooks.Clients
{
    /// <summary>
    /// <see cref="WebhookClientBase"/> can be extended by webhook clients to provide shared utilities like 
    /// triggering events, parsing data etc.
    /// </summary>
    public abstract class WebhookClientBase : IWebhookClient
    {
        protected readonly IEventDispatcher eventDispatcher;

        public WebhookClientBase(IEventDispatcher eventDispatcher = null)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public abstract string Type { get; }

        public virtual async Task<Result> Send<T>(string url, T data)
        {
            var task = await this.PostData(url, data);
            this.eventDispatcher?.EmitAsync(new OnWebhookEvent<T>(this, url, this.Type, data));
            return task;
        }

        protected abstract Task<Result> PostData<T>(string url, T data); 
    }
}