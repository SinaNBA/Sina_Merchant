using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;
using System.Security.Claims;

namespace SinaMerchant.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericService<User, RegisterViewModel> _genericService;
        private readonly IMapper _mapper;

        public AccountController(IGenericService<User, RegisterViewModel> genericService, IMapper mapper)
        {
            _genericService = genericService;
            _mapper = mapper;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            //var IsExist = await _genericService.IsExist(x => x.Email == register.Email.ToLower().Trim());
            //if (IsExist)
            //{
            //    ModelState.AddModelError("Email", "This email has registered before!");
            //    return View(register);
            //}
            var success = await _genericService.InsertAsync(register);
            if (success) TempData["SuccessMessage"] = "Succesfully Added.";
            return Redirect("/");
        }

        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _genericService.IsExist(x => x.Email == email.ToLower().Trim()))
            {
                return Json($"A user with email {email} has already registered");
            }
            return Json(true);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _genericService.GetSingleAsync(user => user.Email == login.Email && user.Password == login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email or password is not correct");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            await HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
        #endregion


    }
}
