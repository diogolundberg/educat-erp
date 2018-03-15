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
            modelBuilder.Entity<EnrollmentDisability>().HasKey(t => new { t.EnrollmentId, t.DisabilityId });
            modelBuilder.Entity<Enrollment>().HasOne(t => t.CountryOfGraduationFromHighSchool).WithMany(u => u.CountryOfGraduationFromHighSchoolEnrollments);
            modelBuilder.Entity<Enrollment>().HasOne(t => t.OriginCountry).WithMany(u => u.OriginCountryEnrollments);
            modelBuilder.Entity<Enrollment>().HasOne(t => t.BirthState).WithMany(u => u.BornStateEnrollments);
            modelBuilder.Entity<Enrollment>().HasOne(t => t.CountryState).WithMany(u => u.CountryStateEnrollments);
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

        public DbSet<AddressType> AddressTypes { get; set; }  
        public DbSet<CivilStatus> CivilStatus { get; set; }  
        public DbSet<Country> Countries { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Gender> Genders {get;set;}
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<State> States { get; set; }

    }
}
