using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
        public async Task<IActionResult> Index()
        {
            var list = await _CategoryService.GetAll();
            return View(list);
        }

        // GET: ProductCategoryController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategoryController/Create
        public IActionResult Create()
        {
            var list = _CategoryService.Entities.Where(p => p.ParentId == null).ToList();
            ViewBag.Categories = list;

            return View();
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategoryViewModel productCategory)
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
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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

        public IActionResult? GetParents(int parentId)
        {
            var list = _CategoryService.Entities.Where(x => x.ParentId == parentId).ToList();
            if (!list.Any())
            {
                return null;
            }
            ViewBag.ChildCategories = list;
            return ViewComponent("Category");
        }
    }
}
