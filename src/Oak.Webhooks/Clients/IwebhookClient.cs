using System.Threading.Tasks;

namespace Oak.Webhooks.Clients
{
    /// <summary>
    /// Implement and register webhook clients. This acts to abstract how 
    /// different webhooks are sent - i.e. which HTTP methods, content types etc
    /// </summary>
    public interface IWebhookClient
    {
        string Type { get; }
        Task<Result> Send<T>(string url, T data);
    }
}