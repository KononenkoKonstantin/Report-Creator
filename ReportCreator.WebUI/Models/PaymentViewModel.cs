using ReportCreator.BLL.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Models
{
    public class PaymentViewModel
    {        
        public PaymentDto Payment { get; set; }
        public string CurrentCategory { get { return Payment.Expenditure.Category.Name; }
            set { CurrentCategory = value; } }
        [Display(Name ="Expenditure")]
        public int ExpenditureId { get; set; }
        public SelectList Expenditures { get; set; }
        public SelectListItem SelectedExpenditure { get; set; }        
    }
}