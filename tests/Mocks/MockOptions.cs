using Moq;
using Microsoft.Extensions.Options;

namespace Oak.Tests
{
    public class MockOptions
    {
        public MockOptions()
        {
        }

        public Mock<IOptions<T>> Default<T>(T model) where T : class
        {
            var mock = new Mock<IOptions<T>>();
            mock.Setup(m => m.Value).Returns(model);

            return mock;
        }
    }
}