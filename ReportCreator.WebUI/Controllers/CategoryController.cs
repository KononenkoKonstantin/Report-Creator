using ReportCreator.BLL.DTOs;
using ReportCreator.BLL.Interfaces;
using ReportCreator.BLL.Services;
using ReportCreator.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IExpenditureService _expenditureService;
        public CategoryController(CategoryService categoryService, ExpenditureService expenditureService)
        {
            _categoryService = categoryService;
            _expenditureService = expenditureService;
        }
        // GET: Category
        public ActionResult Index()
        {
            var viewModel = _categoryService.GetAll()
                .Select(c => new CategoryViewModel { Id = c.CategoryId, Name = c.Name })
                .ToList();
            return View(viewModel);
        }

        public ActionResult Create()
        {
            CategoryFormViewModel viewModel = new CategoryFormViewModel()
            {
                Header = "Add a category"
            };
            return View("CategoryForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", viewModel);

            var category = new CategoryDto() { Name = viewModel.Name };
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {            
            var category = _categoryService.GetById((int)id);

            if (category == null)
                return HttpNotFound();

            CategoryFormViewModel viewModel = new CategoryFormViewModel
            {
                Id = id,
                Name = category.Name,
                Header = "Edit category"
            };
            return View("CategoryForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", viewModel);

            var category = _categoryService.GetById(viewModel.Id);
            if (category == null)
                return HttpNotFound();

            category.Update(viewModel.Name);
            _categoryService.Update(category);
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var category = _categoryService.GetById((int)id);
            

            if (category == null)
                return HttpNotFound();

            var expenditure = _expenditureService.GetAll()
                .Where(c => c.Category.CategoryId == category.CategoryId).FirstOrDefault();
            if (expenditure != null)
                return View("_CanNotDelete");
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var category = _categoryService.GetById((int)id);

            if (category == null)
                return HttpNotFound();
            
            _categoryService.Remove(category);
            
            return RedirectToAction("Index");
        }
    }
}