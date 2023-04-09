using Microsoft.AspNetCore.Mvc;
using testApplication.DataAccess;
using testApplication.Models;

namespace testApplicationWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "Display Order cannot be same as Name");
                }
                else
                {
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _db.Categories.Find(id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "Display Order cannot be same as Name");
                }
                else
                {
                    _db.Categories.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        //get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _db.Categories.Find(id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _db.Categories.Find(id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
