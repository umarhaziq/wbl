using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using wbl.Models;
using wbl.Data;

namespace wbl.Controllers
{
    public class Parent1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Parent1Controller(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Parent1/Create
        // GET: Parent1/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var parent1 = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            var viewModel = new ParentViewModel
            {
                // Populate fields with the user's email, phone, and full name
                FatherName = user.FullName,
                FatherPhone = user.PhoneNumber,
                FatherEmail = user.Email
            };

            if (parent1 != null)
            {
                // Populate fields from existing Parent1 record if found
                viewModel.FatherAddress = parent1.FatherAddress;
                viewModel.FatherRace = parent1.FatherRace;
                viewModel.FatherOccupation = parent1.FatherOccupation;
                viewModel.FatherWorkAddress = parent1.FatherWorkAddress;
                viewModel.FatherIc = parent1.FatherIc;

                // Try to fetch Parent2 details
                var parent2 = await _context.tb_parent2.FirstOrDefaultAsync(p => p.FatherIc == parent1.FatherIc);

                if (parent2 != null)
                {
                    // Populate fields from existing Parent2 record if found
                    viewModel.MotherName = parent2.MotherName;
                    viewModel.MotherPhone = parent2.MotherPhone;
                    viewModel.MotherAddress = parent2.MotherAddress;
                    viewModel.MotherRace = parent2.MotherRace;
                    viewModel.MotherOccupation = parent2.MotherOccupation;
                    viewModel.MotherEmail = parent2.MotherEmail;
                    viewModel.MotherWorkAddress = parent2.MotherWorkAddress;
                    viewModel.MotherIc = parent2.MotherIc;
                }
            }

            return View(viewModel);
        }


        // POST: Parent1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound(); // Handle case where user is not found
                }

                // Create and save Parent1 (Father) details
                var parent1 = new Parent1
                {
                    FatherName = viewModel.FatherName,
                    FatherPhone = viewModel.FatherPhone,
                    FatherAddress = viewModel.FatherAddress,
                    FatherRace = viewModel.FatherRace,
                    FatherOccupation = viewModel.FatherOccupation,
                    FatherEmail = viewModel.FatherEmail,
                    FatherWorkAddress = viewModel.FatherWorkAddress,
                    FatherIc = viewModel.FatherIc,
                    ApplicationUserId = user.Id
                    // Add other properties as needed
                };

                _context.Add(parent1);

                // Create and save Parent2 (Mother) details if provided
                if (!string.IsNullOrEmpty(viewModel.MotherName) || !string.IsNullOrEmpty(viewModel.MotherAddress) || 
                    !string.IsNullOrEmpty(viewModel.MotherRace) || !string.IsNullOrEmpty(viewModel.MotherOccupation) || 
                    !string.IsNullOrEmpty(viewModel.MotherEmail) || !string.IsNullOrEmpty(viewModel.MotherIc))
                {
                    var parent2 = new Parent2
                    {
                        MotherName = viewModel.MotherName,
                        MotherPhone = viewModel.MotherPhone,
                        MotherAddress = viewModel.MotherAddress,
                        MotherRace = viewModel.MotherRace,
                        MotherOccupation = viewModel.MotherOccupation,
                        MotherEmail = viewModel.MotherEmail,
                        MotherWorkAddress = viewModel.MotherWorkAddress,
                        MotherIc = viewModel.MotherIc,
                        FatherIc = viewModel.FatherIc // Assuming FatherIc is used to link Parent1 and Parent2
                                                      // Add other properties as needed
                    };

                    _context.Add(parent2);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Redirect to appropriate action or page
            }

            return View(viewModel);
        }

        // GET: Parent1/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            // Check if parent1 record exists for the current user
            var parent1 = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            if (parent1 == null)
            {
                // If parent1 record does not exist, redirect to Create action
                return RedirectToAction("Create");
            }

            var viewModel = new ParentViewModel
            {
                FatherName = parent1.FatherName,
                FatherPhone = parent1.FatherPhone,
                FatherAddress = parent1.FatherAddress,
                FatherRace = parent1.FatherRace,
                FatherOccupation = parent1.FatherOccupation,
                FatherEmail = parent1.FatherEmail,
                FatherWorkAddress = parent1.FatherWorkAddress,
                FatherIc = parent1.FatherIc
            };

            // Check if parent2 record exists based on FatherIc from parent1
            var parent2 = await _context.tb_parent2.FirstOrDefaultAsync(p => p.FatherIc == parent1.FatherIc);
            if (parent2 != null)
            {
                viewModel.MotherName = parent2.MotherName;
                viewModel.MotherPhone = parent2.MotherPhone;
                viewModel.MotherAddress = parent2.MotherAddress;
                viewModel.MotherRace = parent2.MotherRace;
                viewModel.MotherOccupation = parent2.MotherOccupation;
                viewModel.MotherEmail = parent2.MotherEmail;
                viewModel.MotherWorkAddress = parent2.MotherWorkAddress;
                viewModel.MotherIc = parent2.MotherIc;
            }

            return View(viewModel);
        }

        // POST: Parent1/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ParentViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            if (ModelState.IsValid)
            {
                var parent1 = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
                if (parent1 == null)
                {
                    // If parent1 record does not exist, redirect to Create action
                    return RedirectToAction("Create");
                }

                parent1.FatherName = viewModel.FatherName;
                parent1.FatherPhone = viewModel.FatherPhone;
                parent1.FatherAddress = viewModel.FatherAddress;
                parent1.FatherRace = viewModel.FatherRace;
                parent1.FatherOccupation = viewModel.FatherOccupation;
                parent1.FatherEmail = viewModel.FatherEmail;
                parent1.FatherWorkAddress = viewModel.FatherWorkAddress;
                parent1.FatherIc = viewModel.FatherIc;

                _context.Update(parent1);

                // Check if parent2 record exists based on FatherIc from parent1
                var parent2 = await _context.tb_parent2.FirstOrDefaultAsync(p => p.FatherIc == parent1.FatherIc);
                if (parent2 != null)
                {
                    parent2.MotherName = viewModel.MotherName;
                    parent2.MotherPhone = viewModel.MotherPhone;
                    parent2.MotherAddress = viewModel.MotherAddress;
                    parent2.MotherRace = viewModel.MotherRace;
                    parent2.MotherOccupation = viewModel.MotherOccupation;
                    parent2.MotherEmail = viewModel.MotherEmail;
                    parent2.MotherWorkAddress = viewModel.MotherWorkAddress;
                    parent2.MotherIc = viewModel.MotherIc;

                    _context.Update(parent2);
                }
                else if (!string.IsNullOrEmpty(viewModel.MotherName) || !string.IsNullOrEmpty(viewModel.MotherAddress) ||
                    !string.IsNullOrEmpty(viewModel.MotherRace) || !string.IsNullOrEmpty(viewModel.MotherOccupation) ||
                    !string.IsNullOrEmpty(viewModel.MotherEmail) || !string.IsNullOrEmpty(viewModel.MotherIc))
                {
                    parent2 = new Parent2
                    {
                        MotherName = viewModel.MotherName,
                        MotherPhone = viewModel.MotherPhone,
                        MotherAddress = viewModel.MotherAddress,
                        MotherRace = viewModel.MotherRace,
                        MotherOccupation = viewModel.MotherOccupation,
                        MotherEmail = viewModel.MotherEmail,
                        MotherWorkAddress = viewModel.MotherWorkAddress,
                        MotherIc = viewModel.MotherIc,
                        FatherIc = viewModel.FatherIc
                    };

                    _context.Add(parent2);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Redirect to appropriate action or page
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var parent1 = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            if (parent1 == null)
            {
                return NotFound(); // Handle case where Parent1 record is not found
            }

            var viewModel = new ParentViewModel
            {
                FatherName = parent1.FatherName,
                FatherPhone = parent1.FatherPhone,
                FatherAddress = parent1.FatherAddress,
                FatherRace = parent1.FatherRace,
                FatherOccupation = parent1.FatherOccupation,
                FatherEmail = parent1.FatherEmail,
                FatherWorkAddress = parent1.FatherWorkAddress,
                FatherIc = parent1.FatherIc
            };

            var parent2 = await _context.tb_parent2.FirstOrDefaultAsync(p => p.FatherIc == parent1.FatherIc);
            if (parent2 != null)
            {
                viewModel.MotherName = parent2.MotherName;
                viewModel.MotherPhone = parent2.MotherPhone;
                viewModel.MotherAddress = parent2.MotherAddress;
                viewModel.MotherRace = parent2.MotherRace;
                viewModel.MotherOccupation = parent2.MotherOccupation;
                viewModel.MotherEmail = parent2.MotherEmail;
                viewModel.MotherWorkAddress = parent2.MotherWorkAddress;
                viewModel.MotherIc = parent2.MotherIc;
            }

            return View(viewModel);
        }

    }
}
