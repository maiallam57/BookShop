using BulkyBookShop.DataAccess.Repository.IRepository;
using BulkyBookShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookShopWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType")
            };
            return View(cartObj);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public IActionResult Details(ShoppingCart CartObject)
        //{
        //    CartObject.Id = 0;
        //    if (ModelState.IsValid)
        //    {
        //        //then we add to card
        //        //var claimsIdentity = (claimsIdentity)User.Identity;
        //        return View();
        //    }
        //    else
        //    {
        //        var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == CartObject.ProductId, includeProperties: "Category,CoverType");
        //        ShoppingCart cartObj = new ShoppingCart()
        //        {
        //            Product = productFromDb,
        //            ProductId = productFromDb.Id
        //        };
        //        return View(cartObj); 
        //    }

        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}