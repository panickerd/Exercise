using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exercise1.Models;
using MongoDB.Bson;

namespace Exercise1.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        DataAccess _da;
        CategoryActions _ca;
        public CategoryController(DataAccess cm)
        {
            _da = cm;
            _ca = new CategoryActions(_da);
        }

        // GET api/category
        [HttpGet]
        public ActionResult Category(bool create = false)
        {
            if (create) return View();
            List<CategoryModel> categorymodels = _ca.GetItems().ToList();
            if (categorymodels.Any())
            {
                return View("getcategories", categorymodels);
            }
            else
                return View();
        }

        // GET api/category/edit
        [HttpGet("{id}")]
        [Route("edit/")]
        public ActionResult Edit(string Id)
        {
            CategoryModel cm = _ca.GetItem(Id);
            return View(cm);
        }

        // POST api/category
        [HttpPost]
        public RedirectToActionResult Category(CategoryModel cm)
        {
            _ca.Create(cm);
            return RedirectToAction("Category");
        }

        // POST api/category/5&abc
        [HttpPost("{id}/{name}")]
        public ActionResult Category(string id, string name)
        {
            CategoryModel cm = new CategoryModel
            {
                Id = id,
                Name = name
            };
            _ca.Update(id, cm);
            List<CategoryModel> categorymodels = _ca.GetItems().ToList();
            return View("getcategories", categorymodels);
        }

        //api/category/delete
        [HttpGet("{id}")]
        [Route("delete/")]
        public ActionResult Delete(string id)
        {
            _ca.Remove(id);
            List<CategoryModel> categorymodels = _ca.GetItems().ToList();
            if (categorymodels.Any())
            {
                return View("getcategories", categorymodels);
            }
            else
                return View("category");
        }
    }
}
