using HortifrutiWebApp.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }


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
                var smtpClient = new SmtpClient();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(_emailConfiguration.ServerAddressEmail).ConfigureAwait(false);
                // Caso o servidor requer autenticação para enviar
                await smtpClient.AuthenticateAsync(_emailConfiguration.SenderEmail, _emailConfiguration.Password).ConfigureAwait(false);
                await smtpClient.SendAsync(message).ConfigureAwait(false);
                await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex) { throw new InvalidOperationException(ex.Message); }
        }
    }
}
