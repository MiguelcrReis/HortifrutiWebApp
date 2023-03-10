using System;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage);
    }
}
