using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using System.Net.Mail;

namespace USSEScoreboard.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            SendGridMessage m = new SendGridMessage();
            m.AddTo(email);
            m.From = new MailAddress("admin@sedash.azurewebsites.net", "Dashboard Admin");
            m.Subject = subject;
            m.Html = message;

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");

            // create a Web transport, using API Key
            var transportWeb = new Web(apiKey);

            // Send the email, which returns an awaitable task.
            transportWeb.DeliverAsync(m);

            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
