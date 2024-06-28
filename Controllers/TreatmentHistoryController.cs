using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;

namespace wbl.Controllers
{
    public class TreatmentHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TreatmentHistoryController> _logger;
        private readonly IEmailSender _emailSender; // Assuming you have an email sender service

        public TreatmentHistoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                               ILogger<TreatmentHistoryController> logger, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: TreatmentHistory/Index
        // GET: TreatmentHistory/Index
        // GET: TreatmentHistory/Index?childId=1
        public async Task<IActionResult> Index(int? ChildId)
        {
            var treatmentHistories = await _context.TreatmentHistories
                .Include(th => th.Child) // Include related child information if needed
                .Where(th => th.ChildId == ChildId)
                .ToListAsync();

            ViewBag.ChildId = ChildId;

            return View(treatmentHistories); // Pass the filtered list to the view
        }


        // GET: TreatmentHistory/Create
        public async Task<IActionResult> Create(int ChildId)
        {
            var viewModel = new TreatmentHistoryViewModel
            {
                ChildId = ChildId
            };

            return View(viewModel);
        }

        // POST: TreatmentHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TreatmentHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the ChildId exists in the tb_child table
                var childExists = await _context.tb_child.AnyAsync(c => c.ChildId == model.ChildId);

                if (!childExists)
                {
                    ModelState.AddModelError("", "The specified child does not exist.");
                    return View(model);
                }

                var treatmentHistory = new TreatmentHistory
                {
                    ChildId = model.ChildId,
                    pediatricians = model.Pediatricians,
                    recHospClinic = model.RecHospClinic,
                    diagnoseDesc = model.DiagnoseDesc,
                    deadlineDignose = model.DeadlineDiagnose
                };

                try
                {
                    _context.Add(treatmentHistory);
                    await _context.SaveChangesAsync();

                    // Redirect to the manage treatment history view or another appropriate action
                    return RedirectToAction("TreatmentHistory", new { ChildId = model.ChildId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the treatment history. Please try again.");
                }
            }

            return View(model);
        }

        // GET: TreatmentHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var treatmentHistory = await _context.TreatmentHistories
                .Include(t => t.Child)
                .FirstOrDefaultAsync(m => m.historyId == id);

            if (treatmentHistory == null)
            {
                return NotFound();
            }

            return View(treatmentHistory);
        }

        // GET: TreatmentHistory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var treatmentHistory = await _context.TreatmentHistories.FindAsync(id);
            if (treatmentHistory == null)
            {
                return NotFound();
            }

            var viewModel = new TreatmentHistoryViewModel
            {
                historyId = treatmentHistory.historyId,
                ChildId = treatmentHistory.ChildId,
                Pediatricians = treatmentHistory.pediatricians,
                RecHospClinic = treatmentHistory.recHospClinic,
                DiagnoseDesc = treatmentHistory.diagnoseDesc,
                DeadlineDiagnose = treatmentHistory.deadlineDignose
            };

            return View(viewModel);
        }

        // POST: TreatmentHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TreatmentHistoryViewModel model)
        {
            if (id != model.historyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var treatmentHistory = await _context.TreatmentHistories.FindAsync(id);
                if (treatmentHistory == null)
                {
                    return NotFound();
                }

                treatmentHistory.ChildId = model.ChildId;
                treatmentHistory.pediatricians = model.Pediatricians;
                treatmentHistory.recHospClinic = model.RecHospClinic;
                treatmentHistory.diagnoseDesc = model.DiagnoseDesc;
                treatmentHistory.deadlineDignose = model.DeadlineDiagnose;

                try
                {
                    _context.Update(treatmentHistory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { ChildId = model.ChildId });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "An error occurred while updating the treatment history. Please try again.");
                }
            }

            return View(model);
        }

        // GET: TreatmentHistory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var treatmentHistory = await _context.TreatmentHistories
                .Include(t => t.Child)
                .FirstOrDefaultAsync(m => m.historyId == id);

            if (treatmentHistory == null)
            {
                return NotFound();
            }

            return View(treatmentHistory);
        }

        // POST: TreatmentHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatmentHistory = await _context.TreatmentHistories.FindAsync(id);
            _context.TreatmentHistories.Remove(treatmentHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { ChildId = treatmentHistory.ChildId });
        }
    }
}
