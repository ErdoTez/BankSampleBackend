using CUSTOM.DbRepository;
using CUSTOM.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.Models
{
    public class DbContext : ApplicationDbContext
    {

        public DbContext(DbSettings db) : base(db)
        {

        }

        public DbSet<CardTransactionInfo> CardTransactionInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var databaseSchemaName = dbSettings.DatabaseSchemaName;
            if (string.IsNullOrWhiteSpace(databaseSchemaName))
                databaseSchemaName = "bank_sample";

            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetSchema(databaseSchemaName);
            }
        }


    }
}
