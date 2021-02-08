using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Nvg.SMSService.Data.Entities;

namespace Nvg.SMSService.Data
{
    public class SMSDbContext : DbContext
    {
        public virtual DbSet<SMSTemplateTable> SMSTemplates { get; set; }
        public virtual DbSet<SMSHistoryTable> SMSHistories { get; set; }
        public virtual DbSet<SMSQuotaTable> SMSQuotas { get; set; }
        public virtual DbSet<SMSChannelTable> SMSChannels { get; set; }
        public virtual DbSet<SMSPoolTable> SMSPools { get; set; }
        public virtual DbSet<SMSProviderSettingsTable> SMSProviderSettings { get; set; }

        public string _schema { get; set; }

        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {

        }

        public SMSDbContext(DbContextOptions<SMSDbContext> options, string schema) : base(options)
        {
            _schema = schema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<SMSPoolTable>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);
            /*
            modelBuilder.Entity<SMSProviderSettingsTable>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);
            */;
        }
    }
}
