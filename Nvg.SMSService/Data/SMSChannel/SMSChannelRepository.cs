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

        public SMSResponseDto<SMSChannelTable> AddUpdateSMSChannel(SMSChannelTable channelInput)
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

        public SMSResponseDto<SMSChannelTable> GetSMSChannelByKey(string channelKey)
        {
            var response = new SMSResponseDto<SMSChannelTable>();
            try
            {
                var smsChannel = _context.SMSChannels.FirstOrDefault(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
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

        /// <summary>
        /// Gets the Channel by Channel Key.
        /// </summary>
        /// <param name="channelID">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{SMSChannelTable}"/> model</returns>
        public SMSResponseDto<SMSChannelTable> GetSMSChannelByID(string channelID)
        {
            var response = new SMSResponseDto<SMSChannelTable>();
            try
            {
                var smsChannel = _context.SMSChannels.FirstOrDefault(sp => sp.ID.ToLower().Equals(channelID.ToLower()));
                if (smsChannel != null)
                {                    
                    response.Message = $"Retrieved SMS channel data";
                }
                else
                {
                    response.Message = $"SMS Channel Data not found";
                }
                response.Status = true;
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

        public SMSResponseDto<List<SMSChannelTable>> GetSMSChannels(string poolID)
        {
            var response = new SMSResponseDto<List<SMSChannelTable>>();
            try
            {
                var smsChannels = (from p in _context.SMSPools
                                     join c in _context.SMSChannels on p.ID equals c.SMSPoolID
                                     join pr in _context.SMSProviderSettings on c.SMSProviderID equals pr.ID
                                     where p.ID.ToLower().Equals(poolID.ToLower())
                                     select new SMSChannelTable { 
                                     ID=c.ID,
                                     Key=c.Key,
                                     SMSPoolID=c.SMSPoolID,
                                     SMSProviderID=c.SMSProviderID,
                                     SMSPoolName=p.Name,
                                     SMSProviderName=pr.Name
                                     }).ToList();
                
                response.Message = $"Retrieved {smsChannels.Count} SMS channel data";                
                response.Status = true;
                response.Result = smsChannels;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<string> DeleteSMSChannel(string channelID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsChannel = _context.SMSChannels.Where(o => o.ID.ToLower().Equals(channelID.ToLower())).FirstOrDefault();
                if (smsChannel != null)
                {
                    _context.SMSChannels.Remove(smsChannel);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = $"Deleted Successfully";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = $"Failed to delete";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Channel Data not found";
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

        public SMSResponseDto<List<SMSChannelTable>> GetSMSChannelKeys()
        {
            var response = new SMSResponseDto<List<SMSChannelTable>>();
            try
            {
                var smsChannelKeys = _context.SMSChannels.Select(o => new SMSChannelTable { Key = o.Key, ID = o.ID }).ToList();
                
                response.Message = $"Retrieved {smsChannelKeys.Count} keys";                
                response.Status = true;
                response.Result = smsChannelKeys;
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
