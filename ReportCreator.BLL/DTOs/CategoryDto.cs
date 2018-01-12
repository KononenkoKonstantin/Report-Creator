using System.Collections.Generic;

namespace ReportCreator.BLL.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ExpenditureDto> Expenditures { get; set; }
        public CategoryDto()
        {
            Expenditures = new List<ExpenditureDto>();
        }
        public void Update(string name)
        {
            Name = name;
        }
        
    }
}
