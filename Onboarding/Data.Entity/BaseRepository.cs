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
           return _context.Set<TEntity>().Where(x => x.Active);
        }

        public virtual bool Any()
        {
            return _context.Set<TEntity>().Where(x => x.Active).Any();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Active && x.ExternalId == id);
        }

        public virtual TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Active && x.Id == id);
        }

        public virtual void Add(TEntity obj, bool? saveChanges = true)
        {
            TEntity last = _context.Set<TEntity>().OrderByDescending(x => x.ExternalId).FirstOrDefault();
            
            obj.ExternalId =  last != null ? last.ExternalId + 1 : 1;
            obj.DbState = EntityState.Added.ToString();
            obj.Id = Guid.NewGuid();

            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj, TEntity newobj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            obj.Active = false;

            _context.SaveChanges();

            newobj.Id = Guid.NewGuid();
            newobj.ExternalId = obj.ExternalId;
            newobj.DbState = EntityState.Modified.ToString();
            _context.Set<TEntity>().Add(newobj);

            _context.SaveChanges();
        }

        public virtual void Delete(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            obj.DbState = EntityState.Deleted.ToString();
            obj.Active = false;

            _context.SaveChanges();
        }
    }
}