using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public SiteUsersController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: SiteUsers
        [HttpGet("User")]
        public IActionResult Index()
        {
            return View(_unitOfWork.SiteUserRepository.GetAll());
        }

        // GET: SiteUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SiteUsers == null)
            {
                return NotFound();
            }

            var siteUser = await _context.SiteUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteUser == null)
            {
                return NotFound();
            }

            return View(siteUser);
        }

        // GET: SiteUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,FName,LName,Address,City,PostalCode,Country,Phone")] SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteUser);
        }

        // GET: SiteUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SiteUsers == null)
            {
                return NotFound();
            }

            var siteUser = await _context.SiteUsers.FindAsync(id);
            if (siteUser == null)
            {
                return NotFound();
            }
            return View(siteUser);
        }

        // POST: SiteUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,FName,LName,Address,City,PostalCode,Country,Phone")] SiteUser siteUser)
        {
            if (id != siteUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteUser);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.SiteUsers == null)
            {
                return NotFound();
            }

            var siteUser = await _context.SiteUsers
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.SiteUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SiteUsers'  is null.");
            }
            var siteUser = await _context.SiteUsers.FindAsync(id);
            if (siteUser != null)
            {
                _context.SiteUsers.Remove(siteUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteUserExists(int id)
        {
            return _context.SiteUsers.Any(e => e.Id == id);
        }
    }
}
