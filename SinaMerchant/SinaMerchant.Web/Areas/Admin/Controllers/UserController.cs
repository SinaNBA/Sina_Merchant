using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Constructor
        private readonly IGenericService<User, UserAddEditViewModel> _userService;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;

        public UserController(IGenericService<User, UserAddEditViewModel> userService,
            IPasswordHelper passwordHelper, IMapper mapper)
        {
            _userService = userService;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
        }
        #endregion

        #region Get Users
        // GET: User        
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAll());
        }

        // GET: User/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var User = await _userService.GetById(id);
                if (User == null) return NotFound();
                return View(User);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region Create
        // GET: User/Create        
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAddEditViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            // add user to database
            user.Email = user.Email.ToLower().Trim();
            user.Password = _passwordHelper.HashPassword(user.Password);
            user.EmailActiveCode = Guid.NewGuid().ToString("N");
            var success = await _userService.InsertAsync(user);
            if (success) TempData["SuccessMessage"] = "Succesfully Added.";
            return RedirectToAction(nameof(Index));
        }

        // check user is exists or not
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _userService.IsExist(x => x.Email == email.ToLower().Trim()))
            {
                return Json($"A user with email {email} has already registered");
            }
            return Json(true);
        }
        #endregion

        #region Edit
        // GET: User/Edit/5        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var User = await _userService.GetById(id);
            // check user is exists or not
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: User/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserAddEditViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }
            //TODO Fix Edit Action for User
            var user = await _userService.Entities.AsQueryable().AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            userViewModel.EmailActiveCode = user.EmailActiveCode;
            userViewModel.Password = _passwordHelper.HashPassword(userViewModel.Password);

            if (ModelState.IsValid)
            {
                try
                {
                    var success = _userService.Edit(userViewModel);
                    if (success) TempData["SuccessMessage"] = "Succesfully Edited.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }
        #endregion

        #region Delete
        // GET: User/Delete/5        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _userService.GetById(id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SiteUsers' is null.");
            }

            var success = await _userService.DeleteById(id);
            if (success) TempData["SuccessMessage"] = "Succesfully deleted.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool UserExists(int id)
        {
            var User = _userService.GetById(id);
            return User.Result != null;
        }
    }
}
