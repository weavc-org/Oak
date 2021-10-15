using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.Email
{
    public class SimpleEmailService : IEmailService
    {
        private ILogger<SimpleEmailService> _logger;
        private IMailClient _mailClient;

        public SimpleEmailService(
            ILogger<SimpleEmailService> logger,
            IMailClient mailClient)
        {
            _mailClient = mailClient;
            _logger = logger;
        }

        public async Task<Result> Send(string to, string title, string body)
        {
            if (
                String.IsNullOrEmpty(to)
                || String.IsNullOrEmpty(title)
                || String.IsNullOrEmpty(body))
            {
                return new Result(success: false, message: "To, title and body parameter must not be empty");
            }

            MailMessage mail = this._mail(to, title, body);
            if (mail == null)
            {
                return new Result(success: false, message: "Encountered an error creating mail");
            }

            try
            {
                await _mailClient.Send(mail);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Result(success: false, message: "Encountered an error sending mail");
            }

            return new Result(success: true);
        }

        private MailMessage _mail(string to, string title, string body)
        {
            try
            {
                var mailto = new MailAddress(to);
                var mail = new MailMessage(this._mailClient.FromDefault, mailto);

                mail.Subject = title;
                mail.Body = body;
                mail.IsBodyHtml = false;
                return mail;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}