using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMSService.Data;
using SMSService.Data.Entities;
using SMSService.Data.Models;
using SMSService.Data.SMSChannel;
using SMSService.Data.SMSHistory;
using SMSService.Data.SMSPool;
using SMSService.Data.SMSProvider;
using SMSService.Data.SMSQuota;
using SMSService.Data.SMSTemplate;
using SMSService.DTOS;
using SMSService.SMS;
using SMSService.SMSChannel;
using SMSService.SMSHistory;
using SMSService.SMSPool;
using SMSService.SMSProvider;
using SMSService.SMSQuota;
using System;
using System.Reflection;

namespace SMSService
{
    public static class SMSServiceExtension
    {
        public static void AddSMSServices(this IServiceCollection services, string microservice, string databaseProvider)
        {
            services.AddScoped<ISMSInteractor, SMSInteractor>();

            services.AddScoped<ISMSEventInteractor, SMSEventInteractor>();
            services.AddScoped<ISMSManagementInteractor,SMSManagementInteractor>();

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
                default:
                    services.AddScoped<SMSDbContext, SMSPgSqlDbContext>(provider =>
                    {
                        var dbInfo = provider.GetService<SMSDBInfo>();
                        var builder = new DbContextOptionsBuilder<SMSPgSqlDbContext>();
                        builder.UseNpgsql(dbInfo.ConnectionString,
                                          x => x.MigrationsHistoryTable("__MyMigrationsHistory", microservice));
                        return new SMSPgSqlDbContext(builder.Options, microservice);
                    });
                    break;
            }
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var notificationConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SMSHistoryProfile>();
                cfg.CreateMap<SMSHistoryTable, SMSHistoryDto>();
            });
            IMapper notificationMapper = new Mapper(notificationConfig);
            notificationMapper.Map<SMSHistoryTable, SMSHistoryDto>(new SMSHistoryTable());
        }

    }
}
