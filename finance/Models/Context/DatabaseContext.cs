using Microsoft.EntityFrameworkCore;
using System;

namespace finance.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseModel>())
            {
                auditableEntity.Entity.ExternalId = !string.IsNullOrEmpty(auditableEntity.Entity.ExternalId) ? auditableEntity.Entity.ExternalId : auditableEntity.Entity.CreateExternalId();

                if (auditableEntity.State == EntityState.Added)
                {
                    auditableEntity.Entity.CreatedAt = DateTime.Now;
                }
                else if (auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Property(x => x.CreatedAt).IsModified = false;
                    auditableEntity.Entity.UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
