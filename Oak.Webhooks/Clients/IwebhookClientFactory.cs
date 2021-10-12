namespace Oak.Webhooks.Clients
{
    /// <summary>
    /// Factory for webhook clients. See <see cref="IWebhookClient"/> for more details. 
    /// </summary>
    public  interface IWebhookClientFactory
    {
        IWebhookClient GetWebhookClient(WebhookType type);
    }
}