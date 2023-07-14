﻿using BulkyBookShop.DataAccess;
using BulkyBookShop.DataAccess.Repository.IRepository;
using BulkyBookShop.Models;
using BulkyBookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BulkyBookShopWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Create
        //GET ACTION METHOD
        public IActionResult Create()
        {
            return View();
        }

        //POST ACTION METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            //Custom Validation
            var Products = _unitOfWork.Product.GetAll();
            foreach (string Title in Products.Select(x => x.Title).ToList())
            {
                if (Title.ToLower() == obj.Title.ToLower())
                {
                    ModelState.AddModelError("name", "The product name is already exists.");
                }

            }
            //server side Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();                  //To be added and saved to the database
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"
            }
            return View(obj);
           
        }

        //Edit
        //GET ACTION METHOD
        public IActionResult Upsert(int? id)
        {
            //for the dropdown menu
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };
            if (id == null || id == 0)
            {
                //create the product
                return View(productVM);
            }
            else
            {
                //update the product
                //when i click on edit it will load the product data automatically
                productVM.Product= _unitOfWork.Product.GetFirstOrDefault(u=>u.Id==id);
                return View(productVM);
            }

        }


        //POST ACTION METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            //server side Validation
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    //check file extension and size
                    Product product = new Product();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Convert Image to byte[]
                        file.CopyTo(ms);
                        product.ProductImage = ms.ToArray();
                        obj.Product.ProductImage =product.ProductImage;

                    }
                }
                
                if (Request.Form.Files.Count <= 0)
                {
                    TempData["error"] = "Please upload an image for the product! ";
                    return View(obj);
                }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();                  //To be added and saved to the database
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"
            }
            return View(obj);

        }
 
        //API END POINT
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = productList });
        }

        //POST ACTION API
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message = " Error while Deleting!" });
            }
            //var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ProductImage.TrimStart('\\'));
            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();                  //To be added and saved to the database
            return Json(new { success = true, message = " Product Deleted successfully " });
        }
        #endregion
    }

}
 