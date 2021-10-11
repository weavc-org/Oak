using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    public  interface IWebhookClient
    {
        WebhookType Type { get; }
        Task<Result> Send<T>(string url, T data);
    }
}