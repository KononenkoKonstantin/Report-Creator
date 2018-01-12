using ClosedXML.Excel;

namespace ReportCreator.BLL.Interfaces
{
    public interface IReportingService
    {
        XLWorkbook GenerateExcelReport(string reportName);
    }
}
