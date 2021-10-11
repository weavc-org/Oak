using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    public interface IWebhookDispatcher
    {
        IWebhook<T> CreateWebhook<T>(string url, WebhookType type);
        Task<Result> Send<T>(string url, WebhookType type);
    }
}