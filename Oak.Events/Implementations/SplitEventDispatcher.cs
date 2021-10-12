using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class SplitEventDispatcher : IEventDispatcher, IAsyncDisposable
    {
        private readonly IServiceScope _serviceProvider;
        private readonly ILogger<SplitEventDispatcher> _logger;

        public SplitEventDispatcher(
            IServiceProvider serviceProvider, 
            ILogger<SplitEventDispatcher> logger = null)
        {
            this._serviceProvider = serviceProvider.CreateScope();
            this._logger = logger;
        }

        private List<IEvent> _queue = new List<IEvent>();

        public virtual void EmitOnDispose(IEvent @event)
        {
            this._queue.Add(@event);
        }

        public async virtual ValueTask DisposeAsync()
        {
            foreach(var e in this._queue)
            {
                await this._emitAsync(e);
                this._emit(e);
            }

            this._serviceProvider.Dispose();
        }

        public virtual void Emit(IEvent @event)
        {
            this._emit(@event);
            this._emit(new PostEmitEvent(@event.Sender, @event));
        }

        protected virtual void _emit(IEvent @event)
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

        public async virtual Task EmitAsync(IEvent @event)
        {
            await this._emitAsync(@event);
            await this._emitAsync(new PostEmitEvent(@event.Sender, @event));
        }

        protected virtual async Task _emitAsync(IEvent @event)
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