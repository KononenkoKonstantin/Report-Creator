using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportCreator.BLL.DTOs
{
    public class PaymentDto
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
        public int? ExpenditureId { get; set; }
        public virtual ExpenditureDto Expenditure { get; set; }

        public ICollection<ExpenditureDto> Expenditures { get; set; }
        public PaymentDto()
        {
            Expenditures = new List<ExpenditureDto>();
        }
        public void Update(PaymentDto payment)
        {
            PaymentDate = payment.PaymentDate;
            Receiver = payment.Receiver;
            PurposeOfPayment = payment.PurposeOfPayment;
            Sum = payment.Sum;
            ExpenditureId = payment.ExpenditureId;
        }
    }
}
