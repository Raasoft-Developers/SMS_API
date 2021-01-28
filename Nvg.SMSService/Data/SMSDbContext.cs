using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Nvg.SMSService.Data.Entities;

namespace Nvg.SMSService.Data
{
    public class SMSDbContext : DbContext
    {
        public virtual DbSet<SMSTemplateTable> SMSTemplate { get; set; }
        public virtual DbSet<SMSHistoryTable> SMSHistory { get; set; }
        public virtual DbSet<SMSCounterTable> SMSCounter { get; set; }

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
        }
    }
}
