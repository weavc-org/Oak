using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Webhooks
{
    /// <summary>
    /// Webhook interface, provides our <see cref="IWebhookDispatcher"/> 
    /// and <see cref="IWebhookClient"/> services with the necessary information
    /// to perform their tasks.
    /// </summary>
    /// <typeparam name="T">Type of model to send to the webhook.</typeparam>
    public interface IWebhook<T>
    {
        /// <summary>
        /// Full Url of webhook i.e. https://weav.ovh/api/hook1
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Type of webhook client / request to make. See <see cref="WebhookType"/> for 
        /// available types. 
        /// </summary>
        WebhookType Type { get; }

        /// <summary>
        /// Trigger webhook to send data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A <see cref="Result"/> with information on the webhooks success.</returns>
        Task<Result> Send(T data);
    }
}