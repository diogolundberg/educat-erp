using System;
using Microsoft.EntityFrameworkCore;

namespace SSO.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupFeature>().HasKey(t => new { t.FeatureId, t.GroupId });
            modelBuilder.Entity<UserGroup>().HasKey(t => new { t.UserId, t.GroupId });
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

        public DbSet<User> Users { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}
