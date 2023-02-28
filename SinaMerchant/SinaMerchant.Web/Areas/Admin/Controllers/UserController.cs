using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.HttpExtentions;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    [PermissionChecker("UserManagement")]
    public class UserController : AdminBaseController
    {
        #region Constructor
        private readonly IUserService<UserAddEditViewModel> _userService;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;

        public UserController(IUserService<UserAddEditViewModel> userService,
            IPasswordHelper passwordHelper, IMapper mapper)
        {
            _userService = userService;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
        }
        #endregion

        #region Get Users
        // GET: User        
        public async Task<IActionResult> Index(bool admin)
        {
            if (admin)
            {
                var adminList = _userService.Filter(u => u.IsAdmin);
                if (adminList != null)
                {
                    return View(adminList.ToList());
                }
            }
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
            var createPageMode = new UserAddEditViewModel() { PageMode = "Create" }; //set page mode
            return View(createPageMode);
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
            var success = _userService.Insert(user);
            if (success) TempData["SuccessMessage"] = "Succesfully Added.";
            return RedirectToAction(nameof(Index));
        }

        // check user is exists or not
        public async Task<IActionResult> VerifyEmail(string email, string pageMode)
        {
            if ((await _userService.IsExist(x => x.Email == email.ToLower().Trim())) && pageMode == "Create")
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

            User.PageMode = "Edit"; //set page mode
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
            
            var user = await _userService.GetSingleNoTracking(u => u.Id == id);
            if (user == null) return NotFound();

            ModelState.Remove("Email");
            userViewModel.EmailActiveCode = user.EmailActiveCode;
            userViewModel.Password = _passwordHelper.HashPassword(userViewModel.Password);                       

            if (ModelState.IsValid)
            {
                try
                {
                    var success = _userService.Update(userViewModel);
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
