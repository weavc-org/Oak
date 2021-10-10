using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using MimeKit;

namespace Oak.Email.Client.Smtp
{
    public class MailKitClient : IDisposable, IMailClient
    {
        private SmtpOptions options;
        private EmailOptions emailOptions;
        private MailKit.Net.Smtp.SmtpClient client;
        public MailAddress FromDefault { get; private set; }

        public MailKitClient(
            IOptions<SmtpOptions> options,
            IOptions<EmailOptions> emailOptions)
        {
            this.options = options.Value;
            this.emailOptions = emailOptions.Value;
            this.client = new MailKit.Net.Smtp.SmtpClient();
            this.FromDefault = new MailAddress(this.emailOptions.FromAddress);
        }

        public async Task Send(MailMessage mail, int timeout = 10000)
        {
            if (!this.emailOptions.Active)
                return;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mail.From.DisplayName, mail.From.Address));
            foreach (var to in mail.To)
            {
                message.To.Add(new MailboxAddress(to.DisplayName, to.Address));
            }
            message.Subject = mail.Subject;
            message.Body = new TextPart("html")
            {
                Text = mail.Body
            };

            var token = new CancellationTokenSource();
            token.CancelAfter(timeout);
            await this.client.ConnectAsync(this.options.Address, this.options.Port);
            await this.client.AuthenticateAsync(this.options.Username, this.options.Password);
            await this.client.SendAsync(message);
            return;
        }

        public void Dispose()
        {
            this.client.Disconnect(true);
            this.client.Dispose();
        }
    }
}