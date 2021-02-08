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
        public static void AddSMSServices(this IServiceCollection services, string microservice)
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
