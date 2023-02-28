using Microsoft.AspNetCore.Mvc;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.HttpExtentions;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    [PermissionChecker("ProductManagement")]
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var Category = await _CategoryService.GetById(id);
                if (Category == null) return NotFound();
                return View(Category);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductCategoryController/Create
        public IActionResult Create()
        {
            var list = _CategoryService.Filter(p => p.ParentId == null);
            ViewBag.Categories = list;

            return View();
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryViewModel productCategory)
        {
            try
            {
                if (!ModelState.IsValid) return View(productCategory);

                // add productCategory to database
                var success = _CategoryService.Insert(productCategory);
                if (success) TempData["SuccessMessage"] = "Succesfully Added.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var Category = await _CategoryService.GetById(id);
                if (Category == null) return NotFound();
                return View(Category);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductCategoryViewModel productCategory)
        {
            try
            {
                if (id != productCategory.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid) return View(productCategory);

                var result = _CategoryService.Update(productCategory);
                if (result) TempData["SuccessMessage"] = "Succesfully Edited.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryController/Delete/5      
        public IActionResult? Delete(int id)
        {
            var category = (_CategoryService.GetSingleNoTracking(c => c.Id == id)).Result;

            if (category != null)
            {
                var result = _CategoryService.Delete(category);
                if (result)
                {
                    return Json(new
                    {
                        status = "Success"
                    });
                }

            }
            return Json(new
            {
                status = "NotFound"
            });
        }

        public IActionResult? GetParents(int parentId)
        {
            var list = _CategoryService.Filter(x => x.ParentId == parentId);
            if (!list.Any())
            {
                return null;
            }
            ViewBag.ChildCategories = list;
            return ViewComponent("Category");
        }
    }
}
