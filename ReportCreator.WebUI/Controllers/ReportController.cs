using ClosedXML.Excel;
using ReportCreator.BLL.Interfaces;
using System.IO;
using System.Web.Mvc;
using System;

namespace ReportCreator.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportingService _reportingService;

        public ReportController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        // GET: Report
        public ActionResult CreateReport(string reportName = "Report")
        {     

            XLWorkbook workbook = _reportingService.GenerateExcelReport(reportName);
            string handle = Guid.NewGuid().ToString();
            byte[] xlsInBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.Position = 0;
                xlsInBytes = memoryStream.ToArray();
            }
            return File(xlsInBytes, "application/vnd.ms-excel", $"Report.xlsx");

        }
    }
}