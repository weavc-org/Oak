using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oak.Events.Implementations;

namespace Oak.Events
{
    public static class Configure
    {
        public static void AddOakEventDispatcher(
            this IServiceCollection services, 
            Action<EventDispatcherOptions> options)
        {
            services.AddScoped<IEventDispatcher, DefaultEventDispatcher>();
            services.AddScoped<IEventDispatcher, AmbiguousEventDispatcher>();
            services.AddScoped<IEventDispatcher, IndependentEventDispatcher>();
            services.Configure<EventDispatcherOptions>(options);
        }

        public static void AddEvent<TEventHandler, TEvent>(this IServiceCollection services) 
            where TEvent : IEvent where TEventHandler : class, IEventHandler<TEvent>
        {
            services.AddTransient<IEventHandler<TEvent>, TEventHandler>();
        }

        public static void AddAsyncEvent<TEventHandler, TEvent>(this IServiceCollection services) 
            where TEvent : IEvent where TEventHandler : class, IAsyncEventHandler<TEvent>
        {
            services.AddTransient<IAsyncEventHandler<TEvent>, TEventHandler>();
        }

        public static void TryAddEvent<TEventHandler, TEvent>(this IServiceCollection services) 
            where TEvent : IEvent where TEventHandler : class, IEventHandler<TEvent>
        {
            services.TryAddTransient<IEventHandler<TEvent>, TEventHandler>();
        }

        public static void TryAddAsyncEvent<TEventHandler, TEvent>(this IServiceCollection services) 
            where TEvent : IEvent where TEventHandler : class, IAsyncEventHandler<TEvent>
        {
            services.TryAddTransient<IAsyncEventHandler<TEvent>, TEventHandler>();
        }
    }
}