using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Email
{
    /// <summary>
    /// Smtp mail client service
    /// Implementations should handle settings and disposal of the client
    /// This makes testing easier since this can be easily mocked to send back
    /// positive results
    /// </summary>
    public interface IMailClient : IDisposable
    {
        /// <summary>
        /// Send email
        /// </summary>
        Task<Result> Send(MailMessage mail, int timeout = 10000);

        /// <summary>
        /// Email from address, default from appsettings is used,
        /// Should be used in MailMessage as the mailFrom parameter, but not required
        /// </summary>
        /// <value></value>
        MailAddress FromDefault { get; }
    }

}