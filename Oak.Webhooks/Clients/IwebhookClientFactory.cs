namespace Oak.Webhooks
{
    public  interface IWebhookClientFactory
    {
        IWebhookClient GetWebhookClient(WebhookType type);
    }
}