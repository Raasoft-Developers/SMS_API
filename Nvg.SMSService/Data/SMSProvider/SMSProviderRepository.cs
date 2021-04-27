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
                var provider = _context.SMSProviderSettings.FirstOrDefault(sp => sp.Name.Equals(providerInput.Name) && sp.SMSPoolID.Equals(providerInput.SMSPoolID)&& sp.Type.Equals(providerInput.Type));
                if (provider != null)
                {
                    response.Status = false;
                    response.Message = "This Provider is already used.";
                    response.Result = providerInput;
                }
                else
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

        public SMSResponseDto<SMSProviderSettingsTable> UpdateSMSProvider(SMSProviderSettingsTable providerInput)
        {
            var response = new SMSResponseDto<SMSProviderSettingsTable>();
            try
            {
                var provider = _context.SMSProviderSettings.FirstOrDefault(sp => sp.Name.Equals(providerInput.Name) && sp.SMSPoolID.Equals(providerInput.SMSPoolID) && sp.Type.Equals(providerInput.Type));
                if (provider != null)
                {
                    provider.Configuration = providerInput.Configuration;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = providerInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = providerInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Cannot find Provider with Name {providerInput.Name}";
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

        public SMSResponseDto<SMSProviderSettingsTable> GetDefaultSMSProvider()
        {
            var response = new SMSResponseDto<SMSProviderSettingsTable>();
            try
            {
                var smsProvider = _context.SMSProviderSettings.FirstOrDefault(sp => sp.IsDefault);
                if (smsProvider != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved default SMS provider data for {smsProvider.Name}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Default SMS provider data is not available";
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
                                   join c in _context.SMSChannels on new { PoolID = p.SMSPoolID, ProviderID = p.ID } equals new { PoolID = c.SMSPoolID, ProviderID = c.SMSProviderID }
                                   where c.Key.ToLower().Equals(channelKey.ToLower())
                                   select p).FirstOrDefault();
                if (smsProvider != null)
                {
                    response.Message = $"Retrieved SMS provider data for channel {channelKey}";
                }
                else
                {
                    response.Message = $"SMS provider data for channel {channelKey} is not available.";
                }
                response.Status = true;
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
                                   where sp.Name.ToLower().Equals(poolName.ToLower()) && (string.IsNullOrEmpty(providerName) || p.Name.ToLower().Equals(providerName.ToLower()))
                                   select p).ToList();
                //if (smsProviders.Count != 0)
                //{
                //    //if(!string.IsNullOrEmpty(providerName))
                //    //    smsProviders = smsProviders.Where(s => s.Name.ToLower().Equals(providerName.ToLower())).ToList();
                //    response.Status = true;
                //}
                //else
                response.Status = true;
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

        public SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviders(string poolID)
        {
            var response = new SMSResponseDto<List<SMSProviderSettingsTable>>();
            try
            {
                var smsProviders = (from p in _context.SMSProviderSettings
                                      join sp in _context.SMSPools on p.SMSPoolID equals sp.ID
                                      where sp.ID.ToLower().Equals(poolID.ToLower())
                                      select p).ToList();

             
                response.Status = true;
                response.Message = $"Retrieved {smsProviders.Count} SMS providers data for pool";
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

        public SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviderNames(string poolID)
        {
            var response = new SMSResponseDto<List<SMSProviderSettingsTable>>();
            try
            {
                var smsProviders = (from p in _context.SMSProviderSettings
                                      join sp in _context.SMSPools on p.SMSPoolID equals sp.ID
                                      where sp.ID.ToLower().Equals(poolID.ToLower())
                                      select p).ToList();

               
                response.Status = true;
                response.Message = $"Retrieved {smsProviders.Count} SMS providers data for pool";
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

        public SMSResponseDto<string> DeleteSMSProvider(string providerID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsProvider = _context.SMSProviderSettings.Where(o => o.ID.ToLower().Equals(providerID.ToLower())).FirstOrDefault();

                if (smsProvider != null)
                {
                    _context.SMSProviderSettings.Remove(smsProvider);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = $"Deleted successfully";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = $"Delete failed";
                    }
                }
                else
                {
                    response.Message = "No record found.";
                    response.Status = false;
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

        public SMSResponseDto<string> CheckIfSmsProviderIDIsValid(string providerID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsPool = _context.SMSProviderSettings.Any(sp => sp.ID.ToLower().Equals(providerID.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"SMS Provider ID is valid.";
                    response.Result = "Valid SMS Provider.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Provider data is not available";
                    response.Result = "Invalid SMS Provider.";
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

        public SMSResponseDto<string> CheckIfSmsProviderIDNameValid(string providerID, string providerName)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsPool = _context.SMSProviderSettings.Any(sp => sp.ID.ToLower().Equals(providerID.ToLower()) && sp.Name.ToLower().Equals(providerName.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Valid Provider ID and Provider Name {providerName}.";
                    response.Result = "SMS Provider Valid.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Invalid Provider ID and Provider Name {providerName}";
                    response.Result = "SMS Provider Invalid.";
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
    }
}
