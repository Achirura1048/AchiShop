using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Achi.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Achi.Utility;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UoW;
        public CategoryController(IUnitOfWork UoW)
        {
            _UoW = UoW;
        }
        public IActionResult Index()
        {
            List<Category> Ctg_List = _UoW.Category.GetAll().ToList();
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
                _UoW.Category.Add(obj);
                _UoW.Save();
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

            Category? categoryid = _UoW.Category.Get(i => i.ID == id);

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
                _UoW.Category.Update(obj);
                _UoW.Save();
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

            Category? categoryid = _UoW.Category.Get(i => i.ID == id);

            if (categoryid == null)
            {
                return NotFound();
            }

            return View(categoryid);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _UoW.Category.Get(i => i.ID == id);

            if (obj == null)
            {
                return NotFound();
            }

            _UoW.Category.Remove(obj);
            _UoW.Save();

            TempData["Success"] = obj.Name + " Kategorisi Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}
