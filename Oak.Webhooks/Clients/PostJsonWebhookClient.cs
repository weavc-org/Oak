using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oak.Shared;

namespace Oak.Webhooks.Implementations
{
    public class PostJsonWebhookClient : IWebhookClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<PostJsonWebhookClient> _logger;

        public PostJsonWebhookClient(
            HttpClient client, 
            ILogger<PostJsonWebhookClient> logger = null)
        {
            this._client = client;
            this._logger = logger;
        }

        public WebhookType Type => WebhookType.Post_Json;

        public async Task<Result> Send<T>(string url, T data)
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