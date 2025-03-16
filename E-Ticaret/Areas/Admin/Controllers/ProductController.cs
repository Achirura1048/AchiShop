using Achi.DataAccess.Repository.IRepository;
using E_Ticaret.Data;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UoW;
        public ProductController(IUnitOfWork UoW)
        {
            _UoW = UoW;
        }
        public IActionResult Index()
        {
            List<Product> Prd_List = _UoW.Product.GetAll().ToList();
            return View(Prd_List);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {


            if (ModelState.IsValid)
            {   
                    _UoW.Product.Add(obj);
                    _UoW.Save();
                    TempData["Success"] = obj.Title + " Kategorisi Başarıyla Oluşturuldu";
                    return View();
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productID = _UoW.Product.Get(i => i.ID == id);

            if (productID == null)
            {
                return NotFound();
            }

            return View(productID);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _UoW.Product.Update(obj);
                _UoW.Save();
                TempData["Success"] = obj.Title + " Kategorisi Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }


            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productID = _UoW.Product.Get(i => i.ID == id);

            if (productID == null)
            {
                return NotFound();
            }

            return View(productID);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _UoW.Product.Get(i => i.ID == id);

            if (obj == null)
            {
                return NotFound();
            }

            _UoW.Product.Remove(obj);
            _UoW.Save();

            TempData["Success"] = obj.Title + " Kategorisi Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}
