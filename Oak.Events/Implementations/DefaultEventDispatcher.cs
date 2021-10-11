using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class DefaultEventDispatcher : IEventDispatcher, IAsyncDisposable
    {
        private readonly IServiceScope _serviceProvider;
        private readonly ILogger<DefaultEventDispatcher> _logger;

        public DefaultEventDispatcher(
            IServiceProvider serviceProvider, 
            ILogger<DefaultEventDispatcher> logger = null)
        {
            this._serviceProvider = serviceProvider.CreateScope();
            this._logger = logger;
        }

        private List<IEvent> _asyncQueue = new List<IEvent>();
        private List<IEvent> _syncQueue = new List<IEvent>();

        public virtual void EmitAsyncOnDispose(IEvent @event)
        {
            this._asyncQueue.Add(@event);
        }

        public virtual void EmitOnDispose(IEvent @event)
        {
            this._syncQueue.Add(@event);
        }

        public async ValueTask DisposeAsync()
        {
            foreach(var e in this._asyncQueue)
                await this.EmitAsync(e);

            foreach(var e in this._syncQueue)
                this.Emit(e);

            this._serviceProvider.Dispose();
        }

        public virtual void Emit(IEvent @event)
        {
            var eventType = @event.GetType();
            var eventHandler = typeof(IEventHandler<>).MakeGenericType(eventType);
            var events = this._serviceProvider.ServiceProvider.GetServices(eventHandler);

            foreach(var e in events)
            {
                var method = e.GetType().GetMethod("HandleEvent");

                try 
                {
                    var task = method.Invoke(e, new[] { @event });
                }
                catch (Exception ex) 
                { 
                    this._logger?.LogError(ex.ToString());
                }
            }
        }

        public virtual async Task EmitAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            var eventHandler = typeof(IAsyncEventHandler<>).MakeGenericType(eventType);
            var events = this._serviceProvider.ServiceProvider.GetServices(eventHandler);

            await Task.WhenAll(events.Select(e =>
            {
                var method = e.GetType().GetMethod("HandleEventAsync");

                try 
                {
                    var task = method.Invoke(e, new[] { @event });
                    return (Task)task;
                }
                catch (Exception ex) 
                { 
                    this._logger?.LogError(ex.ToString());
                    return Task.CompletedTask;
                }
            }));
        }
    }
}