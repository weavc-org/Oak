using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oak.Events.Implementations;

namespace Oak.Events
{
    public static class Configure
    {
        public static void AddOakEventDispatcher(this IServiceCollection services)
        {
            services.TryAddScoped<IEventDispatcher, AmbiguousEventDispatcher>();
        }

        public static void AddEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IEventHandler<TEvent>
        {
            services.AddOakEventDispatcher();
            services.AddTransient<IEventHandler<TEvent>, TEventHandler>();
        }

        public static void AddAsyncEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IAsyncEventHandler<TEvent>
        {
            services.AddOakEventDispatcher();
            services.AddTransient<IAsyncEventHandler<TEvent>, TEventHandler>();
        }

        public static void TryAddEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IEventHandler<TEvent>
        {
            services.AddOakEventDispatcher();
            services.TryAddTransient<IEventHandler<TEvent>, TEventHandler>();
        }

        public static void TryAddAsyncEvent<TEventHandler, TEvent>(this IServiceCollection services) where TEvent : IEvent where TEventHandler : class, IAsyncEventHandler<TEvent>
        {
            services.AddOakEventDispatcher();
            services.TryAddTransient<IAsyncEventHandler<TEvent>, TEventHandler>();
        }
    }
}