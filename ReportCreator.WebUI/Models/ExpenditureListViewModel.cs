using ReportCreator.BLL.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportCreator.WebUI.Models
{
    public class ExpenditureListViewModel
    {       
        
        public IEnumerable<ExpenditureDto> Expenditures { get; set; }

        [Display(Name="Category")]
        
        public string CurrentCategory { get; set; }
        
    }
}