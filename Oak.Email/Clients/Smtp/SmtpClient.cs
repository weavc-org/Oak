using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Oak.Email.Client.Smtp
{
    public class SmtpMailClient : IDisposable, IMailClient
    {
        private SmtpOptions _options;
        private SmtpClient _client;
        private EmailOptions _emailOptions;
        public MailAddress FromDefault { get; private set; }

        public SmtpMailClient(
            IOptions<SmtpOptions> options,
            IOptions<EmailOptions> emailOptions)
        {
            this._options = options.Value;
            this._emailOptions = emailOptions.Value;
            this._client = _defaults();
            this.FromDefault = new MailAddress(this._emailOptions.FromAddress);
        }

        public async Task Send(MailMessage mail, int timeout = 10000)
        {
            if (!this._emailOptions.Active)
                return;

            var token = new CancellationTokenSource();
            token.CancelAfter(timeout);
            await this._client.SendMailAsync(mail, token.Token);
            return;
        }

        public void Configure(Action<SmtpClient> action)
        {
            action.Invoke(this._client);
            return;
        }

        private SmtpClient _defaults()
        {
            var smtp = new SmtpClient();
            smtp.Host = this._options.Address;
            smtp.Port = this._options.Port;
            smtp.Timeout = 100000;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential(
                this._options.Username,
                this._options.Password);

            return smtp;

        }

        public void Dispose()
        {
            this._client.Dispose();
        }
    }
}