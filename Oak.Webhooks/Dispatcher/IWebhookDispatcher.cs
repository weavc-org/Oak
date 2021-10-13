using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks.Dispatcher
{
    /// <summary>
    /// Dispatcher for webhooks. Dynmically create webhooks, get registered webhooks and send webhooks.
    /// </summary>
    public interface IWebhookDispatcher
    {
        IWebhook<T> CreateWebhook<T>(string url, string type);
        TWebhook GetWebhook<TWebhook, T>() where TWebhook : IWebhook<T>;
        Task<Result> Send<T>(string url, string type, T data);
        Task<Result> Send<T>(IWebhook<T> webhook, T data);
    }
}