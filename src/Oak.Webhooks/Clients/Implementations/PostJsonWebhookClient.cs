using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oak.Events;
using Oak.Shared;
using Newtonsoft.Json;
using System.Text;

namespace Oak.Webhooks.Clients.Implementations
{
    public class PostJsonWebhookClient : WebhookClientBase, IWebhookClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<PostJsonWebhookClient> _logger;

        public PostJsonWebhookClient(
            HttpClient client, 
            ILogger<PostJsonWebhookClient> logger = null,
            IEventDispatcher eventDispatcher = null) 
            : base(eventDispatcher)
        {
            this._client = client;
            this._logger = logger;
        }

        public override string Type => WebhookTypes.PostJson;

        protected override async Task<Result> _send<T>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var response = await this._client.PostAsync($"{url}", new StringContent(json, Encoding.UTF8, "application/json"));

            if ((int)response.StatusCode >= 400)
            {
                var error = await response.Content.ReadAsStringAsync();

                this._logger?.LogCritical(error);
                return new Result(success: false, message: error);
            }

            return new Result(success: true);
        }
    }
}