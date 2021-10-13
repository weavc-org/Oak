namespace Oak.Webhooks
{
    /// <summary>
    /// Types of webhook. Used to identify which 
    /// <see cref="IWebhookClient"/> to use when sending the request.
    /// </summary>
    public static class WebhookTypes
    {
        public const string PostJson = "Oak.PostJson";    
    }
}