using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Achi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AchiShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _UoW;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork UoW)
        {
            _UoW = UoW;

        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _UoW.ShoppingCart.GetAll(u => u.ApplicationUserId == userID , includeProperties:"Product"),
                
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                 cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.CartTotal += (cart.Count * cart.Price);
            }
            return View(ShoppingCartVM);
        }
        public IActionResult Summary()
        {
            return View();
        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }

            else
            {
                if(shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else 
                {
                    return shoppingCart.Product.Price100;
                }

            }
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _UoW.ShoppingCart.Get(u => u.ID == cartId);
            cartFromDb.Count += 1;
            _UoW.ShoppingCart.Update(cartFromDb);
            _UoW.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _UoW.ShoppingCart.Get(u => u.ID == cartId);
            if (cartFromDb.Count == 1)
            {
                _UoW.ShoppingCart.Remove(cartFromDb);
                _UoW.Save();
            }
            else
            {
                cartFromDb.Count -= 1;
                _UoW.ShoppingCart.Update(cartFromDb);
                _UoW.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _UoW.ShoppingCart.Get(u => u.ID == cartId);
            _UoW.ShoppingCart.Remove(cartFromDb);
            _UoW.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
