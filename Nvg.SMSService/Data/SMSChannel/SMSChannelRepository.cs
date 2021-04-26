using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.SMSService.Data.SMSChannel
{
    public class SMSChannelRepository : ISMSChannelRepository
    {
        private readonly SMSDbContext _context;

        public SMSChannelRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSResponseDto<SMSChannelTable> AddSMSChannel(SMSChannelTable channelInput)
        {
            var response = new SMSResponseDto<SMSChannelTable>();
            try
            {
                var channel = _context.SMSChannels.FirstOrDefault(sp => sp.Key.Equals(channelInput.Key) && sp.SMSPoolID.Equals(channelInput.SMSPoolID));
                if (channel != null)
                {
                    response.Status = false;
                    response.Message = "This Channel is already used.";
                    response.Result = channelInput;
                }
                else
                {
                    channelInput.ID = Guid.NewGuid().ToString();
                    _context.SMSChannels.Add(channelInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = channelInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = channelInput;
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

        public SMSResponseDto<SMSChannelTable> UpdateSMSChannel(SMSChannelTable channelInput)
        {
            var response = new SMSResponseDto<SMSChannelTable>();
            try
            {
                var channel = _context.SMSChannels.FirstOrDefault(sp => sp.Key.Equals(channelInput.Key) && sp.SMSPoolID.Equals(channelInput.SMSPoolID));
                if (channel != null)
                {
                    channel.SMSProviderID = channelInput.SMSProviderID;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = channelInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = channelInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Cannot find Channel with Key {channelInput.Key}";
                    response.Result = channelInput;
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

        public SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey)
        {
            var response = new SMSResponseDto<SMSChannelDto>();
            try
            {
                var smsChannel = (from sc in _context.SMSChannels
                                    from sq in _context.SMSQuotas.Where(quota => quota.SMSChannelID == sc.ID).DefaultIfEmpty()
                                    select new SMSChannelDto
                                    {
                                        ID = sc.ID,
                                        Key = sc.Key,
                                        SMSPoolID = sc.SMSPoolID,
                                        SMSProviderID = sc.SMSProviderID,
                                        MonthlyQuota = sq.MonthlyQuota,
                                        TotalQuota = sq.TotalQuota,
                                        MonthlyConsumption = sq.MonthlyConsumption,
                                        TotalConsumption = sq.TotalConsumption,
                                        CurrentMonth = sq.CurrentMonth
                                    }).FirstOrDefault();
                //var smsChannel = _context.SMSChannels.FirstOrDefault(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
                if (smsChannel != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved SMS channel data for {channelKey}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Channel Data Unavailable for {channelKey}";
                }
                response.Result = smsChannel;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<bool> CheckIfChannelExist(string channelKey)
        {
            var response = new SMSResponseDto<bool>();
            try
            {
                var channelExist = _context.SMSChannels.Any(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
                response.Status = channelExist;
                response.Message = $"Is channel existing : {channelExist}";
                response.Result = channelExist;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public SMSResponseDto<string> CheckIfSmsChannelIDIsValid(string channelID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsPool = _context.SMSChannels.Any(sp => sp.ID.ToLower().Equals(channelID.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"SMS Channel ID is valid.";
                    response.Result = "Valid SMS Channel.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Channel data is not available";
                    response.Result = "Invalid SMS Channel.";
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

        public SMSResponseDto<string> CheckIfSmsChannelIDKeyValid(string channelID, string channelKey)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsPool = _context.SMSChannels.Any(sp => sp.ID.ToLower().Equals(channelID.ToLower()) && sp.Key.ToLower().Equals(channelKey.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Valid Channel ID and Channel Key {channelKey}.";
                    response.Result = "SMS Channel Valid.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"No data found for Channel ID and Channel Key {channelKey}";
                    response.Result = "SMS Channel InValid.";
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
