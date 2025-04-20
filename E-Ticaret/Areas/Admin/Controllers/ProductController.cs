using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Achi.Models.ViewModels;
using Achi.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Achi.Utility;
namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]  
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
            List<Product> Prd_List = _UoW.Product.GetAll(includeProperties:"Category")
            .ToList();
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
        public IActionResult Upsert(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _WHE.WebRootPath;
                string productPath = Path.Combine(webRootPath, "images", "product");

                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }

                if (obj.Product.ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Product.ImageFile.FileName);
                    string filePath = Path.Combine(productPath, fileName);

                    if(!string.IsNullOrEmpty(obj.Product.Image))
                    {
                        var oldFilePath = Path.Combine(webRootPath, obj.Product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        obj.Product.ImageFile.CopyTo(fileStream);
                    }

                    obj.Product.Image = @"\images\product\" + fileName;
                }

                if (obj.Product.ID == 0)
                {   

                    _UoW.Product.Add(obj.Product);
                    TempData["Success"] = obj.Product.Title + " başarıyla oluşturuldu.";
                }
                else
                {
                    _UoW.Product.Update(obj.Product);
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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> Prd_List = _UoW.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = Prd_List });
        }
        #endregion
    }
}
