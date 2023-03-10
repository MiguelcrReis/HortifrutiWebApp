using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace HortifrutiWebApp.Pages
{
    public class RedefinePasswordModel : PageModel
    {
        #region Dependency Injection
        private readonly UserManager<AppUser> _userManager;
        public RedefinePasswordModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Parameters
        [BindProperty]
        public PasswordResetData Data { get; set; }
        public class PasswordResetData
        {
            [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
            [StringLength(16, MinimumLength = 6, ErrorMessage = "A \"{0}\" deve conter pelo menos \"{2}\" e no máximo  \"{1}\" caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Required(ErrorMessage = "\"{0}\" obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmarção de Senha")]
            [Compare("Password", ErrorMessage = "A confirmação de senha deve ser igual a senha.")]
            public string ConfirmationPassword { get; set; }

            public string Token { get; set; }

        }
        #endregion

        #region OnGet
        public IActionResult OnGet(string token = null)
        {
            if (token == null)
            {
                return BadRequest("Um token deve ser fornecido para poder redefinir a senha.");
            }
            else
            {
                Data = new PasswordResetData { Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)) };
                return Page();
            }
        }
        #endregion

        #region OnPost Async
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(Data.Email);

            if (user == null)
                // Não revela que o usuário que não existe, ou que o e-mail está incorreto
                return RedirectToPage("./ConfirmationRedefinePassword");

            var result = await _userManager.ResetPasswordAsync(user, Data.Token, Data.Password);

            if (result.Succeeded)
                return RedirectToPage("./ConfirmationRedefinePassword");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
        #endregion
    }
}
