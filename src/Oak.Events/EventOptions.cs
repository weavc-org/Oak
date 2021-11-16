namespace Oak.Events
{
    public class EventDispatcherOptions
    {
        /// <summary>
        /// Define how to handle emitted events.<br/>
        /// 
        /// <see cref="EventDispatcherMode.Ambiguous"/>: Asynchronous and synchronous event handlers both will 
        /// be triggered no matter what Emit method is used. Example: <see cref="IEventDispatcher.EmitAsync(IEvent)"/> 
        /// will trigger event handlers registered as both <see cref="IAsyncEventHandler{TEvent}"/> and/or <see cref="IEventHandler{TEvent}"/>.<br/>
        /// 
        /// <see cref="EventDispatcherMode.Independent"/>: Event handlers will only be triggered if the emit methods concurrency
        /// modifier matches the event handler. Example: <see cref="IEventDispatcher.EmitAsync(IEvent)"/>will only trigger
        /// event handlers registered as <see cref="IAsyncEventHandler{TEvent}"/>. 
        /// </summary>
        public EventDispatcherMode Mode  { get; set; }
    }

    public enum EventDispatcherMode
    {
        Ambiguous,
        Independent
    }
}