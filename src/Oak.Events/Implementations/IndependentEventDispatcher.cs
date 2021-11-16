using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class IndependentEventDispatcher : EventDispatcherBase, IEventDispatcher, IAsyncDisposable
    {
        public IndependentEventDispatcher(
            IServiceProvider serviceProvider, 
            ILogger<IndependentEventDispatcher> logger = null)
            : base(serviceProvider, logger)
        {
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
                await this.EmitEventAsync(e);
                this.EmitEvent(e);
            }
        }

        public virtual void Emit(IEvent @event)
        {
            this.EmitEvent(@event);
            this.EmitEvent(new OnPostEmitEvent(@event));
        }

        public async virtual Task EmitAsync(IEvent @event)
        {
            await this.EmitEventAsync(@event);
            await this.EmitEventAsync(new OnPostEmitEvent(@event));
        }

        private void EmitEvent(IEvent @event)
        {
            IEnumerable<object> events = GetHandlers(@event);

            foreach (var e in events)
            {
                InvokeHandler(@event, e);
            }
        }

        private async Task EmitEventAsync(IEvent @event)
        {
            IEnumerable<object> events = GetAsyncHandlers(@event);

            await Task.WhenAll(events.Select((Func<object, Task>)(e =>
            {
                return InvokeAsyncHandler(@event, e);
            })));
        }
    }
}