using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nvg.SMSService.Data.SMSHistory
{
    public class SMSHistoryRepository : ISMSHistoryRepository
    {
        private readonly SMSDbContext _context;

        public SMSHistoryRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSResponseDto<SMSHistoryTable> AddSMSHistory(SMSHistoryTable historyInput)
        {
            var response = new SMSResponseDto<SMSHistoryTable>();
            try
            {
                historyInput.ID = Guid.NewGuid().ToString();
                historyInput = _context.SMSHistories.Add(historyInput).Entity;
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Added";
                    response.Result = historyInput;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Not Added";
                    response.Result = historyInput;
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

        public SMSResponseDto<List<SMSHistoryTable>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate)
        {
            var response = new SMSResponseDto<List<SMSHistoryTable>>();
            try
            {
                var fromDateDateTime = Convert.ToDateTime(fromDate);
                if (fromDate.Contains("Z"))
                    fromDateDateTime = fromDateDateTime.ToUniversalTime();
                var toDateDateTime = Convert.ToDateTime(toDate);
                if (toDate.Contains("Z"))
                    toDateDateTime = toDateDateTime.ToUniversalTime();

                var smsHistories = new List<SMSHistoryTable>();
                smsHistories = (from h in _context.SMSHistories
                                join c in _context.SMSChannels on h.SMSChannelID equals c.ID
                                join pr in _context.SMSProviderSettings on h.SMSProviderID equals pr.ID
                                where c.Key.ToLower().Equals(channelKey.ToLower()) && (string.IsNullOrEmpty(tag) || h.Tags.ToLower().Equals(tag.ToLower()))
                                && h.SentOn >= fromDateDateTime && h.SentOn <= toDateDateTime
                                select new SMSHistoryTable
                                {
                                    ID = h.ID,
                                    Sender = h.Sender,
                                    Recipients = h.Recipients,
                                    SentOn = h.SentOn,
                                    MessageSent = h.MessageSent,
                                    Attempts = h.Attempts,
                                    Status = h.Status,
                                    Tags = h.Tags,
                                    SMSProviderID = h.SMSProviderID,
                                    SMSChannelID = h.SMSChannelID,
                                    TemplateName = h.TemplateName,
                                    TemplateVariant = h.TemplateVariant,
                                    ChannelKey = c.Key,
                                    ProviderName = pr.Name
                                }).ToList();

                response.Status = true;

                response.Message = $"Retrieved {smsHistories.Count} SMS histories data for the given date range.";
                response.Result = smsHistories;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<List<SMSHistoryTable>> GetSMSHistoriesByTag(string channelKey, string tag)
        {
            var response = new SMSResponseDto<List<SMSHistoryTable>>();
            try
            {
                var smsHistories = new List<SMSHistoryTable>();
                    smsHistories = (from h in _context.SMSHistories
                                    join c in _context.SMSChannels on h.SMSChannelID equals c.ID
                                    join pr in _context.SMSProviderSettings on h.SMSProviderID equals pr.ID
                                    where c.Key.ToLower().Equals(channelKey.ToLower()) && (string.IsNullOrEmpty(tag) || h.Tags.ToLower().Equals(tag.ToLower()))
                                    select  new SMSHistoryTable { 
                                        ID=h.ID,
                                    Sender=h.Sender,
                                    Recipients=h.Recipients,
                                    SentOn=h.SentOn,
                                    MessageSent=h.MessageSent,
                                    Attempts=h.Attempts,
                                    Status=h.Status,
                                    Tags=h.Tags,
                                    SMSProviderID=h.SMSProviderID,
                                    SMSChannelID = h.SMSChannelID,
                                    TemplateName=h.TemplateName,
                                    TemplateVariant=h.TemplateVariant,
                                    ChannelKey=c.Key,
                                    ProviderName=pr.Name
                                    }).ToList();
                
                response.Status = true;
                
                response.Message = $"Retrieved {smsHistories.Count} SMS histories data for pool";
                response.Result = smsHistories;
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
