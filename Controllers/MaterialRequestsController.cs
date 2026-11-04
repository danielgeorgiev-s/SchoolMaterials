using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMaterials.Data;
using SchoolMaterials.Models;

namespace SchoolMaterials.Controllers
{
    [Authorize]
    public class MaterialRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MaterialRequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var requests = isAdmin 
                ? await _context.MaterialRequests.Include(m => m.User).ToListAsync()
                : await _context.MaterialRequests.Where(m => m.UserId == user.Id).ToListAsync();

            return View(requests);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialRequest request)
        {
            if (ModelState.IsValid)
            {
                request.UserId = _userManager.GetUserId(User);
                request.Status = "Pending";
                request.Date = DateTime.Now;
                request.RequestNumber = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var request = await _context.MaterialRequests.FindAsync(id);
            if (request != null)
            {
                request.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}