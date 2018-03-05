using System;
using Microsoft.EntityFrameworkCore;

namespace Onboarding.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            
        }

        public override int SaveChanges()  
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseModel>())
            {
                if (auditableEntity.Entity.Id == Guid.Empty)
                {
                    auditableEntity.Entity.Id = Guid.NewGuid();
                }
            }

            return base.SaveChanges();
        } 
    }
}
