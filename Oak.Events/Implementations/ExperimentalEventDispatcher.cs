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
            base.Emit(@event);
            base.EmitAsync(@event).GetAwaiter().GetResult();
        }

        public override async Task EmitAsync(IEvent @event)
        {
            base.Emit(@event);
            await base.EmitAsync(@event);
        }
    }
}