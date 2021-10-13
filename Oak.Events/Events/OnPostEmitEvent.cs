namespace Oak.Events
{
    public class OnPostEmitEvent : IEvent
    {
        public OnPostEmitEvent(
            object sender, 
            IEvent emittedEvent)
        {
            Sender = sender;
            EmittedEvent = emittedEvent;
        }

        public object Sender { get; set; }
        public IEvent EmittedEvent { get; set; }
    }
}