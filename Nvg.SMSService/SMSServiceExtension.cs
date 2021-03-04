using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nvg.SMSService.Data;
using Nvg.SMSService.Data.Models;
using Nvg.SMSService.SMS;
using Nvg.SMSService.Data.SMSTemplate;
using Nvg.SMSService.Data.SMSQuota;
using Nvg.SMSService.SMSHistory;
using Nvg.SMSService.Data.SMSHistory;
using Nvg.SMSService.SMSQuota;
using Nvg.SMSService.SMSPool;
using Nvg.SMSService.Data.SMSPool;
using Nvg.SMSService.SMSProvider;
using Nvg.SMSService.Data.SMSProvider;
using Nvg.SMSService.SMSChannel;
using Nvg.SMSService.Data.SMSChannel;

namespace Nvg.SMSService
{
    public static class SMSServiceExtension
    {
        public static void AddSMSServices(this IServiceCollection services, string microservice, string databaseProvider)
        {
            services.AddScoped<ISMSInteractor, SMSInteractor>();

            services.AddScoped<ISMSEventInteractor, SMSEventInteractor>();

            services.AddScoped<ISMSPoolInteractor, SMSPoolInteractor>();
            services.AddScoped<ISMSPoolRepository, SMSPoolRepository>();

            services.AddScoped<ISMSProviderInteractor, SMSProviderInteractor>();
            services.AddScoped<ISMSProviderRepository, SMSProviderRepository>();

            services.AddScoped<ISMSTemplateInteractor, SMSTemplateInteractor>();
            services.AddScoped<ISMSTemplateRepository, SMSTemplateRepository>();

            services.AddScoped<ISMSChannelInteractor, SMSChannelInteractor>();
            services.AddScoped<ISMSChannelRepository, SMSChannelRepository>();

            services.AddScoped<ISMSQuotaInteractor, SMSQuotaInteractor>();
            services.AddScoped<ISMSQuotaRepository, SMSQuotaRepository>();

            services.AddScoped<ISMSHistoryRepository, SMSHistoryRepository>();
            services.AddScoped<ISMSHistoryInteractor, SMSHistoryInteractor>();
            databaseProvider ??= string.Empty;
            switch (databaseProvider.ToLower())
            {
                case "postgresql":
                    services.AddScoped<SMSDbContext, SMSPgSqlDbContext>(provider =>
                    {
                        var dbInfo = provider.GetService<SMSDBInfo>();
                        var builder = new DbContextOptionsBuilder<SMSPgSqlDbContext>();
                        builder.UseNpgsql(dbInfo.ConnectionString,
                                          x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                        return new SMSPgSqlDbContext(builder.Options, microservice);
                    });
                    break;
                case "mssql":
                    services.AddScoped<SMSDbContext, SMSSqlServerDbContext>(provider =>
                    {
                        var dbInfo = provider.GetService<SMSDBInfo>();
                        var builder = new DbContextOptionsBuilder<SMSSqlServerDbContext>();
                        builder.UseSqlServer(dbInfo.ConnectionString,
                                          x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                        return new SMSSqlServerDbContext(builder.Options, microservice);
                    });
                    break;
                case "oracle":
                    //services.AddScoped<SMSDbContext, SMSOracleDbContext>(provider =>
                    //{
                    //    var dbInfo = provider.GetService<SMSDBInfo>();
                    //    var builder = new DbContextOptionsBuilder<SMSOracleDbContext>();
                    //    builder.UseOracle(dbInfo.ConnectionString,
                    //                      x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                    //    return new SMSOracleDbContext(builder.Options, microservice);
                    //});
                    break;
                default:
                    services.AddScoped<SMSDbContext, SMSSqlServerDbContext>(provider =>
                    {
                        var dbInfo = provider.GetService<SMSDBInfo>();
                        var builder = new DbContextOptionsBuilder<SMSSqlServerDbContext>();
                        builder.UseSqlServer(dbInfo.ConnectionString,
                                          x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                        return new SMSSqlServerDbContext(builder.Options, microservice);
                    });
                    break;
            }
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}
