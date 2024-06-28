using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using wbl.Data;
using wbl.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace wbl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(InputModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    CreatedAt = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(admin, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Admin created a new account with password.");

                    await _userManager.AddToRoleAsync(admin, "Admin");

                    return RedirectToAction("listAdmin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Phone NumberS")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> ListAdmin()
        {
            // Get all users
            var allUsers = await _userManager.GetUsersInRoleAsync("Admin");

            // Pass admins to view
            ViewBag.NumberOfAdmins = allUsers.Count;
            return View(allUsers);
        }


        [HttpGet]
        public async Task<IActionResult> EditAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            var model = new ApplicationUser
            {
                Id = admin.Id,
                Email = admin.Email,
                FullName = admin.FullName,
                PhoneNumber = admin.PhoneNumber,
                // Add other properties as needed
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByIdAsync(model.Id);
                if (admin == null)
                {
                    return NotFound();
                }

                admin.Email = model.Email;
                admin.FullName = model.FullName;
                admin.PhoneNumber = model.PhoneNumber;
                // Update other properties as needed

                var result = await _userManager.UpdateAsync(admin);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Admin details updated successfully.");
                    return RedirectToAction("ListAdmin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If ModelState is not valid, redisplay the form with the current model
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            var model = new ApplicationUser
            {
                Id = admin.Id,
                Email = admin.Email,
                FullName = admin.FullName,
                PhoneNumber = admin.PhoneNumber,
                // Add other properties as needed
            };

            return View(model); // Pass ApplicationUser directly
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(admin);
            if (result.Succeeded)
            {
                _logger.LogInformation("Admin deleted successfully.");
                return RedirectToAction("ListAdmin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // If delete operation fails, redirect back to details view or handle error as needed
            return RedirectToAction("ListAdmin");
        }

        public async Task<IActionResult> ChildApproval()
        {
            var pendingChildren = await _context.tb_child
                .Include(c => c.Programs)
                .Where(c => c.ApplicationStatus == "Pending")
                .ToListAsync();

            var pendingApplicationsCount = await _context.tb_child
          .CountAsync(c => c.ApplicationStatus == "Pending");

            // Store the count in TempData or ViewBag
            TempData["PendingApplicationsCount"] = pendingApplicationsCount;

            return View(pendingChildren);
        }

        public async Task<IActionResult> ApproveChild(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.tb_child.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            child.ApplicationStatus = "Approved";
            _context.Update(child);
            await _context.SaveChangesAsync();

            // Notify parent about approval (if required)

            return RedirectToAction(nameof(ChildApproval));
        }

        public async Task<IActionResult> RejectChild(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.tb_child.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            child.ApplicationStatus = "Rejected";
            _context.Update(child);
            await _context.SaveChangesAsync();

            // Notify parent about rejection (if required)

            return RedirectToAction(nameof(ChildApproval));
        }

        public async Task<IActionResult> ViewChild(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.tb_child
                .Include(c => c.Programs)
                .FirstOrDefaultAsync(m => m.Child_IC == id);

            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }
        private bool ChildExists(string id)
        {
            return _context.tb_child.Any(e => e.Child_IC == id);
        }
    }
}
