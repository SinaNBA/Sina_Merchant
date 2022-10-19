﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    public class ProductCategoryController : AdminBaseController
    {
        #region Constructor
        private readonly IGenericService<ProductCategory, ProductCategoryViewModel> _CategoryService;

        public ProductCategoryController(IGenericService<ProductCategory, ProductCategoryViewModel> categoryService)
        {
            _CategoryService = categoryService;
        }

        #endregion

        // GET: ProductCategoryController
        public async Task<ActionResult> Index()
        {
            var list = await _CategoryService.GetAll();            
            return View(list);
        }

        // GET: ProductCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategoryController/Create
        public ActionResult Create()
        {
            var list = _CategoryService.Entities.Where(p => p.ParentId == null).ToList();
            ViewBag.Categories = list;
            return View();
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCategoryViewModel productCategory)
        {
            try
            {
                if (!ModelState.IsValid) return View(productCategory);

                // add productCategory to database
                var success = await _CategoryService.InsertAsync(productCategory);
                if (success) TempData["SuccessMessage"] = "Succesfully Added.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}