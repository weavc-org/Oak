using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using MimeKit;
using Oak.Shared;
using Oak.Shared.Errors;
using Microsoft.Extensions.Logging;

namespace Oak.Email.Smtp
{
    public class MailKitClient : IDisposable, IMailClient
    {
        private readonly SmtpOptions _options;
        private readonly EmailOptions _emailOptions;
        private MailKit.Net.Smtp.SmtpClient _client;
        private readonly ILogger<MailKitClient> _logger;

        public MailAddress FromDefault { get; private set; }

        public MailKitClient(
            IOptions<SmtpOptions> options,
            IOptions<EmailOptions> emailOptions,
            ILogger<MailKitClient> logger = null)
        {
            this._options = options.Value;
            this._emailOptions = emailOptions.Value;
            this._client = new MailKit.Net.Smtp.SmtpClient();
            this.FromDefault = new MailAddress(this._emailOptions.FromAddress);
            this._logger = logger;
        }

        public async Task<Result> Send(MailMessage mail, int timeout = 10000)
        {
            if (!this._emailOptions.Active)
                return new Result(success: false, error: new UnavailableError("Email currently unavailable"));

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
            try
            {
                await this._client.ConnectAsync(this._options.Address, this._options.Port);
                await this._client.AuthenticateAsync(this._options.Username, this._options.Password);
                await this._client.SendAsync(message, token.Token);
            }
            catch(Exception ex)
            {
                this._logger?.LogError(ex.ToString());
                return new Result(success: false);
            }
            
            return new Result(success:true);
            
        }

        public void Dispose()
        {
            this._client.Disconnect(true);
            this._client.Dispose();
        }
    }
}