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

        public virtual TEntity GetByExternalId(string externalId)
        {
            return _context.Set<TEntity>().SingleOrDefault(x => x.Active && x.ExternalId == externalId);
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(x => x.Active && x.Id == id);
        }

        public virtual void Add(TEntity obj, bool? saveChanges = true)
        {
            obj.DbState = EntityState.Added.ToString();

            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj, TEntity newobj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            obj.Active = false;

            _context.SaveChanges();

            newobj.Id = 0;
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
