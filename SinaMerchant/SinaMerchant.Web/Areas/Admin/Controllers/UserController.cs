using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NuGet.Protocol.Core.Types;
using SinaMerchant.Web.Data;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IGenericService<User, UserAddEditViewModel> _genericService;
        private readonly IPasswordHelper _passwordHelper;

        public UserController(IGenericService<User, UserAddEditViewModel> genericService, IPasswordHelper passwordHelper)
        {
            _genericService = genericService;
            _passwordHelper = passwordHelper;
        }

        // GET: User        
        public async Task<IActionResult> Index()
        {
            return View(await _genericService.GetAll());
        }

        // GET: User/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var User = await _genericService.GetById(id);
                if (User == null) return NotFound();
                return View(User);
            }
            else
            {
                return NotFound();
            }
        }

        #region Create
        // GET: User/Create        
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email, Password,FName,LName,Address,City,PostalCode,Country,Phone")] UserAddEditViewModel user)
        {
            if (!ModelState.IsValid) return View(user);            

            // add user to database
            user.Email = user.Email.ToLower().Trim();
            user.Password = _passwordHelper.HashPassword(user.Password);
            var success = await _genericService.InsertAsync(user);
            if (success) TempData["SuccessMessage"] = "Succesfully Added.";
            return RedirectToAction(nameof(Index));
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
        #endregion

        // GET: User/Edit/5        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var User = await _genericService.GetById(id);
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
        public async Task<IActionResult> Edit(int id, UserAddEditViewModel User)
        {
            if (id != User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _genericService.Edit(User);
                    if (success) TempData["SuccessMessage"] = "Succesfully Edited.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(User.Id))
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
            return View(User);
        }

        // GET: User/Delete/5        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _genericService.GetById(id);
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
            if (_genericService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SiteUsers'  is null.");
            }

            var success = await _genericService.DeleteById(id);
            if (success) TempData["SuccessMessage"] = "Succesfully deleted.";

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            var User = _genericService.GetById(id);
            return User.Result != null;
        }
    }
}
