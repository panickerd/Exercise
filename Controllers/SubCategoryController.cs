using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exercise1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exercise1.Controllers
{
    [Produces("application/json")]
    [Route("api/SubCategory")]
    public class SubCategoryController : Controller
    {
        DataAccess _da;
        SubCategoryActions _scm;
        CategoryActions _ca;
        public SubCategoryController(DataAccess cm)
        {
            _da = cm;
            _scm = new SubCategoryActions(_da);
            _ca = new CategoryActions(_da);
        }

        [HttpGet]
        public ActionResult SubCategory(bool create = false)
        {
            if (create)
            {
                List<CategoryModel> categoryModels = _ca.GetItems().ToList();
                ViewBag.CategoryNames = new SelectList(categoryModels, "Id", "Name");
                return View();
            }
            List<SubCategoryModel> subCategoryModels = _scm.GetItems().ToList();
            if (subCategoryModels.Any())
            {
                return View("GetSubCategories", subCategoryModels);
            }
            else
            {
                List<CategoryModel> categoryModels = _ca.GetItems().ToList();
                ViewBag.CategoryNames = new SelectList(categoryModels, "Id", "Name");
                return View();
            }
        }

        // GET api/subcategory/edit
        [HttpGet("{id}")]
        [Route("edit/")]
        public ActionResult Edit(string Id)
        {
            SubCategoryModel scm = _scm.GetItem(Id);
            List<CategoryModel> categoryModels = _ca.GetItems().ToList();
            ViewBag.CategoryNames = new SelectList(categoryModels, "Id", "Name");
            return View(scm);
        }

        // POST: api/SubCategory
        [HttpPost]
        public RedirectToActionResult SubCategory(SubCategoryModel scm)
        {
            _scm.Create(scm);
            return RedirectToAction("SubCategory");
        }

        // POST api/category/5&abc
        [HttpPost("{id}/{name}/{categoryid}")]
        public ActionResult Category(string id, string name, string categoryid)
        {
            SubCategoryModel scm = new SubCategoryModel
            {
                Id = id,
                Name = name,
                CategoryId = categoryid
            };
            _scm.Update(id, scm);
            List<SubCategoryModel> subCategoryModels = _scm.GetItems().ToList();
            return View("GetSubCategories", subCategoryModels);
        }

        [HttpGet("{id}")]
        [Route("delete/")]
        public ActionResult Delete(string id)
        {
            _scm.Remove(id);
            List<SubCategoryModel> subcategorymodels = _scm.GetItems().ToList();
            if (subcategorymodels.Any())
            {
                return View("getsubcategories", subcategorymodels);
            }
            else
                return View("subcategory");
        }
    }
}
