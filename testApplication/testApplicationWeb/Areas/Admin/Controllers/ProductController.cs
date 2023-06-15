using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using testApplication.DataAccess;
using testApplication.DataAccess.Repository.IRepository;
using testApplication.Models;

namespace testApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.coverTypeRepository.GetAll();
            return View(objCoverTypeList);
        }

        //get
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> categoryList = _unitOfWork.categoryRepository.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                );

            IEnumerable<SelectListItem> coverTypeList = _unitOfWork.coverTypeRepository.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                );
            if (id == null || id == 0)
            {
                //create product
                return View(product);
            }
            else
            {
                //update product
            }
            return View(product);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.coverTypeRepository.update(obj);
                _unitOfWork.save();
                TempData["success"] = "Cover Type updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _unitOfWork.coverTypeRepository.GetFirstOrDefault(x => x.Id == id);
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
            var obj = _unitOfWork.coverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            //var obj = _db.Categories.FirstOrDefault(obj=>obj.Id == id);
            //var obj = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            if (obj == null)
                return NotFound();
            _unitOfWork.coverTypeRepository.Remove(obj);
            _unitOfWork.save();
            TempData["success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
