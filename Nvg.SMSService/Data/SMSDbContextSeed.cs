using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using Nvg.SMSService.Data.Entities;

namespace Nvg.SMSService.Data
{
    public class SMSDbContextSeed
    {
        public async Task SeedAsync(SMSDbContext context, IConfiguration configuration, dynamic smsTemplatesData)
        {
            await context.Database.MigrateAsync();

            if (context.SMSTemplate.Any())
            {
                foreach (var smsTemplate in smsTemplatesData)
                {
                    var name = (string)smsTemplate.Name;
                    var messageTemplate = (string)smsTemplate.MessageTemplate;

                    var smsTemplateFromTblHasValue = context.SMSTemplate.Any(s => s.Name == name && s.MessageTemplate == messageTemplate);
                    if (!smsTemplateFromTblHasValue)
                        SeedSMSTemplate(context, smsTemplate);
                }
            }
            else
            {
                foreach (var smsTemplate in smsTemplatesData)
                    SeedSMSTemplate(context, smsTemplate);
            }
        }

        private static void SeedSMSTemplate(SMSDbContext context, dynamic smsTemplate)
        {
            context.SMSTemplate.Add(new SMSTemplateTable
            {
                Name = smsTemplate.name,
                MessageTemplate = smsTemplate.messageTemplate,
            });
            context.SaveChanges();
        }
    }
}
