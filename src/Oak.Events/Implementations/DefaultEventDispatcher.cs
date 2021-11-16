using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Oak.Events.Implementations
{
    public class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventDispatcherOptions _options;

        public DefaultEventDispatcher(
            IServiceProvider serviceProvider,
            IOptions<EventDispatcherOptions> options,
            ILogger<DefaultEventDispatcher> logger = null)
        {
            this._serviceProvider = serviceProvider;
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
                    dispatcher = this._serviceProvider.GetService<AmbiguousEventDispatcher>();
                    break;
                case EventDispatcherMode.Independent:
                    dispatcher = this._serviceProvider.GetService<AmbiguousEventDispatcher>();
                    break;
            }

            if (dispatcher is null)
                throw new NotSupportedException();

            return dispatcher;
        }
    }
}