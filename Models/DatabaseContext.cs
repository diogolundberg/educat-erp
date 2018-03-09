using System;
using System.Threading;
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
                if (auditableEntity.State == EntityState.Added)
                {
                    auditableEntity.Entity.Id = Guid.NewGuid();
                    auditableEntity.Entity.Active = true;
                    auditableEntity.Entity.State = auditableEntity.State.ToString();
                    auditableEntity.Entity.CommitedBy = "";
                    auditableEntity.Entity.CommittedAt = DateTime.Now;
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbSet<AddressType> AddressTypes { get; set; }  
        public DbSet<CivilStatus> CivilStatus { get; set; }  
        public DbSet<Country> Countries { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Gender> Genders {get;set;}
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<State> States { get; set; }
    }
}
