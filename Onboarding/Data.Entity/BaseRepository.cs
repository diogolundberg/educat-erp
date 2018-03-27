using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;

namespace Onboarding.Data.Entity
{
    public class BaseRepository<TEntity> : IDisposable where TEntity : BaseModel
    {
        private readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public virtual IEnumerable<TEntity> List()
        {
            return _context.Set<TEntity>();
        }

        public virtual bool Any()
        {
            return _context.Set<TEntity>().Any();
        }

        public virtual TEntity GetByExternalId(string externalId)
        {
            return _context.Set<TEntity>().SingleOrDefault(x => x.ExternalId == externalId);
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(x => x.Id == id);
        }

        public virtual void Add(TEntity obj, bool? saveChanges = true)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
