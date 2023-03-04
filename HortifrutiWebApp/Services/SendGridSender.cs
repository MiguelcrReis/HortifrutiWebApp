using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Services
{
    public class SendGridSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage)
        {
            var apiKey = "apiKey";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("adm.hortifruti.reis@outlook.com", "HortiFruti Reis");
            var subjectEmail = subject;
            var to = new EmailAddress(email);
            var plainTextContent = textMessage;
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subjectEmail, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
