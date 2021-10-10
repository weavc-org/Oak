using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oak.Shared;
using Oak.Shared.Errors;

namespace Oak.Email.Smtp
{
    public class SmtpMailClient : IDisposable, IMailClient
    {
        private readonly SmtpOptions _options;
        private readonly SmtpClient _client;
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<SmtpMailClient> _logger;

        public MailAddress FromDefault { get; private set; }

        public SmtpMailClient(
            IOptions<SmtpOptions> options,
            IOptions<EmailOptions> emailOptions,
            ILogger<SmtpMailClient> logger = null)
        {
            this._options = options.Value;
            this._emailOptions = emailOptions.Value;
            this._client = _defaults();
            this.FromDefault = new MailAddress(this._emailOptions.FromAddress);
            this._logger = logger;
        }

        public async Task<Result> Send(MailMessage mail, int timeout = 10000)
        {
            if (!this._emailOptions.Active)
                return new Result(success: false, error: new UnavailableError("Email currently unavailable"));

            var token = new CancellationTokenSource();
            token.CancelAfter(timeout);

            try
            {
                await this._client.SendMailAsync(mail, token.Token);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ToString());
                return new Result(success: false);
            }

            return new Result(success: true);
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