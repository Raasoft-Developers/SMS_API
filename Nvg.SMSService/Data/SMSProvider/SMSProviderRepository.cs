using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.SMSService.Data.SMSProvider
{
    public class SMSProviderRepository : ISMSProviderRepository
    {
        private readonly SMSDbContext _context;

        public SMSProviderRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSResponseDto<SMSProviderSettingsTable> AddSMSProvider(SMSProviderSettingsTable providerInput)
        {
            var response = new SMSResponseDto<SMSProviderSettingsTable>();
            try
            {
                providerInput.ID = Guid.NewGuid().ToString();
                _context.SMSProviderSettings.Add(providerInput);
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Added";
                    response.Result = providerInput;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Not Added";
                    response.Result = providerInput;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByName(string providerName)
        {
            var response = new SMSResponseDto<SMSProviderSettingsTable>();
            try
            {
                var smsProvider = _context.SMSProviderSettings.FirstOrDefault(sp => sp.Name.ToLower().Equals(providerName.ToLower()));
                if(smsProvider != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved SMS provider data for {providerName}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS provider data for {providerName} is not available";
                }
                response.Result = smsProvider;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByChannelKey(string channelKey)
        {
            var response = new SMSResponseDto<SMSProviderSettingsTable>();
            try
            {
                var smsProvider = (from p in _context.SMSProviderSettings
                                   join c in _context.SMSChannels on p.SMSPoolID equals c.SMSPoolID
                                   where c.Key.ToLower().Equals(channelKey.ToLower())
                                   select p).FirstOrDefault();
                if (smsProvider != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved SMS provider data for channel {channelKey}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS provider data for channel {channelKey} is not available.";
                }
                response.Result = smsProvider;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProvidersByPool(string poolName, string providerName)
        {
            var response = new SMSResponseDto<List<SMSProviderSettingsTable>>();
            try
            {
                var smsProviders = (from p in _context.SMSProviderSettings
                                   join sp in _context.SMSPools on p.SMSPoolID equals sp.ID
                                   where sp.Name.ToLower().Equals(poolName.ToLower())
                                   select p).ToList();
                if (smsProviders.Count != 0)
                {
                    if(!string.IsNullOrEmpty(providerName))
                        smsProviders = smsProviders.Where(s => s.Name.ToLower().Equals(providerName)).ToList();
                    response.Status = true;
                }
                else
                    response.Status = false;
                response.Message = $"Retrieved {smsProviders.Count} SMS providers data for pool {poolName}";
                response.Result = smsProviders;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
