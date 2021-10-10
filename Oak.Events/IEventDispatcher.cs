using System;
using System.Threading.Tasks;

namespace Oak.Events
{
    public interface IEventDispatcher : IAsyncDisposable
    {
        void Emit(IEvent @event);
        Task EmitAsync(IEvent @event);

        void EmitOnDispose(IEvent @event);
        void EmitAsyncOnDispose(IEvent @event);
    }
}