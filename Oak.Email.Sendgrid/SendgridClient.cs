using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Oak.Core;
using Oak.Core.Errors;
using SendGrid.Helpers.Mail;

namespace Oak.Email.Sendgrid
{
    public class SendGridClient : IMailClient
    {
        private readonly SendgridOptions _sendgridOptions;
        private readonly EmailOptions _emailOptions;
        private SendGrid.SendGridClient _client; 
        private string _errorString = "Error Sending Emails";

        public SendGridClient(
            IOptions<SendgridOptions> sendgridOptions,
            IOptions<EmailOptions> emailOptions)
        {
            this._sendgridOptions = sendgridOptions.Value;
            this._emailOptions = emailOptions.Value;
            this._client = new SendGrid.SendGridClient(this._sendgridOptions.ApiKey); 
            this.FromDefault = new MailAddress(this._emailOptions.FromAddress);
        }

        public MailAddress FromDefault { get; set; }

        public async Task<Result> Send(MailMessage mail, int timeout = 10000)
        {
            if (!this._emailOptions.Active)
                return new Result(success: false, error: new UnavailableError("Email currently unavailable"));

            var errors = new List<(int status, string email, string error)>();

            var fromAddress = new EmailAddress(this.FromDefault.Address, this.FromDefault.DisplayName);
            foreach(var to in mail.To)
            {
                var toAddress = new EmailAddress(to.Address, to.DisplayName);
                var msg = MailHelper.CreateSingleEmail(fromAddress, toAddress, mail.Subject, mail.Body, mail.Body);

                var token = new CancellationTokenSource();
                token.CancelAfter(timeout);

                var result = await this._client.SendEmailAsync(msg, token.Token);
                if (!result.IsSuccessStatusCode)
                    errors.Add(((int)result.StatusCode, to.Address, await result.Body.ReadAsStringAsync()));
            }

            if (errors.Count == mail.To.Count)
                return new Result(success: false, error: new CustomError<List<(int status, string email, string error)>>(
                    400, this._errorString, this._errorString, this._errorString, errors));
            else if (errors.Count > 0)
                return new Result(success: true, error: new CustomError<List<(int status, string email, string error)>>(
                    200, this._errorString, this._errorString, this._errorString, errors));

            return new Result(success: true);
        }

        public void Dispose()
        {
            this._client = null;
        }
    }
}