using Microsoft.Extensions.DependencyInjection;
using Oak.Events.Implementations;

namespace Oak.Events
{
    public static class Configure
    {
        public static void AddOakEventDispatcher(this IServiceCollection services)
        {
            services.AddScoped<IEventDispatcher, EventDispatcher>();
        }

        public static void AddEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IEventHandler<TEvent>
        {
            services.AddTransient<IEventHandler<TEvent>, TEventHandler>();
        }

        public static void AddAsyncEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IAsyncEventHandler<TEvent>
        {
            services.AddTransient<IAsyncEventHandler<TEvent>, TEventHandler>();
        }
    }
}