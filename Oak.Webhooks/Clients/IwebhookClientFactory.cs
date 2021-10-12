namespace Oak.Webhooks.Clients
{
    public  interface IWebhookClientFactory
    {
        IWebhookClient GetWebhookClient(WebhookType type);
    }
}