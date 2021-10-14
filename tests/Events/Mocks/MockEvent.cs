using System;
using Newtonsoft.Json;
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

        //[JsonIgnore]
        public object Sender { get; set; }
        [JsonIgnore]
        public Action<object> Action  { get; set; }
        public string Value => "Value";
    }
}