using System.ComponentModel.DataAnnotations;

namespace ReportCreator.WebUI.Models
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Header { get; set; }
        public string Action => Id == 0 ? "Create" : "Update";
    }
}