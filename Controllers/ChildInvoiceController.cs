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
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.GetUserAsync(User);

            var parent = await _context.tb_parent1.FirstOrDefaultAsync(p => p.ApplicationUserId == user.Id);


            var statuses = new List<string> { "Pending", "Approved", "Rejected" };
            var Pstatuses = new List<string> { "Paid", "Unpaid" };

            var children = await _context.tb_child
                .Include(c => c.Programs)
                .Where(c => c.FatherIc == parent.FatherIc)
                .ToListAsync();

            return View(children);
        }
        private object GetCurrentParentId()
        {
            throw new NotImplementedException();
        }

    }
}
