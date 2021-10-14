using Oak.Events;

namespace ContactMe.Models
{
    public class ContactMeEvent : ContactMeBindingModel, IEvent
    {
        public ContactMeEvent(object sender, ContactMeBindingModel model)
        {
            base.Email = model.Email;
            base.Body = model.Body;
            this.Sender = sender;
        }

        public object Sender { get; set; }
    }
}
