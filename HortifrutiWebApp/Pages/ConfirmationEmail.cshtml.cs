using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace HortifrutiWebApp.Pages
{
    public class ConfirmationEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmationEmailModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public string StatusMessage { get; set; }
        public bool ConfirmationEmail { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) RedirectToPage("/Index");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound($"Não foi localizado nenhum usuário com Id '{userId}'.");

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            ConfirmationEmail = result.Succeeded;
            StatusMessage = ConfirmationEmail ? "E-mail confirmado co sucesso!" : "Ocorreu um erro ao confirmar o e-mail.";

            return Page();
        }
    }
}
