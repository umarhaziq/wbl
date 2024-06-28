using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wbl.Data;
using wbl.Models;

namespace wbl.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AnnouncementController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

                    private bool AnnouncementExists(int id)
                    {
                        return _context.tb_Announcement.Any(e => e.Id == id);
                    }

        public IActionResult Index()
        {
            var announcements = _context.tb_Announcement.ToList(); // Replace _context with your ApplicationDbContext instance
            return View(announcements);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.tb_Announcement.FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Announcement obj, IFormFile? ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagePath.CopyTo(fileStream);
                    }
                    obj.ImagePath = "/Upload/Images/" + uniqueFileName;
                }

                _context.tb_Announcement.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Announcement created successfully!!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //public async Task<IActionResult> Create([Bind("Title, Content, ImagePath")] Announcement announcement, IFormFile imageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (imageFile != null && imageFile.Length > 0)
        //        {
        //            // Generate a unique file name
        //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Images", fileName);

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await imageFile.CopyToAsync(stream);
        //            }

        //            announcement.ImagePath = "Upload/Images/" + fileName;
        //        }

        //        _context.Add(announcement);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(announcement);
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _context.tb_Announcement.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost]

        [HttpPost]
        public IActionResult Edit(Announcement obj, IFormFile? ImagePath)
        {
            if (ModelState.IsValid)
            {
                var announcementFromDb = _context.tb_Announcement.AsNoTracking().FirstOrDefault(a => a.Id == obj.Id);
                if (announcementFromDb == null)
                {
                    return NotFound();
                }

                if (ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/Images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagePath.CopyTo(fileStream);
                    }
                    obj.ImagePath = "/Upload/Images/" + uniqueFileName;
                }
                else
                {
                    obj.ImagePath = announcementFromDb.ImagePath;
                }

                _context.tb_Announcement.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Announcement updated successfully!!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Content, ImagePath")] Announcement announcement, IFormFile imageFile)
        //{
        //    if (id != announcement.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (imageFile != null && imageFile.Length > 0)
        //            {
        //                // Delete existing image if any
        //                if (!string.IsNullOrEmpty(announcement.ImagePath))
        //                {
        //                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", announcement.ImagePath.TrimStart('/'));
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }
        //                }

        //                // Upload new image
        //                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        //                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Images", fileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await imageFile.CopyToAsync(stream);
        //                }

        //                announcement.ImagePath = "Upload/Images/" + fileName;
        //            }

        //            _context.Update(announcement);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AnnouncementExists(announcement.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(announcement);
        //}




        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _context.tb_Announcement.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.tb_Announcement.FindAsync(id);
            _context.tb_Announcement.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
