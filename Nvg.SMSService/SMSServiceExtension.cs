using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nvg.SMSService.Data;
using Nvg.SMSService.Data.Models;

namespace Nvg.SMSService
{
    public static class SMSServiceExtension
    {
        public static void AddSMSServices(this IServiceCollection services, string microservice)
        {
            services.AddScoped<ISMSInteractor, SMSInteractor>();
            services.AddScoped<ISMSCounterInteractor, SMSCounterInteractor>();
            services.AddScoped<ISMSCounterRepository, SMSCounterRepository>();
            services.AddScoped<ISMSTemplateRepository, SMSTemplateRepository>();
            services.AddScoped<ISMSTemplateInteractor, SMSTemplateInteractor>();
            services.AddScoped<ISMSHistoryRepository, SMSHistoryRepository>();
            services.AddScoped<ISMSHistoryInteractor, SMSHistoryInteractor>();

            services.AddScoped(provider =>
            {
                var dbInfo = provider.GetService<SMSDBInfo>();
                var builder = new DbContextOptionsBuilder<SMSDbContext>();
                builder.UseNpgsql(dbInfo.ConnectionString,
                                  x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                return new SMSDbContext(builder.Options, microservice);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
