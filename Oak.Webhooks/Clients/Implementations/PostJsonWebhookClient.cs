using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oak.Events;
using Oak.Shared;

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
            var json = JsonContent.Create<T>(data);
            var response = await this._client.PostAsJsonAsync($"{url}", json);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = await response.Content.ReadAsStringAsync();

                this._logger?.LogCritical(error);
                return new Result(success: false, message: error);
            }

            return new Result(success: true);
        }
    }
}