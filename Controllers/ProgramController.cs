using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using wbl.Data;
using wbl.Models;
using wbl.Models.ViewModels;

namespace wbl.Controllers
{
    public class ProgramController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgramController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Program
        public async Task<IActionResult> Index()
        {
            return View(await _context.Set<Programs>().ToListAsync());
        }

        // GET: Program/Details/5
        // GET: Program/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.tb_Program
                .FirstOrDefaultAsync(m => m.ProgramId == id);

            if (program == null)
            {
                return NotFound();
            }

            // Fetch students enrolled in this program with status "Approved" in Child model
            var students = await _context.tb_child
                .Where(c => c.ProgramId == id && c.ApplicationStatus == "Approved")
                .ToListAsync();

            // You may want to include more details or customize the view model as per your needs
            var viewModel = new ProgramDetailsViewModel
            {
                Program = program,
                Students = students
            };

            return View(viewModel);
        }


        // GET: Program/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Program/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramId,ProgramName,Description,Program_Category,Price")] Programs program)
        {
            if (ModelState.IsValid)
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(program);
        }

        // GET: Program/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Set<Programs>().FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Program/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProgramId,ProgramName,Description,Program_Category,Price")] Programs program)
        {
            if (id != program.ProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.ProgramId))
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
            return View(program);
        }


        // GET: Program/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Set<Programs>()
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.Set<Programs>().FindAsync(id);
            _context.Set<Programs>().Remove(program);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramExists(int id)
        {
            return _context.Set<Programs>().Any(e => e.ProgramId == id);
        }
    }
}
