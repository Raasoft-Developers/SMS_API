using Microsoft.EntityFrameworkCore;
using SMSService.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSService.Data
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
                        var key = (string)channel.key;
                        var smsPoolName = (string)channel.smsPoolName;
                        var smsPoolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
                        var smsChannelFromTblHasValue = context.SMSChannels.Any(s => s.Key == key && s.SMSPoolID == smsPoolID);
                        if (!smsChannelFromTblHasValue)
                            SeedSMSChannels(context, channel, smsPoolID);
                    }
                }
                else
                {
                    foreach (var channel in smsChannelData)
                        SeedSMSChannels(context, channel, null);
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
                        var smsPoolName = (string)template.smsPoolName;
                        var smsPoolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
                        var smsTemplateFromTblHasValue = context.SMSTemplates.Any(s => s.Name == name && s.SMSPoolID == smsPoolID && s.MessageTemplate == messageTemplate);
                        if (!smsTemplateFromTblHasValue)
                            SeedSMSTemplates(context, template, smsPoolID);
                    }
                }
                else
                {
                    foreach (var template in smsTemplatesData)
                        SeedSMSTemplates(context, template, null);
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
                        var smsPoolName = (string)provider.smsPoolName;
                        var smsPoolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
                        var smsProviderFromTblHasValue = context.SMSProviderSettings.Any(s => s.Name == name && s.SMSPoolID == smsPoolID);
                        if (!smsProviderFromTblHasValue)
                            SeedSMSProviders(context, provider, smsPoolID);
                    }
                }
                else
                {
                    foreach (var provider in smsProvidersData)
                        SeedSMSProviders(context, provider, null);
                }
            }
        }

        private static void SeedSMSPools(SMSDbContext context, dynamic smsPool)
        {
            context.SMSPools.Add(new SMSPoolTable
            {
                ID = Guid.NewGuid().ToString(),
                Name = smsPool.name,
            });
            context.SaveChanges();
        }

        private static void SeedSMSChannels(SMSDbContext context, dynamic smsChannel,string poolID)
        {
            if (string.IsNullOrEmpty(poolID)) {
                var smsPoolName = (string)smsChannel.smsPoolName;
                poolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
            }
            var smsProviderName = (string)smsChannel.smsProviderName;
            var providerID = context.SMSProviderSettings.Where(o => o.Name.ToLower().Equals(smsProviderName.ToLower())).Select(o => o.ID).FirstOrDefault();
            context.SMSChannels.Add(new SMSChannelTable
            {
                ID = Guid.NewGuid().ToString(),
                Key = smsChannel.key,
                SMSPoolID =poolID,
                SMSProviderID = providerID
            });
            context.SaveChanges();
        }

        private static void SeedSMSTemplates(SMSDbContext context, dynamic smsTemplate, string poolID)
        {
            if (string.IsNullOrEmpty(poolID))
            {
                var smsPoolName = (string)smsTemplate.smsPoolName;
                poolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
            }
            context.SMSTemplates.Add(new SMSTemplateTable
            {
                ID = Guid.NewGuid().ToString(),
                Name = smsTemplate.name,
                MessageTemplate = smsTemplate.messageTemplate,
                Variant = smsTemplate.variant,
                Sender = smsTemplate.sender,
                SMSPoolID =poolID
            });
            context.SaveChanges();
        }

        private static void SeedSMSProviders(SMSDbContext context, dynamic smsProvider, string poolID)
        {
            if (string.IsNullOrEmpty(poolID))
            {
                var smsPoolName = (string)smsProvider.smsPoolName;
                poolID = context.SMSPools.Where(o => o.Name.ToLower().Equals(smsPoolName.ToLower())).Select(o => o.ID).FirstOrDefault();
            }
            context.SMSProviderSettings.Add(new SMSProviderSettingsTable
            {
                ID = Guid.NewGuid().ToString(),
                Name = smsProvider.name,
                Configuration = smsProvider.configuration,
                Type = smsProvider.type,
                SMSPoolID = poolID,
                IsDefault = smsProvider.isDefault
            });
            context.SaveChanges();
        }
    }
}
