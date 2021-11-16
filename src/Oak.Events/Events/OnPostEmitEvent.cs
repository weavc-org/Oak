namespace Oak.Events
{
    public class OnPostEmitEvent : IEvent
    {
        public OnPostEmitEvent(
            IEvent @event)
        {
            Value = @event;
        }

        public IEvent Value { get; set; }
    }
}