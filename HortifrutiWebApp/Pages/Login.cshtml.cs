using System;
using System.Threading.Tasks;
using HortifrutiWebApp.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HortifrutiWebApp.Pages
{
    public class LoginModel : PageModel
    {
        #region Dependency Injection
        private readonly SignInManager<AppUser> _singInManager;
        public LoginModel(SignInManager<AppUser> signInManager)
        {
            _singInManager = signInManager;
        }
        #endregion

        #region Parameters
        [BindProperty]
        public LoginData Data { get; set; }

        // Salva para qual a pagina o usuario queria ir, pra quando fizer login ser redirecionado para ela
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMesage { get; set; }
        #endregion

        #region OnGet Async
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMesage))
            {
                ModelState.AddModelError(string.Empty, ErrorMesage);
            }

            returnUrl ??= Url.Content("~/");

            // Remove o cookie anterior para garantir um novo processo de login
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ReturnUrl = returnUrl;
        }
        #endregion

        #region OnPost Async
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _singInManager.PasswordSignInAsync(Data.Email, Data.Password, Data.Remember, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login inválido! Tente novamente.");
                    return Page();
                }
            }
            return Page();
        }
        #endregion

        #region OnPost Logout Async
        public async Task<ActionResult> OnPostLogoutAsync(string returnUrl = null)
        {
            await _singInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
        #endregion
    }
}
