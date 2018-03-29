using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Onboarding.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PersonalDataDisability>().HasKey(t => new { t.PersonalDataId, t.DisabilityId });
            builder.Entity<PersonalDataSpecialNeed>().HasKey(t => new { t.PersonalDataId, t.SpecialNeedId });
            builder.Entity<PersonalDataDocument>().HasKey(t => new { t.PersonalDataId, t.DocumentId });
            builder.Entity<GuarantorDocument>().HasKey(t => new { t.GuarantorId, t.DocumentId });

            builder.Entity<PersonalDocumentType>().HasBaseType<DocumentType>();
            builder.Entity<ResponsibleDocumentType>().HasBaseType<DocumentType>();
            builder.Entity<GuarantorDocumentType>().HasBaseType<DocumentType>();

            builder.Entity<RepresentativePerson>().HasBaseType<Representative>();
            builder.Entity<RepresentativeCompany>().HasBaseType<Representative>();
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

        public DbSet<AddressKind> AddressKinds { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<HighSchoolKind> HighSchoolKinds { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<SpecialNeed> SpecialNeeds { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<PersonalData> PersonalDatas { get; set; }
        public DbSet<FinanceData> FinanceDatas { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<Guarantor> Guarantors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
