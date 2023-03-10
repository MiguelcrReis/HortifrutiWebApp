using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HortifrutiWebApp.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        #region Parameters
        private readonly ILogger<ErrorModel> _logger;
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Parameters
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion

        #region OnGet
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
        #endregion
    }
}
