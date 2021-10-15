using System;
using System.Collections.Generic;
using ContactMe.Models;
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
                    Author = new DiscordAuthor 
                    { 
                        Name = !string.IsNullOrEmpty(contactMeEvent.Name) ? 
                            $"{contactMeEvent.Name} ({contactMeEvent.Email})" : $"{contactMeEvent.Email}",
                    }, 
                    Description = contactMeEvent.Body,
                    Colour = 0xffad49,
                    Timestamp = DateTime.UtcNow.ToUniversalTime().ToString("o")
                }
            };
        }

        [JsonProperty("embeds")]
        public IEnumerable<DiscordEmbed> Embeds { get; set; }
    }

    public class DiscordEmbed
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public DiscordAuthor Author { get; set; }

        [JsonProperty("color")]
        public int Colour { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class DiscordAuthor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
