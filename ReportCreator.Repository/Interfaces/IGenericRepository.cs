using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReportCreator.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);        
    }
}
