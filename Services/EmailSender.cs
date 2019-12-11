using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GroupProject.Services 
{
    public class EmailSender : IEmailSender
    {
        // Constructor for email service
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        // Gets the user / API key from the secret manager
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        // Method from IEmailSender Interface
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        // Called by the interface method above but also brings in the apiKey parameter from the constructor above.
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("EmailVal@test.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            // Not the same method from interface. This is called from the SendGridClient Class
            return client.SendEmailAsync(msg);
        }
    }
}