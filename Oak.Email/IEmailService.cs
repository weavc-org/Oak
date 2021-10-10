using System.Threading.Tasks;
using Oak.Shared;

namespace Oak.Email
{
    /// <summary>
    /// Standard email service definition
    /// Might be expanded to send attachments etc at a later date
    /// </summary>
    public interface IEmailService : IBasicEmailService { }

    /// <summary>
    /// Send basic text/html emails
    /// </summary>
    public interface IBasicEmailService
    {
        Task<Result> Send(string to, string title, string body);
    }
}