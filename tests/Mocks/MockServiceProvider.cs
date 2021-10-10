using Moq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System;
using Oak.TaskScheduler.Services;

namespace Oak.Tests
{
    public class MockServiceProvider
    {

        public MockServiceProvider()
        {
        }

        public IServiceProvider Provider(Action<IServiceCollection> action = null)
        {
            IServiceCollection collection = new ServiceCollection();
            action?.Invoke(collection);

            return collection.BuildServiceProvider();
        }
    }
}