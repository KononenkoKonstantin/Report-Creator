namespace ReportCreator.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Payment")]
    public partial class Payment
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        [Display(Name = "Payment date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(50)]        
        public string Receiver { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Purpose of payment")]
        public string PurposeOfPayment { get; set; }
        [Required]
        [Column(TypeName = "money")]       
        public decimal Sum { get; set; }

        public int? ExpenditureId { get; set; }

        public virtual Expenditure Expenditure { get; set; }
    }
}
