using ReportCreator.BLL.Interfaces;
using System.Linq;
using ClosedXML.Excel;

namespace ReportCreator.BLL.Services
{
    public class ReportingService : IReportingService
    {

        ICategoryService _categoryService;
        IExpenditureService _expenditureService;
        public ReportingService(CategoryService categoryService, ExpenditureService expenditureService)
        {
            _categoryService = categoryService;
            _expenditureService = expenditureService;
        }
        public XLWorkbook GenerateExcelReport(string reportName)
        {
            var categories = _categoryService.GetAll();
            var expenditures = _expenditureService.GetAll();
            var totalSum = categories.Sum(c => c.Expenditures.Sum(e => e.Payments.Sum(p => p.Sum)));
            int i = 1;
            XLWorkbook workbook = new XLWorkbook();

            foreach (var category in categories)
            {
                var worksheet = workbook.Worksheets.Add(category.Name);
                worksheet.Column("B").Width = 12;
                worksheet.Column("C").Width = 12;
                worksheet.Column("D").Width = 20;
                worksheet.Column("E").Width = 30;
                worksheet.Column("F").Width = 12;
                worksheet.Cell(2, 2).SetValue("Report name:").Style.Font.SetBold();
                worksheet.Cell(2, 3).SetValue(reportName).Style.Font.SetItalic();


                worksheet.Cell(3, 2).SetValue("Total sum: € ").Style.Font.SetBold();
                worksheet.Cell(3, 3).SetValue(totalSum).Style.Font.SetBold();

                worksheet.Cell(5, 2).SetValue("Current number").Style.Font.SetBold();
                worksheet.Cell(5, 3).SetValue("Day of payment").Style.Font.SetBold();
                worksheet.Cell(5, 4).SetValue("Recipient / depositor").Style.Font.SetBold();
                worksheet.Cell(5, 5).SetValue("Reason of payment").Style.Font.SetBold();
                worksheet.Cell(5, 6).SetValue("Amount in EUR").Style.Font.SetBold();

                int x = 6;
                int y = 2;

                foreach (var exp in expenditures.Where(e => e.Category.CategoryId == category.CategoryId))
                {
                    foreach (var payment in exp.Payments)
                    {
                        worksheet.Cell(x, y).SetValue(exp.Number.ToString() + "." + i); y++;
                        worksheet.Cell(x, y).SetValue(payment.PaymentDate); y++;
                        worksheet.Cell(x, y).SetValue(payment.Receiver); y++;
                        worksheet.Cell(x, y).SetValue(payment.PurposeOfPayment); y++;
                        worksheet.Cell(x, y).SetValue(payment.Sum); y++;
                        
                        x++; y = 2; i++;
                    }
                    i = 1;

                }

            }

            return workbook;
        }
    }
}
