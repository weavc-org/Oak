using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.Events.Implementations
{
    public class DefaultEventDispatcher : SplitEventDispatcher, IEventDispatcher, IAsyncDisposable
    {
        public DefaultEventDispatcher(
            IServiceProvider serviceProvider, 
            ILogger<SplitEventDispatcher> logger = null) 
            : base(serviceProvider, logger)
        {
        }
        
        public override void Emit(IEvent @event)
        {
            base.EmitEvent(@event);
            base.EmitEventAsync(@event).GetAwaiter().GetResult();
            this.EmitEvent(new OnPostEmitEvent(@event.Sender, @event));
        }

        public override async Task EmitAsync(IEvent @event)
        {
            base.EmitEvent(@event);
            await base.EmitEventAsync(@event);
            await this.EmitEventAsync(new OnPostEmitEvent(@event.Sender, @event));
        }
    }
}