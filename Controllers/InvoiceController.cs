using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wbl.Data;
using wbl.Models;

namespace wbl.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {

            var invoices = await _context.Invoicess
                .Include(i => i.Child)
                .ThenInclude(c => c.Programs)
                .ToListAsync();

            return View(await _context.Invoicess.ToListAsync());


        }

        // GET: Invoice/Details/5
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

        // GET: Invoice/Create


        public async Task<IActionResult> Create()
        {
            var approvedChildren = await _context.tb_child
                .Where(c => c.ApplicationStatus == "Approved")
                .Select(c => new {
                    c.ChildId,
                    ChildName = c.Child_Name + " (" + c.Child_IC + ")"
                })
                .ToListAsync();


            ViewBag.Children = new SelectList(approvedChildren, "ChildId", "ChildName");

            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildId,Date,InvoiceName,Attachment")] Invoices invoice)
        {


            if (ModelState.IsValid)
            {
                var child = await _context.tb_child
                    .Include(c => c.Parent)
                    .Include(c => c.Programs)
                    .FirstOrDefaultAsync(c => c.ChildId == invoice.ChildId);

                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,Value,Attachment")] Invoices invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoicess
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoicess.FindAsync(id);
            _context.Invoicess.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
                invoice.Attachment = memoryStream.ToArray();
                invoice.AttachmentContentType = file.ContentType; // Save the content type
            }

            _context.Update(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
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

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ChildPstatus = await _context.tb_child.FindAsync(id);
            if (ChildPstatus == null)
            {
                return NotFound();
            }

            ChildPstatus.PaymentStatus = ChildPstatus.PaymentStatus == "Paid" ? "Unpaid" : "Paid";
            _context.Update(ChildPstatus);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        private bool InvoiceExists(int id)
        {
            return _context.Invoicess.Any(e => e.Id == id);
        }
    }
}