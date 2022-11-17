using Microsoft.EntityFrameworkCore;
using SMSService.Data.Entities;

namespace SMSService.Data
{
    public class SMSOracleDbContext : SMSDbContext
    {
        public SMSOracleDbContext(DbContextOptions<SMSOracleDbContext> options) : base(options)
        {

        }

        public SMSOracleDbContext(DbContextOptions<SMSOracleDbContext> options, string schema) : base(options)
        {
            _schema = schema;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<SMSPoolTable>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);

            // Change column Channel name to Channel key
            modelBuilder.Entity<SMSChannelTable>()
                .Property(c => c.Key)
                .HasColumnName("Key");

            modelBuilder.Entity<SMSChannelTable>()
               .HasIndex(x => x.Key).IsUnique(true);

            modelBuilder.Entity<SMSProviderSettingsTable>()
               .HasIndex(p => new { p.Name, p.SMSPoolID })
               .IsUnique(true);

            modelBuilder.Entity<SMSTemplateTable>()
               .HasIndex(p => new { p.Name, p.SMSPoolID })
               .IsUnique(true);
        }
    }
}
