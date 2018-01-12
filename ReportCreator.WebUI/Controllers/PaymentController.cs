using ReportCreator.BLL.DTOs;
using ReportCreator.BLL.Interfaces;
using ReportCreator.BLL.Services;
using ReportCreator.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IExpenditureService _expenditureService;
        public int PageSize = 4;
        public PaymentController(PaymentService paymentService, ExpenditureService expenditureService)
        {
            _paymentService = paymentService;
            _expenditureService = expenditureService;
        }
        // GET: Payment
        public ViewResult List(string category, int page = 1)
        {
            PaymentsListViewModel model = new PaymentsListViewModel()
            {
                Payments = _paymentService.GetAll()
                    .Where(p => category == null || p.Expenditure.Category.Name == category)
                    .OrderBy(p => p.Expenditure.Number)                    
                    .ThenBy(p => p.PaymentDate)                                        
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _paymentService.GetAll()
                    .Where(p => category == null || p.Expenditure.Category.Name == category).Count()
                },
                CurrentCategory = category,
                TotalSum = _paymentService.GetAll().Sum(p => p.Sum),
                CategorySum = _paymentService.GetAll()
                .Where(p => category == null ||  p.Expenditure.Category.Name == category)
                .Sum(p => p.Sum)                
            };            
            return View(model);
        }       
        
        public ActionResult Create()
        {
            List<ExpenditureDto> expenditures = _expenditureService.GetAll().ToList();
            
            var viewModel = new PaymentFormViewModel()
            {
                Expenditures = new SelectList(expenditures, "ExpenditureId", "Number"),                
                Header = "Creating payment"
            };
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            PaymentDto payment = new PaymentDto()
            {
                ExpenditureId = viewModel.ExpenditureId,
                PaymentDate = viewModel.PaymentDate,                
                Receiver = viewModel.Receiver,
                PurposeOfPayment = viewModel.PurposeOfPayment,
                Sum = viewModel.Sum
            };           
            
            _paymentService.Add(payment);
            
            return RedirectToAction("List","Payment");           
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)            
                return HttpNotFound();
           
            PaymentDto payment = _paymentService.GetById((int)id);
            if (!_paymentService.GetAll().Any(e => e.PaymentId == (int)id))
                return HttpNotFound();

            List<ExpenditureDto> expenditures = _expenditureService.GetAll().ToList();
            PaymentFormViewModel viewModel = new PaymentFormViewModel()
            {
                Expenditures = new SelectList(expenditures, "ExpenditureId", "Number", payment.ExpenditureId),
                PaymentId = payment.PaymentId,
                PaymentDate = payment.PaymentDate,
                Receiver = payment.Receiver,
                PurposeOfPayment = payment.PurposeOfPayment,
                Sum = payment.Sum,
                ExpenditureId = payment.ExpenditureId,
                Header = "Editing payment"
            };
            return View(viewModel);
            
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentFormViewModel viewModel)
        {
            if (viewModel == null)
                return HttpNotFound();

            PaymentDto payment = _paymentService.GetById(viewModel.PaymentId);
            if (payment == null)
                return HttpNotFound();
            PaymentDto paymentToUpdate = new PaymentDto()
            {                
                PaymentDate = viewModel.PaymentDate,
                Receiver = viewModel.Receiver,
                PurposeOfPayment = viewModel.PurposeOfPayment,
                ExpenditureId = viewModel.ExpenditureId,
                Sum = viewModel.Sum
            };
            payment.Update(paymentToUpdate);
           
            _paymentService.Update(payment);            
            return RedirectToAction("List");
        }
               

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)            
                return HttpNotFound();
            
            var payment = _paymentService.GetById((int)id);

            if (payment == null)
                return HttpNotFound();
            
            _paymentService.Remove(payment);
                
            return RedirectToAction("List");            
        }        
    }
}