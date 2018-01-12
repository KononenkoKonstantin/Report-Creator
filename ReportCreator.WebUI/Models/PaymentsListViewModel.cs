using ReportCreator.BLL.DTOs;
using System.Collections.Generic;

namespace ReportCreator.WebUI.Models
{
    public class PaymentsListViewModel
    {
        public IEnumerable<PaymentDto> Payments { get; set; }                
        public PagingInfo PagingInfo { get; set; }        
        public string CurrentCategory { get; set; }
        public decimal TotalSum { get; set; }
        public decimal CategorySum { get; set; }
    }
}