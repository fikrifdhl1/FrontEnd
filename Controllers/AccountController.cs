using FrontEnd.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _services;

        public AccountController(IAccountService services)
        {
            _services = services;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await _services.LoginAsync(username, password);
            if(token != null)
            {
                HttpContext.Session.SetString("sessionLogin-username", username);
                HttpContext.Session.SetString("sessionLogin-token", token.Data.Token);
                HttpContext.Session.SetString("sessionLogin-userid", JwtDecoder.GetJwtClaim(token.Data.Token,ClaimTypes.Sid));
                return RedirectToAction("Index", "Product");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }
}
