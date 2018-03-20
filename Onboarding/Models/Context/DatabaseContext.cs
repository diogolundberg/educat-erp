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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDataDisability>().HasKey(t => new { t.PersonalDataId, t.DisabilityId });
            modelBuilder.Entity<PersonalDataSpecialNeed>().HasKey(t => new { t.PersonalDataId, t.SpecialNeedId });

            modelBuilder.Entity<PersonalDocument>().HasBaseType<DocumentType>();
            modelBuilder.Entity<ResponsibleDocument>().HasBaseType<DocumentType>();
            modelBuilder.Entity<GuarantorDocument>().HasBaseType<DocumentType>();
        }

        public override int SaveChanges()  
        {
            foreach (var auditableEntity in ChangeTracker.Entries<BaseModel>())
            {
                if (auditableEntity.State == EntityState.Added)
                {
                    auditableEntity.Entity.Active = true;
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

        public DbSet<AddressKind> AddressKinds { get; set; }  
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }  
        public DbSet<Country> Countries { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Gender> Genders {get;set;}
        public DbSet<Race> Races { get; set; }
        public DbSet<HighSchoolKind> HighSchoolKinds { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set;}
        public DbSet<SpecialNeed> SpecialNeeds { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
    }
}
