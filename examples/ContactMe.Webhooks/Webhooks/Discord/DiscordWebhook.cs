using System.Threading.Tasks;
using Oak.Events;
using Oak.Webhooks;
using Oak.Webhooks.Clients;

namespace ContactMe.Webhooks
{
    public class DiscordWebhook : Webhook<DiscordWebhookRequest>, IAsyncEventHandler<ContactMeEvent>, IWebhook<DiscordWebhookRequest>
    {
        public DiscordWebhook(IWebhookClientFactory webhookClientFactory) 
            : base(webhookClientFactory)
        {
        }

        public override string Url => "";

        public override  string Type => WebhookTypes.PostJson;

        public async Task HandleEventAsync(ContactMeEvent args)
        {
            await base.Send(new DiscordWebhookRequest(args));
        }
    }
}
