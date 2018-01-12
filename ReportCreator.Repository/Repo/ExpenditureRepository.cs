using ReportCreator.Domain.Entities;
using ReportCreator.Repository.Common;
using ReportCreator.Repository.Interfaces;
using System.Data.Entity;

namespace ReportCreator.Repository.Repo
{
    public class ExpenditureRepository : GenericRepository<Expenditure>
    {
        IUnitOfWork _unitOfWork;
        DbContext _context;
        public ExpenditureRepository(DbContext context, UnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _unitOfWork = unitOfWork;            
        } 
        

    }
}
