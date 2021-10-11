using System.Threading.Tasks;

namespace Oak.Events
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        void HandleEvent(TEvent args);
    }

    public interface IAsyncEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleEventAsync(TEvent args);
    }
}