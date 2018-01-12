using ReportCreator.BLL.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Models
{
    public class PaymentFormViewModel
    {
        public int PaymentId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Payment date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        [Required]
        public string Receiver { get; set; }
        [Required]
        [Display(Name = "Purpose of payment")]
        public string PurposeOfPayment { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Sum { get; set; }
        [Required]
        [Display(Name = "Expenditure")]
        public int? ExpenditureId { get; set; }
        public virtual ExpenditureDto Expenditure { get; set; }
        //public ICollection<ExpenditureDto> Expenditures { get; set; }
        //public PaymentDto()
        //{
        //    Expenditures = new List<ExpenditureDto>();
        //}
        //public Category CurrentCategory { get { return Expenditure.Category.Category }; set; }
        //[Display(Name = "Expenditure")]
        //[Required]
        //public int? ExpenditureId { get; set; }
        public string Header { get; set; }
       // public string Action => Payment.ExpenditureId == 0 ? "Create" : "Update";
        public SelectListItem SelectedExpenditure { get; set; }
        public SelectList Expenditures { get; set; }        
    }
}