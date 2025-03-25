using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Achi.Models.ViewModels;
using Achi.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UoW;
        private readonly IWebHostEnvironment _WHE;
        public ProductController(IUnitOfWork UoW , IWebHostEnvironment WHE)
        {
            _UoW = UoW;
            _WHE = WHE;
        }
        public IActionResult Index()
        {
            List<Product> Prd_List = _UoW.Product.GetAll().ToList();

            return View(Prd_List);
        }

        public IActionResult Upsert(int? id)
        {


            ProductVM productVM = new()
            {
                CategoryList = _UoW.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString()
                }),

                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }

            else
            {
                productVM.Product = _UoW.Product.Get(i => i.ID == id);
                return View(productVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _WHE.WebRootPath;

                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(webRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(ProductPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.Image = @"\images\product\" + filename;
                }

                if (obj.Product.ID == 0)
                {             
                    TempData["Success"] = obj.Product.Title + " başarıyla oluşturuldu.";
                }
                else
                { 
                    TempData["Success"] = obj.Product.Title + " başarıyla güncellendi.";
                }

                
                _UoW.Save();
                return RedirectToAction("Index");
                /*return View(new ProductVM
                {
                    CategoryList = _UoW.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.ID.ToString()
                    }),
                    Product = new Product()
                });*/
            }

            /*obj.CategoryList = _UoW.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });*/

            return View(obj);
        }

       /* public IActionResult Edit(int? id)
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
       */
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
