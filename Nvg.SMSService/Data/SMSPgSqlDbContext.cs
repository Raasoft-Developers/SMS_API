using Microsoft.EntityFrameworkCore;
using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data
{
    public class SMSPgSqlDbContext : SMSDbContext
    {
        public SMSPgSqlDbContext(DbContextOptions<SMSPgSqlDbContext> options) : base(options)
        {

        }

        public SMSPgSqlDbContext(DbContextOptions<SMSPgSqlDbContext> options, string schema) : base(options)
        {
            _schema = schema;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<SMSPoolTable>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);

            // Change column Channel name to Channel key
            modelBuilder.Entity<SMSChannelTable>()
                .Property(c => c.Key)
                .HasColumnName("Key");
        }

    }
}
