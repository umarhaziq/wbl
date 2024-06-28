using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using wbl.Models;
using Microsoft.AspNetCore.Authorization;

namespace wbl.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return RedirectToAction(nameof(AdminDashboard));
            }
            else if (roles.Contains("Parent"))
            {
                return RedirectToAction(nameof(ParentDashboard));
            }

            return View("AccessDenied");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        public IActionResult ParentDashboard()
        {
            return View();
        }
    }
}
