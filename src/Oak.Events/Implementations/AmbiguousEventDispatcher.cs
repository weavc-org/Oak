using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class AmbiguousEventDispatcher : EventDispatcherBase, IEventDispatcher, IAsyncDisposable
    {
        public AmbiguousEventDispatcher(
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
                await this.EmitEvent(e);
            }
        }

        public void Emit(IEvent @event)
        {
            this.EmitEvent(@event).GetAwaiter().GetResult();
            this.EmitEvent(new OnPostEmitEvent(@event)).GetAwaiter().GetResult();
        }

        public async Task EmitAsync(IEvent @event)
        {
            await this.EmitEvent(@event);
            await this.EmitEvent(new OnPostEmitEvent(@event));
        }

        private async Task EmitEvent(IEvent @event)
        {
            var asyncHandlers = this.GetAsyncHandlers(@event);
            var syncHandlers = this.GetHandlers(@event);

            foreach(var h in asyncHandlers)
            {
                await this.InvokeAsyncHandler(@event, h);
            }

            foreach(var h in syncHandlers)
            {
                this.InvokeHandler(@event, h);
            }
        }
    }
}