using BulkyBookShop.DataAccess;
using BulkyBookShop.DataAccess.Repository.IRepository;
using BulkyBookShop.Models;
using BulkyBookShop.Models.ViewModels;
using BulkyBookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BulkyBookShopWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Edit
        //GET ACTION METHOD
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
                //create the product
                return View(company);
            }
            else
            {
                //update the product
                //when i click on edit it will load the product data automatically
                company = _unitOfWork.Company.GetFirstOrDefault(u=>u.Id==id);
                TempData["success"] = "Company Updated Successfully";
                return View(company);
            }

        }


        //POST ACTION METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            //server side Validation
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    var Companies = _unitOfWork.Company.GetAll();
                    foreach (string name in Companies.Select(x => x.Name).ToList())
                    {
                        if (name.ToLower() == obj.Name.ToLower())
                        {
                            ModelState.AddModelError("name", "The Company name is already exists.");
                        }

                    }
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Added Successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company Updated Successfully";
                }
                _unitOfWork.Save();                  //To be added and saved to the database
                return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"
            }
            return View(obj);

        }
 
        //API END POINT
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        //POST ACTION API
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message = " Error while Deleting!" });
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();                  //To be added and saved to the database
            return Json(new { success = true, message = " Product Deleted successfully " });
        }
        #endregion
    }

}
 