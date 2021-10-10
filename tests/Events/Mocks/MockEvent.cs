using System;
using Oak.Events;

namespace Oak.Tests.Events
{
    public class MockEvent : IEvent
    {
        public MockEvent(object sender, Action<object> action)
        {
            this.Sender = sender;
            this.Action = action;
        }

        public object Sender { get; set; }
        public Action<object> Action  { get; set; }
    }
}