using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Managers;
using MoneyFlow.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyFlow.Controllers
{
    [AllowAnonymous]
    public class AccountController(UserManager _userManager) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginVM();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var found = _userManager.Login(viewModel);
            if(found == null || found.UserId == 0)
            {
                ViewBag.message = "Invalid email or password.";
                return View(viewModel);
            }
            else
            {
                // Set user session or authentication cookie here
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, found.UserId.ToString()),
                    new Claim(ClaimTypes.Name, found.FullName),
                    new Claim(ClaimTypes.Email, found.Email)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties() { AllowRefresh = true}
                    );

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new UserVM
            {
                FullName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                RepeatPassword = string.Empty
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Register(UserVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var response = _userManager.Register(viewModel);

                if(response != 0)
                {
                    ViewBag.message = "Su cuenta ha sido registrada. Por favor proceda al login";
                    ViewBag.Class = "alert-success";
                }else
                {
                    ViewBag.message = "Ocurrió un error al registrar su cuenta. Por favor intente nuevamente.";
                    ViewBag.Class = "alert-danger";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = $"Ocurrió un error al registrar su cuenta: {ex.Message}";
                ViewBag.Class = "alert-danger";

            }
            return View();
        }
    }
}
