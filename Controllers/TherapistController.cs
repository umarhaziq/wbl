using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;

namespace wbl.Controllers
{
    [Route("Therapist")]
    public class TherapistController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TherapistController> _logger;
        private readonly ApplicationDbContext _context;

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
            [Display(Name = "Phone Number")]
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

            [Display(Name = "Specialization")]
            public string Specialization { get; set; }

            [Required]
            [Display(Name = "Availability")]
            public string Availability { get; set; }

            [Required]
            [Display(Name = "Experience Level")]
            public string ExperienceLevel { get; set; }

            [Display(Name = "Notes or Comments")]
            public string Notes { get; set; }
        }

        public TherapistController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<TherapistController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet("AddTherapist")]
        public IActionResult AddTherapist()
        {
            return View();
        }

        [HttpPost("AddTherapist")]
        public async Task<IActionResult> AddTherapist(InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    CreatedAt = DateTime.Now,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Therapist created a new account.");

                    await _userManager.AddToRoleAsync(user, "Therapist");

                    var therapist = new Therapist
                    {
                        ApplicationUserId = user.Id,
                        Specialization = model.Specialization,
                        ExperienceLevel = model.ExperienceLevel,
                        Notes = model.Notes
                    };

                    _context.tb_Therapist.Add(therapist); // Ensure this adds to the correct table
                    await _context.SaveChangesAsync();

                    return RedirectToAction("ListTherapist");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpGet("ListTherapist")]
        public async Task<IActionResult> ListTherapist()
        {
            var therapists = await _context.tb_Therapist
                                         .Include(t => t.ApplicationUser)  // Include ApplicationUser navigation property
                                         .ToListAsync();

            var therapistList = therapists.Select(t => new TherapistViewModel
            {
                TherapistId = t.TherapistId,
                Email = t.ApplicationUser.Email,
                FullName = t.ApplicationUser.FullName,
                CreatedAt = t.ApplicationUser.CreatedAt,
                PhoneNumber = t.ApplicationUser.PhoneNumber,
                Specialization = t.Specialization,
                ExperienceLevel = t.ExperienceLevel,
                Notes = t.Notes
            }).ToList();

            ViewBag.numberOfTherapists = therapistList.Count;

            return View(therapistList);
        }

        [HttpGet]
        public async Task<IActionResult> EditTherapist(int id)
        {
            var therapist = await _context.tb_Therapist
                                        .Include(t => t.ApplicationUser)
                                        .FirstOrDefaultAsync(t => t.TherapistId == id);
            if (therapist == null)
            {
                return NotFound();
            }

            var model = new TherapistViewModel
            {
                TherapistId = therapist.TherapistId,
                Email = therapist.ApplicationUser.Email,
                FullName = therapist.ApplicationUser.FullName,
                CreatedAt = therapist.ApplicationUser.CreatedAt,
                PhoneNumber = therapist.ApplicationUser.PhoneNumber,
                Specialization = therapist.Specialization,
                ExperienceLevel = therapist.ExperienceLevel,
                Notes = therapist.Notes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTherapist(TherapistViewModel model)
        {
            if (ModelState.IsValid)
            {
                var therapist = await _context.tb_Therapist
                                            .Include(t => t.ApplicationUser) // Include ApplicationUser navigation property
                                            .FirstOrDefaultAsync(t => t.TherapistId == model.TherapistId);
                if (therapist == null)
                {
                    return RedirectToAction("ListTherapist");
                }

                therapist.ApplicationUser.Email = model.Email;
                therapist.ApplicationUser.FullName = model.FullName;
                therapist.ApplicationUser.PhoneNumber = model.PhoneNumber;
                therapist.Specialization = model.Specialization;
                therapist.ExperienceLevel = model.ExperienceLevel;
                therapist.Notes = model.Notes;

                _context.tb_Therapist.Update(therapist);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListTherapist");
            }

            return View(model);
        }

        [HttpGet("SetAvailability")]
        public IActionResult SetAvailability()
        {
            return View();
        }

        [HttpPost("SetAvailability")]
        public async Task<IActionResult> SetAvailability(int therapistId, DateTime availableDate)
        {
            var user = await _userManager.GetUserAsync(User);

            if (User != null)
            {
                var Therapist = await _context.tb_Therapist.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
                if (Therapist != null)
                {
                    therapistId = Therapist.TherapistId;
                }
            }

            var availability = new Availibility
            {
                TherapistId = therapistId,
                AvailableDate = availableDate
            };
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }



    }


}
