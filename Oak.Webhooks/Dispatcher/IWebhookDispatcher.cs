using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks.Dispatcher
{
    public interface IWebhookDispatcher
    {
        IWebhook<T> CreateWebhook<T>(string url, WebhookType type);
        Task<Result> Send<T>(string url, WebhookType type, T data);
        Task<Result> Send<T>(IWebhook<T> webhook, T data);
        TWebhook GetWebhook<TWebhook, T>() where TWebhook : IWebhook<T>;
    }
}