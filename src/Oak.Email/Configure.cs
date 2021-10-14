using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oak.Email.Smtp;

namespace Oak.Email
{
    public static class Configure
    {
        public static void AddOakEmailService(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IEmailService, SimpleEmailService>();
        }

        public static void AddMailKitClient(this IServiceCollection serviceCollection, IConfiguration emailOptions, IConfiguration smtpOptions)
        {
            serviceCollection.AddOakEmailService();
            serviceCollection.TryAddTransient<IMailClient, MailKitClient>();
            serviceCollection.Configure<EmailOptions>(emailOptions);
            serviceCollection.Configure<SmtpOptions>(smtpOptions);
        }

        public static void AddSmtpClient(this IServiceCollection serviceCollection, IConfiguration emailOptions, IConfiguration smtpOptions)
        {
            serviceCollection.AddOakEmailService();
            serviceCollection.TryAddTransient<IMailClient, SmtpMailClient>();
            serviceCollection.Configure<EmailOptions>(emailOptions);
            serviceCollection.Configure<SmtpOptions>(smtpOptions);
        }
    }
}