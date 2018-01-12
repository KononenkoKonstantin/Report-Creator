using ReportCreator.Repository.Common;
using ReportCreator.Domain.Entities;
using System.Data.Entity;
using ReportCreator.Repository.Interfaces;

namespace ReportCreator.Repository.Repo
{
    public class CategoryRepository : GenericRepository<Category>
    {
        IUnitOfWork _unitOfWork;        
        public CategoryRepository(DbContext context, UnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;           
        }        
    }
}
