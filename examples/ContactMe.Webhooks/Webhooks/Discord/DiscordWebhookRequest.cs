using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContactMe.Webhooks
{
    public class DiscordWebhookRequest
    {
        public DiscordWebhookRequest(ContactMeEvent contactMeEvent)
        {
            this.Embeds = new List<DiscordEmbed>() 
            { 
                new DiscordEmbed() 
                { 
                    Title = contactMeEvent.Email, 
                    Description = contactMeEvent.Body 
                } 
            }.ToArray(); 
        }

        [JsonProperty("embeds")]
        public DiscordEmbed[] Embeds { get; set; }
    }

    public class DiscordEmbed
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
