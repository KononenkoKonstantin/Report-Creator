using System.Collections.Generic;

namespace ReportCreator.BLL.DTOs
{
    public class ExpenditureDto
    {
        public int ExpenditureId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual CategoryDto Category { get; set; }
        
        public ICollection<PaymentDto> Payments { get; set; }        
        public ExpenditureDto()
        {
            Payments = new List<PaymentDto>();
        }

        public void Update(ExpenditureDto expenditure)
        {
            Number = expenditure.Number;
            Name = expenditure.Name;
            CategoryId = expenditure.CategoryId;
        }
    }
}
