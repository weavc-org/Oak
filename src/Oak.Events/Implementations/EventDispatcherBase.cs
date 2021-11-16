using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public abstract class EventDispatcherBase
    {
        private const string AsyncHandlerMethod = "HandleEventAsync";
        private const string HandlerMethod = "HandleEvent";
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ILogger _logger;

        public EventDispatcherBase(
            IServiceProvider serviceProvider,
            ILogger logger = null)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        protected IEnumerable<object> GetAsyncHandlers(IEvent @event)
        {
            var eventType = @event.GetType();
            var eventHandler = typeof(IAsyncEventHandler<>).MakeGenericType(eventType);
            var events = this._serviceProvider.GetServices(eventHandler);

            return events;
        }

        protected Task InvokeAsyncHandler(IEvent @event, object e)
        {
            try
            {
                var method = e.GetType().GetMethod(AsyncHandlerMethod);
                var task = method.Invoke(e, new[] { @event });
                return (Task)task;
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ToString());
                return Task.CompletedTask;
            }
        }

        protected IEnumerable<object> GetHandlers(IEvent @event)
        {
            var eventType = @event.GetType();
            var eventHandler = typeof(IEventHandler<>).MakeGenericType(eventType);
            var events = this._serviceProvider.GetServices(eventHandler);

            return events;
        }

        protected void InvokeHandler(IEvent @event, object e)
        {
            try
            {
                var method = e.GetType().GetMethod(HandlerMethod);
                var task = method.Invoke(e, new[] { @event });
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ToString());
            }
        }
    }
}