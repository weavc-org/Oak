using System.Threading.Tasks;
using Oak.Events;

namespace Oak.Tests.Events
{
    public class MockEventHandler : IEventHandler<MockEvent>, IAsyncEventHandler<MockEvent>
    {
        public void HandleEvent(MockEvent ev)
        {
            ev.Action(ev.Sender);
        }

        public Task HandleEventAsync(MockEvent ev)
        {
            ev.Action(ev.Sender);
            return Task.CompletedTask;
        }
    }

    public class MockEventHandler2 : MockEventHandler, IEventHandler<MockEvent>, IAsyncEventHandler<MockEvent> { }
    public class MockEventHandler3 : MockEventHandler, IEventHandler<MockEvent>, IAsyncEventHandler<MockEvent> { }
    public class MockEventHandler4 : MockEventHandler, IEventHandler<MockEvent>, IAsyncEventHandler<MockEvent> { }
}