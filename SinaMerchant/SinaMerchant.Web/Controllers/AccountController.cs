﻿using AutoMapper;
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
        private readonly IGenericService<User, RegisterViewModel> _userService;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(IGenericService<User, RegisterViewModel> userService, IPasswordHelper passwordHelper,
            IConfiguration configuration, IMapper mapper)
        {
            _userService = userService;
            _passwordHelper = passwordHelper;
            _configuration = configuration;
            _mapper = mapper;
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
        public async Task<IActionResult> Register([Bind("Email, Password,ConfirmPassword")] RegisterViewModel register)
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
            await _userService.InsertAsync(register);


            // send email active code
            return RedirectToAction("SendActivationEmail", new { register.Email, register.EmailActiveCode });
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
        public IActionResult SendActivationEmail(string email, string emailActiveCode)
        {
            var domainName = _configuration.GetValue<string>("DomainName");
            string message = $"We created an account for you. Please confirm your email address and get ready to start shopping. <a class=\"btn btn-primary\" role=\"button\" href=\"{domainName}/activate-account/{emailActiveCode}\">ConfirmMyEmail</a>";

            var sendActivationMail = new SendActivationEmailViewModel()
            {
                Email = email,
                EmailActiveCode = emailActiveCode,
                Subject = "Confirm your email",
                Message = message
            };

            EmailSender.SendEmail(sendActivationMail.Email, sendActivationMail.Subject, sendActivationMail.Message);
            return View(sendActivationMail);
        }

        [HttpGet("activate-account/{emailActiveCode}")]
        public IActionResult ActivateAccount(string emailActiveCode)
        {
            var user = _userService.GetSingle(u => u.EmailActiveCode == emailActiveCode, true);
            if (user != null && !user.IsActive)
            {
                user.IsActive = true;
                user.EmailActiveCode = Guid.NewGuid().ToString("N");
                var success = _userService.Edit(user);
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

            ViewBag.referer = ReturnUrl;

            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string? referer)
        {
            if (ModelState.IsValid)
            {
                login.Password = _passwordHelper.HashPassword(login.Password);
                login.Email = login.Email.ToLower().Trim();
                var user = await _userService.GetSingleAsync(user => user.Email == login.Email && user.Password == login.Password);
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


                var user = await _userService.Entities.AsQueryable().SingleOrDefaultAsync(u => u.Email == forgotPass.Email.Trim().ToLower());
                if (user != null)
                {
                    var domainName = _configuration.GetValue<string>("DomainName");
                    string message = $"Hi {user.Email}. Forgot your password? No worries, we’ve got you covered. Click the link to reset your password. <a class=\"btn btn-primary\" role=\"button\" href=\"{domainName}/reset-pass/{user.EmailActiveCode}\">ResetPassword</a>";
                    var sendEmail = new SendActivationEmailViewModel()
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
            var user = await _userService.Entities.AsQueryable().SingleOrDefaultAsync(u => u.EmailActiveCode == emailActiveCode);

            if (user == null) return NotFound();

            return View();
        }

        [HttpPost("reset-pass /{emailActiveCode}")]
        public async Task<IActionResult> ResetPassword(string emailActiveCode, ResetPassViewModel resetPass)
        {
            if (ModelState.IsValid)
            {
                //var user = _userService.Entities.AsQueryable().SingleOrDefault(u => u.EmailActiveCode == emailActiveCode);
                var user = _userService.GetSingle(u => u.EmailActiveCode == emailActiveCode, true);
                if (user == null) return NotFound();

                user.Password = _passwordHelper.HashPassword(resetPass.Password);
                user.EmailActiveCode = Guid.NewGuid().ToString("N");
                user.IsActive = true;
                //var userViewModel = _mapper.Map<RegisterViewModel>(user);
                var success = _userService.Edit(user);
                TempData["SuccessMessage"] = "Reset successfully.";
                if (success) return RedirectToAction("Login");

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
