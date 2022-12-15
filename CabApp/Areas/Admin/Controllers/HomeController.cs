using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public async Task<ActionResult> Details()
        {
            return View(await _db.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _db.Users.FindAsync(id);
            return View(new RegisterViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model, string id)
        {
            var user = await _db.Users.FindAsync(id);
            if (!ModelState.IsValid)
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _db.Users.FindAsync(id);
            var bookingList = await _db.Bookings.ToListAsync();
            foreach (var item in bookingList)
            {
                if (item.UserId == user.Id)
                {
                    _db.Bookings.Remove(item);
                }
            }
            var driver = await _db.CabDrivers.FirstOrDefaultAsync(m => m.DriverId == id);
            await  _userManager.DeleteAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }
    }
}
