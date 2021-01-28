using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data
{
    public class SMSDbContextFactory : IDesignTimeDbContextFactory<SMSDbContext>
    {
        public SMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SMSDbContext>();
            // For Removing migration or Updating the database, uncomment the hardcoded connection string.
            //string connectionString = "Server=172.16.16.62;Database=TestIdentity;User ID =Nyletech;Password=Novigo@123;Port=5432;Integrated Security=true;Pooling=true;No Reset On Close=true;Trust Server Certificate=true;Server Compatibility Mode=Redshift;";
            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseNpgsql("SMS-ConnectionString");
            return new SMSDbContext(optionsBuilder.Options, "SMS"); // TODO: Should avoid hardcoding of schema.
        }
    }
}
