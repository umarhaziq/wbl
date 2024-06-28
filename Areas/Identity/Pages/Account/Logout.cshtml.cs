using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using wbl.Models;

namespace wbl.Areas.Identity.Pages.Account
{
    [AllowAnonymous] // Allow anonymous access to this logout page
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync(); // Sign the user out
            _logger.LogInformation("User logged out."); // Log the logout action

            // Redirect back to the original page if returnUrl is provided
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // If no returnUrl is provided, redirect to a default page
                return RedirectToPage();
            }
        }
    }
}
