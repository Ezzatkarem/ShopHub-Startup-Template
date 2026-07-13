using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using myshop.Entities.Models;

namespace myshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vM)
        {
            if (!ModelState.IsValid)
                return View(vM);
            var res=await userService.Registr(vM);
            if (!res)
            {
                ModelState.AddModelError("", "Email already exists.");
                return View(vM);
            }
            TempData["Success"] = "A confirmation email has been sent. Please check your inbox.";

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await userService.LoginAsync(vm);

            if (!result)
            {
                ModelState.AddModelError("", "Please confirm your email before logging in.");
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await userService.LogOUtAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return RedirectToAction("Login");
            return BadRequest();
        }

    }
}
