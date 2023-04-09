using Microsoft.AspNetCore.Mvc;
using testApplication.DataAccess;
using testApplication.DataAccess.Repository.IRepository;
using testApplication.Models;

namespace testApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.categoryRepository.GetAll();
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
                    _unitOfWork.categoryRepository.Add(obj);
                    _unitOfWork.save();
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
            var obj = _unitOfWork.categoryRepository.GetFirstOrDefault(x => x.Id == id);
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
                    _unitOfWork.categoryRepository.update(obj);
                    _unitOfWork.save();
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
            var obj = _unitOfWork.categoryRepository.GetFirstOrDefault(x => x.Id == id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _unitOfWork.categoryRepository.GetFirstOrDefault(x => x.Id == id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            _unitOfWork.categoryRepository.Remove(obj);
            _unitOfWork.save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
