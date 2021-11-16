using System;
using System.Threading.Tasks;

namespace Oak.Events
{
    public interface IEventDispatcher
    {
        void Emit(IEvent @event);
        Task EmitAsync(IEvent @event);
        void EmitOnDispose(IEvent @event);
    }
}