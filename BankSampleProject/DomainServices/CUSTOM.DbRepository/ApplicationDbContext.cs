using CUSTOM.DbRepository.BaseDbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository
{
    public class ApplicationDbContext : DbContext
    {

        protected readonly DbSettings dbSettings;

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbSettings dbSettings) : base()
        {
            this.dbSettings = dbSettings;
        }


        public void ResetContextAndUndoAllChanges()
        {
            this.ChangeTracker.DetectChanges();
            var modifiedEntities = this.ChangeTracker.Entries().ToList();
            foreach (var entry in modifiedEntities)
            {
                try
                {
                    entry.State = EntityState.Detached;
                }
                catch { }
            }
            base.SaveChanges();
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var modifiedEntities = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted).ToList();
            foreach (var entry in modifiedEntities)
            {
                if (entry.State == EntityState.Added && entry.Entity is IEntityAudit)
                {
                    (entry.Entity as IEntityAudit).CreatedAt = DateTime.Now.ToUniversalTime();
                    (entry.Entity as IEntityAudit).CreatedBy = 0;
                }

                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
                    (entry.Entity as IEntityAudit).UpdatedAt = DateTime.Now.ToUniversalTime();
                    (entry.Entity as IEntityAudit).UpdatedBy = 0;
                }
            }

            List<KeyValuePair<EntityState, object>> modifiedEntitiesForRules = new List<KeyValuePair<EntityState, object>>();

            foreach (var entry in modifiedEntities)
            {
                modifiedEntitiesForRules.Add(new KeyValuePair<EntityState, object>(entry.State, entry.Entity));
            }
            var affectedRows = base.SaveChanges();
            if (affectedRows <= 0)
            {
                affectedRows = 1;
            }
            return affectedRows;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(dbSettings.ConnectionString);

            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
