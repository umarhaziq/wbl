using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;

public class ChildController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ChildController> _logger;
    private readonly IEmailSender _emailSender; // Assuming you have an email sender service

    public ChildController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                           ILogger<ChildController> logger, IEmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<IActionResult> Index(string id)
    {
        var user = await _userManager.GetUserAsync(User);

        var parent = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
        if (parent == null)
        {
            TempData["Notification"] = "Please Register Your Child First";
            return RedirectToAction("Create","Child"); // Handle if parent is not found
        }

        var statuses = new List<string> { "Pending", "Approved", "Rejected" };
        var Pstatuses = new List<string> { "Paid", "Unpaid" };


        var children = await _context.tb_child
            .Include(c => c.Programs)
            .Where(c => c.FatherIc == parent.FatherIc && statuses.Contains(c.ApplicationStatus))
            .ToListAsync();

        return View(children);
    }

    private object GetCurrentParentId()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var child = await _context.tb_child
            .Include(c => c.Programs)
            .FirstOrDefaultAsync(m => m.ChildId == id);

        if (child == null)
        {
            return NotFound();
        }

        return View(child);
    }

    public async Task<IActionResult> Create()
    {
        var programsList = await _context.tb_Program
            .Select(p => new ChildViewModel.CustomSelectListItem
            {
                Value = p.ProgramId.ToString(),
                Text = p.ProgramName,
                Program_Category = p.Program_Category,
                Price = p.Price
            })
            .ToListAsync();

        var viewModel = new ChildViewModel
        {
            ProgramsList = programsList
        };

        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var parent1 = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);
            if (parent1 != null)
            {
                viewModel.FatherIc = parent1.FatherIc;
            }
        }

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ChildViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var child = new Child
            {
                Child_IC = viewModel.Child_IC,
                Child_Name = viewModel.Child_Name,
                Child_Birthplace = viewModel.Child_Birthplace,
                Child_DOB = viewModel.Child_DOB,
                Child_Gender = viewModel.Child_Gender,
                Child_Nationality = viewModel.Child_Nationality,
                Child_Race = viewModel.Child_Race,
                Child_Religion = viewModel.Child_Religion,
                ProgramId = viewModel.ProgramId,
                Child_Address = viewModel.Child_Address,
                FatherIc = viewModel.FatherIc,
                Program_Category = viewModel.Program_Category,
                ApplicationStatus = "Pending",
                PaymentStatus = "Paid"
            };

            _context.Add(child);
            await _context.SaveChangesAsync();

            // Notify admins about new child application
            await NotifyAdminsNewApplication(child);

            return RedirectToAction(nameof(Index));
        }

        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                var errorMessage = error.ErrorMessage;
                var exception = error.Exception;

                _logger.LogError(errorMessage);
                Debug.WriteLine(errorMessage);
            }
        }

        TempData["Notification"] = "Please fill in the parent form first.";

        viewModel.ProgramsList = await _context.tb_Program
            .Select(p => new SelectListItem
            {
                Value = p.ProgramId.ToString(),
                Text = p.ProgramName
            })
            .ToListAsync();

        return RedirectToAction("Edit", "Parent1", new { id = viewModel.FatherIc });
    }

    public async Task<IActionResult> Edit(int? id)
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

        var programsList = await _context.tb_Program
            .Select(p => new SelectListItem
            {
                Value = p.ProgramId.ToString(),
                Text = p.ProgramName
            })
            .ToListAsync();

        var viewModel = new ChildViewModel
        {
            ChildId = child.ChildId, // Include ChildId to edit
            Child_IC = child.Child_IC,
            Child_Name = child.Child_Name,
            Child_Birthplace = child.Child_Birthplace,
            Child_DOB = child.Child_DOB,
            Child_Gender = child.Child_Gender,
            Child_Nationality = child.Child_Nationality,
            Child_Race = child.Child_Race,
            Child_Religion = child.Child_Religion,
            ProgramId = child.ProgramId,
            Child_Address = child.Child_Address,
            FatherIc = child.FatherIc,
            Program_Category = child.Program_Category,
            ProgramsList = programsList
        };

        return View(viewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ChildViewModel viewModel)
    {

        if (ModelState.IsValid)
        {
            var child = await _context.tb_child.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            child.Child_IC = viewModel.Child_IC;
            child.Child_Name = viewModel.Child_Name;
            child.Child_Birthplace = viewModel.Child_Birthplace;
            child.Child_DOB = viewModel.Child_DOB;
            child.Child_Gender = viewModel.Child_Gender;
            child.Child_Nationality = viewModel.Child_Nationality;
            child.Child_Race = viewModel.Child_Race;
            child.Child_Religion = viewModel.Child_Religion;
            child.ProgramId = viewModel.ProgramId;
            child.Child_Address = viewModel.Child_Address;
            child.Program_Category = viewModel.Program_Category;
            child.FatherIc = viewModel.FatherIc;

            _context.Update(child);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        viewModel.ProgramsList = await _context.tb_Program
            .Select(p => new SelectListItem
            {
                Value = p.ProgramId.ToString(),
                Text = p.ProgramName
            })
            .ToListAsync();

        return View(viewModel);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var child = await _context.tb_child
            .Include(c => c.Programs)
            .FirstOrDefaultAsync(m => m.Child_IC == id);
        if (child == null)
        {
            return NotFound();
        }

        return View(child);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string child_Ic)
    {
        var child = await _context.tb_child.FindAsync(child_Ic);
        if (child == null)
        {
            return NotFound();
        }

        _context.tb_child.Remove(child);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    private async Task NotifyAdminsNewApplication(Child child)
    {
        var admins = await _userManager.GetUsersInRoleAsync("Admin");

        foreach (var admin in admins)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Your Application", "your-email@example.com"));
            email.To.Add(new MailboxAddress(admin.FullName, admin.Email));
            email.Subject = "New Child Application Submitted";

            var builder = new BodyBuilder();
            builder.TextBody = $"A new child application ({child.Child_Name}) has been submitted and requires your attention.";
            email.Body = builder.ToMessageBody();

            // Assuming _emailSender is an instance of a service or class that implements IEmailSender
            await _emailSender.SendEmailAsync(admin.Email, email.Subject, builder.TextBody);
        }
    }

    public IActionResult SelectDay()
    {
        //List<Availibility> objAvailibilityList = _context.Availabilities.ToList();
        var availableDates = _context.Availabilities.ToList();
        return View(availableDates);
        //return View(objAvailibilityList);
    }

    [HttpPost]
    public IActionResult SelectDay(int patientId, DateTime selectedDate)
    {
        var availability = _context.Availabilities.FirstOrDefault(a => a.AvailableDate == selectedDate);
        if (availability != null)
        {
            // Redirect to Staff for session registration
            return RedirectToAction("RegisterSession", "Staffs", new { patientId, selectedDate });
        }

        return RedirectToAction(nameof(SelectDay));
    }
    public IActionResult GetProgramsByCategory(string category)
    {
        var programs = _context.tb_Program
            .Where(p => p.Program_Category == category)
            .Select(p => new ChildViewModel.CustomSelectListItem
            {
                Value = p.ProgramId.ToString(),
                Text = p.ProgramName
            })
            .ToList();

        return Json(programs);
    }



}
