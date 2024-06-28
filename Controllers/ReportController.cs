using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;
using wbl.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace wbl.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reports
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var reports = from r in _context.Reports.Include(r => r.Session).ThenInclude(s => s.Child).Include(r => r.Session).ThenInclude(s => s.Therapist)
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(r => r.Title.Contains(searchString) || r.Session.Child.Child_Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    reports = reports.OrderByDescending(r => r.Date);
                    break;
                case "Date":
                    reports = reports.OrderBy(r => r.Date);
                    break;
                default:
                    reports = reports.OrderBy(r => r.Title);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Report>.CreateAsync(reports.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Parent Reports
        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> ParentIndex(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            var fatherIc = user.Id; // Assuming the parent's FatherIc is stored in the user entity

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var reports = from r in _context.Reports
                          .Include(r => r.Session)
                          .ThenInclude(s => s.Child)
                          .Include(r => r.Session)
                          .ThenInclude(s => s.Therapist)
                          where r.Session.Child.FatherIc == fatherIc
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(r => r.Title.Contains(searchString) || r.Session.Child.Child_Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    reports = reports.OrderByDescending(r => r.Date);
                    break;
                case "Date":
                    reports = reports.OrderBy(r => r.Date);
                    break;
                default:
                    reports = reports.OrderBy(r => r.Title);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Report>.CreateAsync(reports.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Session)
                .ThenInclude(s => s.Child)
                .Include(r => r.Session)
                .ThenInclude(s => s.Therapist)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create(long sessionId)
        {
            ViewData["SessionId"] = sessionId;
            var session = _context.Sessions.Include(s => s.Child).FirstOrDefault(s => s.SessionId == sessionId);
            ViewData["ChildName"] = session?.Child.Child_Name;
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,SessionId,Title,Content")] Report report)
        {
            if (ModelState.IsValid)
            {
                report.Date = DateTime.Now;
                report.ReportType = ReportType.AfterSession; // Hardcode ReportType to "After Session"
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var session = _context.Sessions.Include(s => s.Child).FirstOrDefault(s => s.SessionId == report.SessionId);
            ViewData["ChildName"] = session?.Child.Child_Name;
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Session)
                .ThenInclude(s => s.Child)
                .FirstOrDefaultAsync(m => m.ReportId == id);

            if (report == null)
            {
                return NotFound();
            }

            ViewBag.Child_IC = new SelectList(_context.tb_child, "Child_IC", "Child_Name", report.Session.Child.Child_IC);
            ViewData["ChildName"] = report.Session?.Child.Child_Name;
            return View(report);
        }

        // POST: Reports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,SessionId,Title,Content,Date,ReportType")] Report report)
        {
            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to Index after save
            }
            var session = _context.Sessions.Include(s => s.Child).FirstOrDefault(s => s.SessionId == report.SessionId);
            ViewData["ChildName"] = session?.Child.Child_Name;
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Session)
                .ThenInclude(s => s.Child)
                .Include(r => r.Session)
                .ThenInclude(s => s.Therapist)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }

        // GET: Reports/DownloadPdf/5
        public async Task<IActionResult> DownloadPdf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Session)
                .ThenInclude(s => s.Child)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("Details", report);
        }
    }
}
