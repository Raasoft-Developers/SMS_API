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
                response.Status = true;
                response.Message = $"Retrieved SMS channel data for {channelKey}";
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
    }
}
