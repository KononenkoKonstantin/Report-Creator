using System.Collections.Generic;

namespace ReportCreator.BLL.DTOs
{
    public class ReportDto
    {             
        public int ReportId { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }

        public decimal TotalSum { get; set; }       
        public ReportDto()
        {
            Categories = new List<CategoryDto>();
        }
    }
}
