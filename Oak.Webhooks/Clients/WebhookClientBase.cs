using System.Threading.Tasks;
using Oak.Events;
using Oak.Shared;
using Oak.Webhooks.Events;

namespace Oak.Webhooks.Clients
{
    public abstract class WebhookClientBase : IWebhookClient
    {
        protected readonly IEventDispatcher eventDispatcher;

        public WebhookClientBase(IEventDispatcher eventDispatcher = null)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public abstract WebhookType Type { get; }

        public virtual async Task<Result> Send<T>(string url, T data)
        {
            var task = await this._send(url, data);
            this.eventDispatcher?.EmitAsync(new OnWebhookEvent<T>(new WebhookEvent<T>(this, url, this.Type, data)));
            return task;
        }

        protected abstract Task<Result> _send<T>(string url, T data); 
    }
}