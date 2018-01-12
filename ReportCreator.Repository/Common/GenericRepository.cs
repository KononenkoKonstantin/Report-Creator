using System;
using System.Collections.Generic;
using System.Linq;
using ReportCreator.Repository.Interfaces;
using System.Data.Entity;

namespace ReportCreator.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }
        public virtual T Get(int id)
        {
            return _dbset.Find(id);
        }
        
        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {            
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {             
            _entities.Entry(entity).State = EntityState.Modified;
        }
               
    }
}
