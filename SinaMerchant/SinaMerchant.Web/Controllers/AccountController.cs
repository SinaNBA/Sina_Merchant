using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;
using System.Security.Claims;

namespace SinaMerchant.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor
        private readonly IUserService<RegisterViewModel> _userService;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService<RegisterViewModel> userService, IPasswordHelper passwordHelper,
            IConfiguration configuration)
        {
            _userService = userService;
            _passwordHelper = passwordHelper;
            _configuration = configuration;
        }
        #endregion

        #region Register
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Email, Password,ConfirmPassword")] RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            // add user to database
            register.Email = register.Email.ToLower().Trim();
            register.Password = _passwordHelper.HashPassword(register.Password);
            register.EmailActiveCode = Guid.NewGuid().ToString("N");
            register.RegisterDate = DateTime.Now;
            _userService.Insert(register);


            // send email active code
            var domainName = _configuration.GetValue<string>("DomainName");
            string message = $"We created an account for you. Please confirm your email address and get ready to start shopping. <a class=\"btn btn-primary\" role=\"button\" href=\"{domainName}/activate-account/{register.EmailActiveCode}\">ConfirmMyEmail</a>";
            var sendMail = new SendEmailViewModel()
            {
                Email = register.Email,
                EmailActiveCode = register.EmailActiveCode,
                Subject = "Confirm your email",
                Message = message
            };
            return RedirectToAction("SendEmail", sendMail);
        }

        // check user is exists or not, through remote attribute in RegisterViewModel
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _userService.IsExist(x => x.Email == email.ToLower().Trim()))
            {
                return Json($"A user with email {email} has already registered");
            }
            return Json(true);
        }

        // send email active code  
        public IActionResult SendEmail(SendEmailViewModel sendEmail)
        {
            EmailSender.SendEmail(sendEmail.Email, sendEmail.Subject, sendEmail.Message);
            return View(sendEmail);
        }

        [HttpGet("activate-account/{emailActiveCode}")]
        public async Task<IActionResult> ActivateAccount(string emailActiveCode)
        {
            var user = await _userService.GetSingleNoTracking(u => u.EmailActiveCode == emailActiveCode);
            if (user != null && !user.IsActive)
            {
                user.IsActive = true;
                user.EmailActiveCode = Guid.NewGuid().ToString("N");
                var success = _userService.Update(user);
                if (success)
                {
                    TempData["SuccessMessage"] = "Your account has been successfully activated!";
                    return RedirectToAction("Login");
                }
            }

            // block ip
            return NotFound();
        }
        #endregion

        #region Login
        [HttpGet("login")]
        public IActionResult Login(string? ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            if (ReturnUrl != null)
            {
                ViewBag.referer = ReturnUrl;
            }

            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string? referer)
        {
            if (ModelState.IsValid)
            {
                login.Password = _passwordHelper.HashPassword(login.Password);
                login.Email = login.Email.ToLower().Trim();
                var user = await _userService.GetSingleNoTracking(user => user.Email == login.Email && user.Password == login.Password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        //var userRole = _userService.EntitiesSet.Include(u=> u.UserRoles.Where(x=>x.UserId==user.Id)).ThenInclude(u=>u.Role).ToList();

                        // set user claims
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Email),
                        };
                        // Identity
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // generate current user
                        var principal = new ClaimsPrincipal(identity);

                        // sign in user
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);

                        if (!string.IsNullOrEmpty(referer) && Url.IsLocalUrl(referer))
                        {
                            return Redirect(referer);
                        }

                        //if (user.IsAdmin) return RedirectToAction("Index", "Home", new { area = "Admin" });

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

        #region Forgot Password
        [HttpGet("forgot-pass")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost("forgot-pass")]
        public async Task<IActionResult> ForgotPassword(ForgotPassModelView forgotPass)
        {
            if (ModelState.IsValid)
            {


                var user = await _userService.GetSingleNoTracking(u => u.Email == forgotPass.Email.Trim().ToLower());
                if (user != null)
                {
                    var domainName = _configuration.GetValue<string>("DomainName");
                    string message = $"Hi {user.Email}. Forgot your password? No worries, we’ve got you covered. Click the link to reset your password. <a class=\"btn btn-primary\" role=\"button\" href=\"{domainName}/reset-pass/{user.EmailActiveCode}\">ResetPassword</a>";
                    var sendEmail = new SendEmailViewModel()
                    {
                        Email = user.Email,
                        EmailActiveCode = user.EmailActiveCode,
                        Subject = "Reset your password",
                        Message = message
                    };

                    return RedirectToAction("SendEmail", sendEmail);
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is not correct!");
                }
            }
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet("reset-pass/{emailActiveCode}")]
        public async Task<IActionResult> ResetPassword(string emailActiveCode)
        {
            var user = await _userService.GetSingleNoTracking(u => u.EmailActiveCode == emailActiveCode);

            if (user == null) return NotFound();

            return View();
        }

        [HttpPost("reset-pass /{emailActiveCode}")]
        public async Task<IActionResult> ResetPassword(string emailActiveCode, ResetPassViewModel resetPass)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetSingleNoTracking(u => u.EmailActiveCode == emailActiveCode);
                if (user == null) return NotFound();

                user.Password = _passwordHelper.HashPassword(resetPass.Password);
                user.EmailActiveCode = Guid.NewGuid().ToString("N");
                user.IsActive = true;

                var success = _userService.Update(user);
                if (success)
                {
                    TempData["SuccessMessage"] = "Reset successfully.";
                    return RedirectToAction("Login");
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
