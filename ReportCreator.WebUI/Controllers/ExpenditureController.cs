using ReportCreator.BLL.DTOs;
using ReportCreator.BLL.Interfaces;
using ReportCreator.BLL.Services;
using ReportCreator.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Controllers
{
    public class ExpenditureController : Controller
    {
        private readonly IExpenditureService _expenditureService;
        private readonly ICategoryService _categoryService;
        private readonly IPaymentService _paymentService;

        public ExpenditureController(ExpenditureService expenditureService, CategoryService categoryService, PaymentService paymentService)
        {
            _expenditureService = expenditureService;
            _categoryService = categoryService;
            _paymentService = paymentService;
        }
        // GET: Expenditure
        public ActionResult Index(string category)
        {
            ExpenditureListViewModel viewModel = new ExpenditureListViewModel()
            {
                Expenditures = _expenditureService.GetAll()
                    .Where(e => category == null || e.Category.Name == category)
                    .OrderBy(e => e.Category.CategoryId)
                    .ThenBy(e => e.Number),
                CurrentCategory = category
            };
            
            return View(viewModel);
        }

        public ActionResult Create()
        {
            List<CategoryDto> categories = _categoryService.GetAll().ToList();
            
            ExpenditureFormViewModel viewModel = new ExpenditureFormViewModel()
            {
                Categories = new SelectList(categories, "CategoryId", "Name"),
                Header = "Creating expenditure"
            };
            return View("ExpenditureForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenditureFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("ExpenditureForm", viewModel);

            var expenditure = new ExpenditureDto()
            {                  
                Number = viewModel.Number,
                Name = viewModel.Name,
                CategoryId = viewModel.CategoryId
            };
            
            _expenditureService.Add(expenditure);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {            
            if (id == null)
                return HttpNotFound();

            List<CategoryDto> categories = _categoryService.GetAll().ToList();            
                
            if (!_expenditureService.GetAll().Any(e => e.ExpenditureId == (int)id))
                return HttpNotFound();
            var expenditure = _expenditureService.GetById((int)id);
            ExpenditureFormViewModel viewModel = new ExpenditureFormViewModel()
            {
                Categories = new SelectList(categories, "CategoryId", "Name", expenditure.CategoryId), 
                ExpenditureId = expenditure.ExpenditureId,
                Number = expenditure.Number,
                Name = expenditure.Name,                
                Header = "Edit expenditure"                
            };
            
            return View("ExpenditureForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ExpenditureFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();

            var expenditure = _expenditureService.GetById(viewModel.ExpenditureId);
            if (expenditure == null)
                return HttpNotFound();
            ExpenditureDto expToUpdate = new ExpenditureDto()
            {
                CategoryId = viewModel.CategoryId,
                Number = viewModel.Number,
                Name = viewModel.Name
            };
            expenditure.Update(expToUpdate);

            _expenditureService.Update(expenditure);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var expenditure = _expenditureService.GetById((int)id);
            if (expenditure == null)
                return HttpNotFound();

            var payment = _paymentService.GetAll()
                .Where(p => p.ExpenditureId == expenditure.ExpenditureId).FirstOrDefault();
            if (payment != null)
                return View("_CanNotDelete");
            return View(expenditure);
        }

        [HttpPost, ActionName("Delete")]        
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var expenditure = _expenditureService.GetById((int)id);

            if (expenditure == null)
                return HttpNotFound();

            _expenditureService.Remove(expenditure);

            return RedirectToAction("Index");
        }
    }
}