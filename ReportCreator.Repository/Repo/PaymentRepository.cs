using ReportCreator.Domain.Entities;
using ReportCreator.Repository.Common;
using System.Data.Entity;
using ReportCreator.Repository.Interfaces;

namespace ReportCreator.Repository.Repo
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        IUnitOfWork _unitOfWork;
        public PaymentRepository(DbContext context, UnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }        
    }
}
