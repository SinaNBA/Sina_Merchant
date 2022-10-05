using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SinaMerchant.Web.Data;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Controllers
{
    public class SiteUsersController : Controller
    {
        private readonly IGenericService<SiteUser, SiteUserViewModel> _genericService;

        public SiteUsersController(IGenericService<SiteUser, SiteUserViewModel> genericService)
        {
            _genericService = genericService;
        }


        // GET: SiteUsers        
        public async Task<IActionResult> Index()
        {
            return View(await _genericService.GetAll());
        }

        // GET: SiteUsers/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var siteUser = await _genericService.GetById(id);
                if (siteUser == null) return NotFound();
                return View(siteUser);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: SiteUsers/Create        
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteUsers/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email, Password,FName,LName,Address,City,PostalCode,Country,Phone")] SiteUserViewModel siteUser)
        {
            if (ModelState.IsValid)
            {
                var success = await _genericService.InsertAsync(siteUser);
                if (success) TempData["SuccessMessage"] = "Succesfully Added.";
                return RedirectToAction(nameof(Index));

            }
            return View(siteUser);
        }

        // GET: SiteUsers/Edit/5        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteUser = await _genericService.GetById(id);
            if (siteUser == null)
            {
                return NotFound();
            }

            return View(siteUser);
        }

        // POST: SiteUsers/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SiteUserViewModel siteUser)
        {
            if (id != siteUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _genericService.Edit(siteUser);
                    if (success) TempData["SuccessMessage"] = "Succesfully Edited.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteUserExists(siteUser.Id))
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
            return View(siteUser);
        }

        // GET: SiteUsers/Delete/5        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteUser = await _genericService.GetById(id);
            if (siteUser == null)
            {
                return NotFound();
            }

            return View(siteUser);
        }

        // POST: SiteUsers/Delete/5
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

        private bool SiteUserExists(int id)
        {
            var siteUser = _genericService.GetById(id);
            return siteUser.Result != null;
        }
    }
}
