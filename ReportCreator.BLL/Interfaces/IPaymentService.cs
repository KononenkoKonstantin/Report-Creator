using ReportCreator.BLL.DTOs;
using System.Collections.Generic;

namespace ReportCreator.BLL.Interfaces
{
    public interface IPaymentService
    {
        void Add(PaymentDto paymentDto);
        void Remove(PaymentDto paymentDto);
        void Update(PaymentDto paymentDto);
        PaymentDto GetById(int id);
        IEnumerable<PaymentDto> GetAll();                
    }
}
