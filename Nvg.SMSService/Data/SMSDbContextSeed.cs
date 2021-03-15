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
        public async Task SeedAsync(SMSDbContext context, dynamic smsPoolsData, dynamic smsProviderData, dynamic smsChannelsData, dynamic smsTemplatesData)
        {
            await context.Database.MigrateAsync();
            MigrateSMSPoolData(context, smsPoolsData);
            MigrateSMSProviderData(context, smsProviderData);
            MigrateSMSChannelData(context, smsChannelsData);
            MigrateSMSTemplateData(context, smsTemplatesData);
        }

        private void MigrateSMSPoolData(SMSDbContext context, dynamic smsPoolData)
        {
            if(smsPoolData != null)
            {
                if (context.SMSPools.Any())
                {
                    foreach (var pool in smsPoolData)
                    {
                        var name = (string)pool.name;
                        var smsPoolFromTblHasValue = context.SMSPools.Any(s => s.Name == name);
                        if (!smsPoolFromTblHasValue)
                            SeedSMSPools(context, pool);
                    }
                }
                else
                {
                    foreach (var pool in smsPoolData)
                        SeedSMSPools(context, pool);
                }
            }
        }

        private void MigrateSMSChannelData(SMSDbContext context, dynamic smsChannelData)
        {
            if (smsChannelData != null)
            {
                if (context.SMSChannels.Any())
                {
                    foreach (var channel in smsChannelData)
                    {
                        var name = (string)channel.name;
                        var smsPoolID = (string)channel.smsPoolID;
                        var smsChannelFromTblHasValue = context.SMSChannels.Any(s => s.Key == name && s.SMSPoolID == smsPoolID);
                        if (!smsChannelFromTblHasValue)
                            SeedSMSChannels(context, channel);
                    }
                }
                else
                {
                    foreach (var channel in smsChannelData)
                        SeedSMSChannels(context, channel);
                }
            }
        }

        private void MigrateSMSTemplateData(SMSDbContext context, dynamic smsTemplatesData)
        {
            if (smsTemplatesData != null)
            {
                if (context.SMSTemplates.Any())
                {
                    foreach (var template in smsTemplatesData)
                    {
                        var name = (string)template.name;
                        var messageTemplate = (string)template.messageTemplate;
                        var smsPoolID = (string)template.smsPoolID;
                        var smsTemplateFromTblHasValue = context.SMSTemplates.Any(s => s.Name == name && s.SMSPoolID == smsPoolID && s.MessageTemplate == messageTemplate);
                        if (!smsTemplateFromTblHasValue)
                            SeedSMSTemplates(context, template);
                    }
                }
                else
                {
                    foreach (var template in smsTemplatesData)
                        SeedSMSTemplates(context, template);
                }
            }
        }

        private void MigrateSMSProviderData(SMSDbContext context, dynamic smsProvidersData)
        {
            if(smsProvidersData != null)
            {
                if (context.SMSProviderSettings.Any())
                {
                    foreach (var provider in smsProvidersData)
                    {
                        var name = (string)provider.name;
                        var configuration = (string)provider.configuration;
                        var smsPoolID = (string)provider.smsPoolID;
                        var id = (string)provider.id;
                        var smsProviderFromTblHasValue = context.SMSProviderSettings.Any(s => s.ID == id && s.SMSPoolID == smsPoolID);
                        if (!smsProviderFromTblHasValue)
                            SeedSMSProviders(context, provider);
                    }
                }
                else
                {
                    foreach (var provider in smsProvidersData)
                        SeedSMSProviders(context, provider);
                }
            }
        }

        private static void SeedSMSPools(SMSDbContext context, dynamic smsPool)
        {
            context.SMSPools.Add(new SMSPoolTable
            {
                ID = smsPool.id,
                Name = smsPool.name,
            });
            context.SaveChanges();
        }

        private static void SeedSMSChannels(SMSDbContext context, dynamic smsChannel)
        {
            context.SMSChannels.Add(new SMSChannelTable
            {
                ID = smsChannel.id,
                Key = smsChannel.name,
                SMSPoolID = smsChannel.smsPoolID,
                SMSProviderID = smsChannel.smsProviderID
            });
            context.SaveChanges();
        }

        private static void SeedSMSTemplates(SMSDbContext context, dynamic smsTemplate)
        {
            context.SMSTemplates.Add(new SMSTemplateTable
            {
                ID = smsTemplate.id,
                Name = smsTemplate.name,
                MessageTemplate = smsTemplate.messageTemplate,
                Variant = smsTemplate.variant,
                Sender = smsTemplate.sender,
                SMSPoolID = smsTemplate.smsPoolID
            });
            context.SaveChanges();
        }

        private static void SeedSMSProviders(SMSDbContext context, dynamic smsProvider)
        {
            context.SMSProviderSettings.Add(new SMSProviderSettingsTable
            {
                ID = smsProvider.id,
                Name = smsProvider.name,
                Configuration = smsProvider.configuration,
                Type = smsProvider.type,
                SMSPoolID = smsProvider.smsPoolID,
                IsDefault = smsProvider.isDefault
            });
            context.SaveChanges();
        }
    }
}
