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
                    this.Title(args), 
                    this.Body(args));
            }
        }

        private string Body(ContactMeEvent model)
        {
            return $"{model.Body}\n\n{this.Name(model)}";
        }

        private string Title(ContactMeEvent model)
        {
            
            return $"New Message from: {this.Name(model)}";
        }

        private string Name(ContactMeEvent model)
        {
            return !string.IsNullOrEmpty(model.Name) ? $"{model.Name} ({model.Email})" : $"{model.Email}";
        }
    }
}
