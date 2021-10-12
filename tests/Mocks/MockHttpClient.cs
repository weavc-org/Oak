using System.Net.Http;
using Moq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Oak.Tests.Mocks
{
    public class MockHttpClients
    {
        public Mock<HttpClient> Default() 
        {
            var mock = new Mock<HttpClient>();
            
            mock.Setup(m => m.PostAsJsonAsync<It.IsAnyType>(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<JsonSerializerOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.PutAsJsonAsync<It.IsAnyType>(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<JsonSerializerOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.PatchAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            mock.Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            return mock;
        }

        public Mock<HttpClient> ErrorResponse() 
        {
            var mock = new Mock<HttpClient>();
            
            mock.Setup(m => m.PostAsJsonAsync<It.IsAnyType>(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<JsonSerializerOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.PutAsJsonAsync<It.IsAnyType>(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<JsonSerializerOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.PatchAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            mock.Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            return mock;
        }
    }
}