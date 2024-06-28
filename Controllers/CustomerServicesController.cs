using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;

namespace wbl.Controllers
{
    public class CustomerServiceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<CustomerServiceController> _logger;
        private readonly ApplicationDbContext _context;
        public CustomerServiceController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<CustomerServiceController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(12)]
            [Display(Name = "IC Number")]
            public string IcNumber { get; set; }

            [Required]
            [StringLength(12)]
            [Display(Name = "Status")]
            public string Status { get; set; } 

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


        // GET: CustomerService
        public async Task<IActionResult> Index()
        {
            var customerServices = await _context.CustomerServices.ToListAsync();
            return View(customerServices);
        }

        // GET: CustomerService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(m => m.CustomerServiceId == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.Name,
                    CreatedAt = DateTime.Now,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Therapist created a new account.");

                    await _userManager.AddToRoleAsync(user, "CustomerService");

                    var cs = new CustomerService
                    {
                        ApplicationUserId = user.Id,
                        Name = model.Name,
                        Email = model.Email,
                        IcNumber = model.IcNumber,
                        Status = model.Status,
                        Phone = model.PhoneNumber
                    };

                    _context.CustomerServices.Add(cs);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        // GET: CustomerService/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .Include(cs => cs.ApplicationUser) // Ensure ApplicationUser is included
                .FirstOrDefaultAsync(cs => cs.CustomerServiceId == id);

            if (customerService == null)
            {
                return NotFound();
            }

            // Map to ViewModel
            var viewModel = new CustomerServiceViewModel
            {
                Id = customerService.CustomerServiceId,
                Email = customerService.Email,
                FullName = customerService.Name,
                CreatedAt = customerService.ApplicationUser.CreatedAt, // This should now be populated
                IcNumber = customerService.IcNumber,
                Phone = customerService.Phone,
                Status = customerService.Status
            };

            return View(viewModel);
        }


        // POST: CustomerService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Email, FullName, IcNumber, Phone, Status")] CustomerServiceViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customerService = await _context.CustomerServices.FindAsync(id);
                    if (customerService == null)
                    {
                        return NotFound();
                    }

                    // Update the properties
                    customerService.Email = model.Email;
                    customerService.Name = model.FullName;
                    customerService.IcNumber = model.IcNumber;
                    customerService.Phone = model.Phone;
                    customerService.Status = model.Status;

                    _context.Update(customerService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerServiceExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: CustomerService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(m => m.CustomerServiceId == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        // POST: CustomerService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerService = await _context.CustomerServices.FindAsync(id);
            _context.CustomerServices.Remove(customerService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerService/ChangeStatus/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService == null)
            {
                return NotFound();
            }

            customerService.Status = customerService.Status == "Active" ? "Inactive" : "Active";
            _context.Update(customerService);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerServiceExists(int id)
        {
            return _context.CustomerServices.Any(e => e.CustomerServiceId == id);
        }

        public IActionResult RegisterSession(int patientId, DateTime selectedDate)
        {


            var therapists = _context.tb_Therapist.ToList();
            var sessionTimes = _context.SessionTimes.ToList();
            ViewBag.ChildId = patientId;
            ViewBag.SelectedDate = selectedDate;
            ViewBag.Therapists = therapists;
            ViewBag.SessionTimes = sessionTimes;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSession(int ChildId, int therapistId, int staffId, DateTime selectedDate, int timeId)
        {
            var session = new Session
            {
                ChildId = ChildId,
                TherapistId = therapistId,
                CustomerServiceId = staffId,
                Date = selectedDate,
                SessionTimeId = timeId
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
