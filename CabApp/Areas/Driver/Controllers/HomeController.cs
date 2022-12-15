
using Microsoft.AspNetCore.Authorization;

namespace CabApp.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles = "Driver")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _db.Bookings.ToListAsync());
        }

        [HttpGet]
        public IActionResult DriverRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DriverRegister(CabDriverViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new CabDriver()
            {
                LicenseNumber= model.LicenseNumber,
                CabName= model.CabName,
                CabNumber= model.CabNumber,
                CabType= model.CabType,
                DriverId = id
            };
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Login","Home",new {Area = "Account"});
        }

        public async Task<IActionResult> ConfirmBooking(int id) 
        {
            var booking = await _db.Bookings.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var driver = await _db.CabDrivers.FirstOrDefaultAsync(x => x.DriverId == user.Id);
            booking.DriverId = driver.Id;
            return View();
        }
    }
}
