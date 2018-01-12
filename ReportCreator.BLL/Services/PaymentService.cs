using ReportCreator.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ReportCreator.BLL.DTOs;
using ReportCreator.Domain.Entities;
using ReportCreator.Repository.Common;
using ReportCreator.Repository.Interfaces;
using ReportCreator.Repository.Repo;
using AutoMapper;

namespace ReportCreator.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<Payment> _repoPayment;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(PaymentRepository repoPayment, UnitOfWork unitOfWork)
        {
            _repoPayment = repoPayment;
            _unitOfWork = unitOfWork;
        }
        public void Add(PaymentDto paymentDto)
        {
            _repoPayment.Add(Mapper.Map<Payment>(paymentDto));
            _unitOfWork.Save();
        }

        public IEnumerable<PaymentDto> GetAll()
        {
           return _repoPayment.GetAll()                
                .Select(Mapper.Map<Payment, PaymentDto>)
                .ToList();            
        }
        public PaymentDto GetById(int id)
        {
            return (Mapper.Map<PaymentDto>(_repoPayment.Get(id)));
        }

        public void Remove(PaymentDto paymentDto)
        {
            var payment = _repoPayment.Get(paymentDto.PaymentId);
            if(payment != null)
            {
                _repoPayment.Delete(payment);
                _unitOfWork.Save();
            }
        }

        public void Update(PaymentDto paymentDto)
        {
            var payment = _repoPayment.Get(paymentDto.PaymentId);
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.PurposeOfPayment = paymentDto.PurposeOfPayment;
            payment.Receiver = paymentDto.Receiver;
            payment.Sum = paymentDto.Sum;
            payment.ExpenditureId = paymentDto.ExpenditureId;
            if (payment != null)
            {
                //Mapper.Map(paymentDto, payment);
                _repoPayment.Edit(payment);
                _unitOfWork.Save();
            }
        }

       
    }
}
