using E_Ticaret.Data;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> Ctg_List = _db.Categories.ToList();
            return View(Ctg_List);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = obj.Name + " Kategorisi Başarıyla Oluşturuldu";
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

            Category? categoryid = _db.Categories.First(i=>i.ID==id);

            if (categoryid == null)
            {
                return NotFound();
            }

            return View(categoryid);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = obj.Name + " Kategorisi Başarıyla Güncellendi";
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

            Category? categoryid = _db.Categories.First(i => i.ID == id);

            if (categoryid == null)
            {
                return NotFound();
            }

            return View(categoryid);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.First(i => i.ID == id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();

            TempData["Success"] = obj.Name + " Kategorisi Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}
