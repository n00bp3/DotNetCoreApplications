using Microsoft.AspNetCore.Mvc;
using testApplication.DataAccess;
using testApplication.DataAccess.Repository.IRepository;
using testApplication.Models;

namespace testApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.coverTypeRepository.GetAll();
            return View(objCoverTypeList);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.coverTypeRepository.Add(obj);
                _unitOfWork.save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
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
