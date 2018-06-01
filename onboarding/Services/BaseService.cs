using onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace onboarding.Services
{
    public class BaseService<TEntity> : IDisposable where TEntity : BaseModel
    {
        public readonly DatabaseContext _context;

        public BaseService(DatabaseContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> List()
        {
            return _context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return Find(x => x.Id == id).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return List().Where(predicate);
        }

        public virtual TEntity Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();

            return GetById(obj.Id);
        }

        public virtual TEntity Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();

            return GetById(obj.Id);
        }

        public virtual void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
