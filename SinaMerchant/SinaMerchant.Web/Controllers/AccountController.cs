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
        private readonly IPasswordHelper _passwordHelper;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(IGenericService<User, RegisterViewModel> genericService, IPasswordHelper passwordHelper,
            IConfiguration configuration, IMapper mapper)
        {
            _genericService = genericService;
            _passwordHelper = passwordHelper;
            _configuration = configuration;
            _mapper = mapper;
        }



        #region Register
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            // add user to database
            register.Email = register.Email.ToLower().Trim();
            register.Password = _passwordHelper.HashPassword(register.Password);
            register.EmailActiveCode = Guid.NewGuid().ToString("N");
            var success = await _genericService.InsertAsync(register);
            if (success) TempData["SuccessMessage"] = "Succesfully Added.";

            // send email active code
            var domainName = _configuration.GetValue<string>("DomainName");
            string message = $"We created an account for you. Please confirm your email address and get ready to start shopping. <a class=\"btn btn-primary\" role=\"button\" href=\"{domainName}/activate-account/{register.EmailActiveCode}\">ConfirmMyEmail</a>";
            EmailSender.SendEmail(register.Email, "Confirm your email", message);
            return Redirect("/");
        }

        // check user is exists or not
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _genericService.IsExist(x => x.Email == email.ToLower().Trim()))
            {
                return Json($"A user with email {email} has already registered");
            }
            return Json(true);
        }

        [HttpGet("activate-account/{emailActiveCode}")]
        public async Task<IActionResult> ActivateAccount(string emailActiveCode)
        {
            var user = await _genericService.GetSingleAsync(u => u.EmailActiveCode == emailActiveCode);
            if (user != null)
            {
                user.IsActive = true;
                user.EmailActiveCode = Guid.NewGuid().ToString("N");
                var userViewModel = _mapper.Map<RegisterViewModel>(user);
                var success = await _genericService.Edit(userViewModel);
                if (success) return RedirectToAction("Login");

            }

            // block ip
            return NotFound();
        }
        #endregion

        #region Login
        [HttpGet("login")]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.referer = ReturnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel login, string? referer)
        {
            if (ModelState.IsValid)
            {
                login.Password = _passwordHelper.HashPassword(login.Password);
                login.Email = login.Email.ToLower().Trim();
                var user = await _genericService.GetSingleAsync(user => user.Email == login.Email && user.Password == login.Password);
                if (user != null)
                {
                    if (user.IsActive)
                    {


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

                        if (!string.IsNullOrEmpty(referer) && Url.IsLocalUrl(referer))
                        {
                            return Redirect(referer);
                        }

                        if (user.IsAdmin) return RedirectToAction("Index", "Home", new { area = "Admin" });

                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Your account is not active yet!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email or password is not correct!");
                }
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login");
        }
        #endregion


    }
}
