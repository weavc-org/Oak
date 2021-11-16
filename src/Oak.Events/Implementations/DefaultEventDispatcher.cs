using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Oak.Events.Implementations
{
    public class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IEnumerable<IEventDispatcher> _eventDispatchers;
        private readonly EventDispatcherOptions _options;

        public DefaultEventDispatcher(
            IEnumerable<IEventDispatcher> eventDispatchers,
            IOptions<EventDispatcherOptions> options,
            ILogger<DefaultEventDispatcher> logger = null)
        {
            this._eventDispatchers = eventDispatchers;
            this._options = options.Value;
        }

        public void Emit(IEvent @event)
        {
            this.RetrieveEventDispatcher().Emit(@event);
        }

        public async Task EmitAsync(IEvent @event)
        {
            await this.RetrieveEventDispatcher().EmitAsync(@event);
        }

        public void EmitOnDispose(IEvent @event)
        {
            this.RetrieveEventDispatcher().EmitOnDispose(@event);
        }

        private IEventDispatcher RetrieveEventDispatcher()
        {
            IEventDispatcher dispatcher = null;
            switch(this._options.Mode)
            {
                case EventDispatcherMode.Ambiguous:
                    dispatcher = this._eventDispatchers.FirstOrDefault(s => s is AmbiguousEventDispatcher);
                    break;
                case EventDispatcherMode.Independent:
                    dispatcher = this._eventDispatchers.FirstOrDefault(s => s is IndependentEventDispatcher);
                    break;
            }

            if (dispatcher is null)
                throw new NotSupportedException();

            return dispatcher;
        }
    }
}