using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    public interface IWebhook<T>
    {
        string Url { get; }
        WebhookType Type { get; }
        Task<Result> Send(T data);
    }
}