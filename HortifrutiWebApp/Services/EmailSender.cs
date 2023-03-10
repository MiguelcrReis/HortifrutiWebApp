using HortifrutiWebApp.Contracts;
using HortifrutiWebApp.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Services
{
    public class EmailSender : IEmailSender
    {
        #region Dependency Injection
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }
        #endregion

        #region Send Email Async
        public async Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfiguration.SenderName, _emailConfiguration.SenderEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            var builder = new BodyBuilder { TextBody = textMessage, HtmlBody = htmlMessage };
            message.Body = builder.ToMessageBody();

            try
            {
                var smtp = new SmtpClient();

                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtp.ConnectAsync(_emailConfiguration.ServerAddressEmail, _emailConfiguration.ServerPortEmail).ConfigureAwait(false);
                await smtp.AuthenticateAsync(_emailConfiguration.SenderEmail, _emailConfiguration.Password).ConfigureAwait(false);
                await smtp.SendAsync(message).ConfigureAwait(false);
                await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex) { throw new InvalidOperationException(ex.Message); }
        }
        #endregion
    }
}
