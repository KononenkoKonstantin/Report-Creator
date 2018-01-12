namespace ReportCreator.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Expenditure")]
    public partial class Expenditure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expenditure()
        {
            Payments = new HashSet<Payment>();
        }

        public int ExpenditureId { get; set; }

        [Required]
        [StringLength(5)]
        public string Number { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }        
    }
}
