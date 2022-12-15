namespace CabApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Account not Found");
                return View(model);
            }
            var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (res.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(user, "Driver"))
                {
                    return RedirectToAction("Index", "Home", new { Area = "Driver" });
                }
                else if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Details", "Home", new { Area = "Admin" });
                }
                return RedirectToAction("Index", "Home", new { Area = "Account" });
            }
            ModelState.AddModelError("", "Error");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                UserName = model.FirstName + model.LastName
            };
            var role = Convert.ToString(model.Role);
            var res = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, role);

            if (res.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(user, "Driver"))
                {
                    return RedirectToAction("DriverRegister", "Home", new { Area = "Driver", id = user.Id });
                }
                return RedirectToAction(nameof(Login));
            }
            ModelState.AddModelError("", "Error");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new RegisterViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            await _userManager.UpdateAsync(user);
            return Redirect("/");
        }

        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            var bookingList = await _db.Bookings.ToListAsync();
            foreach (var item in bookingList)
            {
                if (item.UserId == user.Id)
                {
                    _db.Bookings.Remove(item);
                }
            }
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult BookNow()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookNow(BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Destination == model.PickUp)
            {
                ModelState.AddModelError(nameof(model.Destination), "Please provide a different Destination!.Pick Up and Destination locations are same");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            var booking = new BookingCab()
            {
                PickUp = model.PickUp,
                Destination = model.Destination,
                CabRideTime = model.CabRideTime,
                BookingTime = DateTime.Now,
                CabType = model.CabType,
                UserId = await _userManager.GetUserIdAsync(user)
            };
            await _db.AddAsync(booking);
            await _db.SaveChangesAsync();
            return RedirectToAction("Payment", new { id = booking.Id });
        }

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Payment(int id)
        {
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> GenerateData()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Driver" });
            var appUser = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "",
                Email = "admin@admin.com",
                PhoneNumber = "123456789",
                UserName = "admin",
            };
            var res = await _userManager.CreateAsync(appUser, "Admin@123");
            await _userManager.AddToRoleAsync(appUser, "Admin");
            return Ok("Data generated");
        }
    }
}
