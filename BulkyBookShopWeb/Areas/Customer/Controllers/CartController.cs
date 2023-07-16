using BulkyBookShop.DataAccess.Repository.IRepository;
using BulkyBookShop.Models;
using BulkyBookShop.Models.ViewModels;
using BulkyBookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookShopWeb.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartVM ShoppingCartVM { get; set; }
        public double OrderTotal { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.Subtotal += (cart.Price * cart.Count);
                if(cart.Count > 0)
                {
                    ShoppingCartVM.ShippingFee = SD.shippingFee;
                }
                else
                {
                    ShoppingCartVM.ShippingFee = 0;
                }
            }
            ShoppingCartVM.CartTotal = (ShoppingCartVM.Subtotal+ ShoppingCartVM.ShippingFee);
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //ShoppingCartVM = new ShoppingCartVM()
            //{
            //    ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
            //    includeProperties: "Product"),
            //};
            //foreach (var cart in ShoppingCartVM.ListCart)
            //{
            //    cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
            //        cart.Product.Price50, cart.Product.Price100);
            //    ShoppingCartVM.Subtotal += (cart.Price * cart.Count);
            //    if (cart.Count > 0)
            //    {
            //        ShoppingCartVM.ShippingFee = SD.shippingFee;
            //    }
            //    else
            //    {
            //        ShoppingCartVM.ShippingFee = 0;
            //    }
            //}
            //ShoppingCartVM.CartTotal = (ShoppingCartVM.Subtotal + ShoppingCartVM.ShippingFee);
            return View();
        }

        public IActionResult plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }

    }
}

       