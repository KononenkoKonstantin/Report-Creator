using ReportCreator.BLL.Interfaces;
using ReportCreator.BLL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReportCreator.WebUI.Controllers
{
    public class NavController : Controller
    {
        ICategoryService _categoryService;
       
        public NavController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _categoryService.GetAll()
                .Select(x => x.Name);

            return PartialView(categories);
        }

        public PartialViewResult ExpenditureMenu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _categoryService.GetAll()
                .Select(x => x.Name);

            return PartialView(categories);
        }
    }
}