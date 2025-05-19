using System.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace E_Ticaret.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger , IConfiguration configuration , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            var tinyMCEApiUrl = _configuration["TinyMCE:APIUrl"];
            ViewBag.TinyMCEApiUrl = tinyMCEApiUrl;
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cart = new()
            {
                Count = 1,
                ProductId = id,
                Product = _unitOfWork.Product.Get(u => u.ID == id, includeProperties: "Category")
            };
            Product product = _unitOfWork.Product.Get(u=>u.ID==id,includeProperties: "Category");
            var tinyMCEApiUrl = _configuration["TinyMCE:APIUrl"];
            ViewBag.TinyMCEApiUrl = tinyMCEApiUrl;
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userID;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userID && u.ProductId == shoppingCart.ProductId, includeProperties: "Product");
            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                shoppingCart.ID = 0;
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }

            
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

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
