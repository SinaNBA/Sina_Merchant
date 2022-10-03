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
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Data.Entities;
using SinaMerchant.Web.Repositories;

namespace SinaMerchant.Web.Controllers
{
    public class SiteUsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiteUsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: SiteUsers        
        public IActionResult Index()
        {
            return View(_unitOfWork.SiteUserRepository.GetAll());
        }

        // GET: SiteUsers/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var siteUser = await _unitOfWork.SiteUserRepository
                .GetById(id);
                if (siteUser == null)
                {
                    return NotFound();
                }
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
        public async Task<IActionResult> Create(SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                var succes = _unitOfWork.SiteUserRepository.Insert(siteUser);
                ViewBag.Succes = succes;
                await _unitOfWork.Save();
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

            var siteUser = await _unitOfWork.SiteUserRepository.GetById(id);
            if (siteUser == null)
            {
                return NotFound();
            }

            return View(siteUser);
        }

        // POST: SiteUsers/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SiteUser siteUser)
        {
            if (id != siteUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.SiteUserRepository.Update(siteUser);
                    await _unitOfWork.Save();
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

            var siteUser = await _unitOfWork.SiteUserRepository.GetById(id);
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
            if (_unitOfWork.SiteUserRepository.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SiteUsers'  is null.");
            }
            var siteUser = await _unitOfWork.SiteUserRepository.GetById(id);
            if (siteUser != null)
            {
                _unitOfWork.SiteUserRepository.Delete(siteUser);
            }

            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteUserExists(int id)
        {
            var siteUser = _unitOfWork.SiteUserRepository.GetById(id);
            return siteUser != null;
        }
    }
}
