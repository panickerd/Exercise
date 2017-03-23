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
    public class ProductController : Controller
    {
        DataAccess _da;
        ProductActions _scm;
        SubCategoryActions _ca;
        public ProductController(DataAccess cm)
        {
            _da = cm;
            _scm = new ProductActions(_da);
            _ca = new SubCategoryActions(_da);
        }

        [HttpGet]
        public ActionResult Product(bool create = false)
        {
            if (create)
            {
                List<SubCategoryModel> subcategoryModels = _ca.GetItems().ToList();
                ViewBag.SubCategoryNames = new SelectList(subcategoryModels, "Id", "Name");
                return View();
            }
            List<ProductModel> productModels = _scm.GetItems().ToList();
            if (productModels.Any())
            {
                return View("GetProducts", productModels);
            }
            else
            {
                List<SubCategoryModel> subcategoryModels = _ca.GetItems().ToList();
                ViewBag.SubCategoryNames = new SelectList(subcategoryModels, "Id", "Name");
                return View();
            }
        }

        // GET api/product/edit
        [HttpGet("{id}")]
        [Route("edit/")]
        public ActionResult Edit(string Id)
        {
            ProductModel scm = _scm.GetItem(Id);
            List<SubCategoryModel> subcategoryModels = _ca.GetItems().ToList();
            ViewBag.SubCategoryNames = new SelectList(subcategoryModels, "Id", "Name");
            return View(scm);
        }

        // POST: api/Product
        [HttpPost]
        public RedirectToActionResult Product(ProductModel scm)
        {
            _scm.Create(scm);
            return RedirectToAction("Product");
        }

        // POST api/product/5&abc
        [HttpPost("{id}/{name}/{categoryid}")]
        public ActionResult Product(string id, string name, string subcategoryid)
        {
            ProductModel scm = new ProductModel
            {
                Id = id,
                Name = name,
                CategoryId = subcategoryid
            };
            _scm.Update(id, scm);
            List<ProductModel> productModels = _scm.GetItems().ToList();
            return View("GetProducts", productModels);
        }

        [HttpGet("{id}")]
        [Route("delete/")]
        public ActionResult Delete(string id)
        {
            _scm.Remove(id);
            List<ProductModel> productModels = _scm.GetItems().ToList();
            if (productModels.Any())
            {
                return View("getproducts", productModels);
            }
            else
                return View("product");
        }
    }
}