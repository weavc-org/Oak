using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class ExperimentalEventDispatcher : DefaultEventDispatcher, IEventDispatcher, IAsyncDisposable
    {
        public ExperimentalEventDispatcher(
            IServiceProvider serviceProvider, 
            ILogger<DefaultEventDispatcher> logger = null) 
            : base(serviceProvider, logger)
        {
        }

        public override void Emit(IEvent @event)
        {
            base._emit(@event);
            base._emitAsync(@event).GetAwaiter().GetResult();
            this._emit(new PostEmitEvent(@event.Sender, @event));
        }

        public override async Task EmitAsync(IEvent @event)
        {
            base._emit(@event);
            await base._emitAsync(@event);
            await this._emitAsync(new PostEmitEvent(@event.Sender, @event));
        }
    }
}