using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Oak.Tests.Mocks
{
    public class FakeMessageHandler : HttpMessageHandler
    {
        private readonly int statusCode;

        public FakeMessageHandler(int statusCode)
        {
            this.statusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage((HttpStatusCode)this.statusCode));
        }
    }

    public class MockHttpClients
    {
        public HttpClient Default() 
        {
            return new HttpClient(new FakeMessageHandler(200));
        }

        public HttpClient ErrorResponse() 
        {
            return new HttpClient(new FakeMessageHandler(400));
        }
    }
}