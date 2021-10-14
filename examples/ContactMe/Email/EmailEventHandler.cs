using System.Threading.Tasks;
using ContactMe.Models;
using Microsoft.Extensions.Options;
using Oak.ContactMe.Models;
using Oak.Email;
using Oak.Events;

namespace ContactMe.Email
{
    public class EmailEventHandler : IAsyncEventHandler<ContactMeEvent>
    {
        private readonly IEmailService _emailService;
        private readonly HookOptions _options;

        public EmailEventHandler(
            IEmailService emailService,
            IOptions<HookOptions> options)
        {
            this._emailService = emailService;
            this._options = options.Value;
        }

        public async Task HandleEventAsync(ContactMeEvent args)
        {
            foreach (var email in this._options.Emails)
            {
                var result = await this._emailService.Send(
                    email, 
                    this._parseTitle(args), 
                    this._parseBody(args));
            }
        }

        private string _parseBody(ContactMeEvent model)
        {
            return $"{model.Body}\n\n{this._parseName(model)}";
        }

        private string _parseTitle(ContactMeEvent model)
        {
            
            return $"New Message from: {this._parseName(model)}";
        }

        private string _parseName(ContactMeEvent model)
        {
            return !string.IsNullOrEmpty(model.Name) ? $"{model.Name} ({model.Email})" : $"{model.Email}";
        }
    }
}
