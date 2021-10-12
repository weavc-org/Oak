namespace Oak.Events
{
    public class PostEmitEvent : IEvent
    {
        public PostEmitEvent(
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