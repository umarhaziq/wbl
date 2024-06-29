using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using wbl.Data;
using wbl.Models;

namespace wbl.Controllers
{
    public class ChildInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChildInvoiceController> _logger;

        public ChildInvoiceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ChildInvoiceController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var parent = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);

            

            var invoices = await _context.Invoicess
                .Include(i => i.Child)
                .ThenInclude(c => c.Programs)
                .Where(i => i.Child.FatherIc == parent.FatherIc)
                .ToListAsync();

            return View(invoices);
        }



        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoicess.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Upload/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("Attachment", "Please select a file.");
                var invoiceWithError = await _context.Invoicess.FindAsync(id);
                return View(invoiceWithError);
            }

            var invoice = await _context.Invoicess.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                invoice.Receipt = memoryStream.ToArray();
                invoice.ReceiptContentType = file.ContentType; // Save the content type
            }

            _context.Update(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(int id)
        {
            var invoice = await _context.Invoicess.FindAsync(id);
            if (invoice == null || invoice.Attachment == null)
            {
                return NotFound();
            }

            var fileName = $"Invoice_{invoice.InvoiceName}_Attachment.pdf";
            return File(invoice.Attachment, "application/pdf", fileName);
        }

        public async Task<IActionResult> ViewPDF(int id)
        {
            var invoice = await _context.Invoicess.FindAsync(id);
            if (invoice == null || invoice.Attachment == null)
            {
                return NotFound();
            }

            return File(invoice.Attachment, "application/pdf");
        }


        public async Task<IActionResult> ViewReceipt(int id)
        {
            var invoice = await _context.Invoicess.FindAsync(id);
            if (invoice == null || invoice.Receipt == null)
            {
                return NotFound();
            }

            return File(invoice.Receipt, "application/pdf");
        }
        private bool InvoiceExists(int id)
        {
            return _context.Invoicess.Any(e => e.Id == id);
        }

    }
}
