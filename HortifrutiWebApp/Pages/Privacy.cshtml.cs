using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HortifrutiWebApp.Pages
{
    public class PrivacyModel : PageModel
    {
        #region Dependency Injection
        private readonly ILogger<PrivacyModel> _logger;
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        #endregion

        #region OnGet
        public void OnGet()
        {
        }
        #endregion
    }
}
