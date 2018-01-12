using ReportCreator.BLL.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Models
{
    public class ExpenditureFormViewModel
    {
        public int ExpenditureId { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int? CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
        public string Header { get; set; }
        public string Action => ExpenditureId == 0 ? "Create" : "Update";
        public SelectList Categories { get; set; }
        public SelectListItem SelectedCategory { get; set; }
        
    }
}