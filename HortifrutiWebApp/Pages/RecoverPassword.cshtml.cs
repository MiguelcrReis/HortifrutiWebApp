using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HortifrutiWebApp.Contracts;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace HortifrutiWebApp.Pages
{
    public class RecoverPasswordModel : PageModel
    {
        #region Dependency Injection
        private UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        public RecoverPasswordModel(WebAppDbContext context, UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        #endregion

        #region Parameters
        [BindProperty]
        public EmailData Data { get; set; }
        #endregion

        #region OnGet
        public IActionResult OnGet()
        {
            return Page();
        }
        #endregion

        #region OnPost Async
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(Data.Email);

                if (user != null)
                {
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var urlPasswordReset = Url.Page("/RedefinePassword", null, new { token }, Request.Scheme);

                    StringBuilder message = new StringBuilder();
                    message.Append("<h1>HortiFruti Reis :: Confirmação de E-mail</h1>");
                    message.Append($"<p>Por gentileza, <a href='{HtmlEncoder.Default.Encode(urlPasswordReset)}'>clique aqui</a> para redefinir a sua senha.</p>");
                    message.Append("<p>Atenciosamente, <br>Equipe HortiFruti Reis</p>");
                    await _emailSender.SendEmailAsync(user.Email, "Recuperação de Senha", "", message.ToString());
                    return RedirectToPage("/RecoveryEmailSent");
                }
                else
                    return RedirectToPage("/RecoveryEmailSent");
            }
            return Page();
        }
        #endregion
    }
}
