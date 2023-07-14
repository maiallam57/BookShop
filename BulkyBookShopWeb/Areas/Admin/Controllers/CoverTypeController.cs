using BulkyBookShop.DataAccess.Repository.IRepository;
using BulkyBookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookShopWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
                private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
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
        public IActionResult Create(CoverType obj)
        {
            //custom Validation
            var CoverTypes = _unitOfWork.CoverType.GetAll();
            foreach (string name in CoverTypes.Select(x => x.Name).ToList())
            {
                if (name.ToLower() == obj.Name.ToLower())
                {
                    ModelState.AddModelError("name", "The name is already exists.");
                }

            }
             //server side Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();                  //To be added and saved to the database
                TempData["success"] = "CoverType Created Successfully";
                return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"
            }
            return View(obj);

        }

        //Edit
        //GET ACTION METHOD
        //Editing is occur apon the id of the CoverType
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypeFromDb = _db.(id);
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var CoverTypeFromDbSingle = _db.CoverTypes.SingleOrDefault(c => c.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST ACTION METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            //custom Validation
            var CoverTypes = _unitOfWork.CoverType.GetAll();
            foreach (string name in CoverTypes.Select(x => x.Name).ToList())
            {
                if (name.ToLower() == obj.Name.ToLower())
                {
                    ModelState.AddModelError("name", "The name is already exists.");
                }

            }
            //server side Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();                  //To be added and saved to the database
                TempData["success"] = "CoverType Updated Successfully";
                return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"
            }
            return View(obj);

        }

        //Delete
        //GET ACTION METHOD
        //Delete is occur apon the id of the CoverType
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypeFromDb = _db.CoverTypes.Find(id);
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            //var CoverTypeFromDbSingle = _db.CoverTypes.SingleOrDefault(c => c.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST ACTION METHOD
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();                  //To be added and saved to the database
            TempData["success"] = "CoverType Deleted Successfully";
            return RedirectToAction("Index");   //Instead of directing it io a view,  we Redirect it to Action "Index"


        }
    }
}
