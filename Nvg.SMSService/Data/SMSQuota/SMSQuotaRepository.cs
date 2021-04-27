using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.SMSService.Data.SMSQuota
{
    public class SMSQuotaRepository : ISMSQuotaRepository
    {
        private readonly SMSDbContext _context;

        public SMSQuotaRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSResponseDto<SMSQuotaTable> GetSMSQuota(string channelKey)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = (from q in _context.SMSQuotas
                                join c in _context.SMSChannels on q.SMSChannelID equals c.ID
                                where c.Key.ToLower().Equals(channelKey.ToLower())
                                select q).FirstOrDefault();
                response.Status = true;
                response.Message = $"Retrieved SMS Quota";
                response.Result = smsQuota;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(string channelID)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = _context.SMSQuotas.FirstOrDefault(q => q.SMSChannelID == channelID);
                if (smsQuota != null)
                {
                    var totalCountInt = Convert.ToInt32(smsQuota.TotalConsumption); // TODO Implement encryption 
                    var monthCountInt = Convert.ToInt32(smsQuota.MonthlyConsumption); // TODO Implement encryption 
                    totalCountInt += 1; monthCountInt += 1;
                    smsQuota.TotalConsumption = totalCountInt;
                    smsQuota.MonthlyConsumption = monthCountInt;
                    _context.SMSQuotas.Update(smsQuota);
                }
                else
                {
                    smsQuota = new SMSQuotaTable()
                    {
                        SMSChannelID = channelID,
                        MonthlyQuota = -1,
                        MonthlyConsumption = 1,
                        TotalConsumption = 1,
                        TotalQuota = -1,
                        CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                    };
                    _context.SMSQuotas.Add(smsQuota);
                }
                if(_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "SMS Quota is updated";
                    response.Result = smsQuota;
                }
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public SMSResponseDto<SMSQuotaTable> UpdateCurrentMonth(string channelKey, string currentMonth)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = (from q in _context.SMSQuotas
                                  join c in _context.SMSChannels on q.SMSChannelID equals c.ID
                                  where c.Key.ToLower().Equals(channelKey.ToLower())
                                  select q).FirstOrDefault();
                smsQuota.CurrentMonth = currentMonth;
                smsQuota.MonthlyConsumption = 0;
                var updated = _context.SaveChanges();
                if (updated > 0)
                {
                    response.Status = true;
                    response.Message = $"Updated Channel Quota Current Month";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Failed to Update Channel Quota Current Month";
                }
                response.Result = smsQuota;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public SMSResponseDto<SMSQuotaTable> AddSMSQuota(SMSChannelDto smsChannel)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = _context.SMSQuotas.FirstOrDefault(q => q.SMSChannelID == smsChannel.ID);
                if (smsQuota == null)
                {
                    smsQuota = new SMSQuotaTable()
                    {
                        SMSChannelID = smsChannel.ID,
                        MonthlyQuota = smsChannel.IsRestrictedByQuota ? smsChannel.MonthlyQuota : -1,
                        MonthlyConsumption = 0,
                        TotalConsumption = 0,
                        TotalQuota = smsChannel.IsRestrictedByQuota ? smsChannel.TotalQuota : -1,
                        CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                    };
                    _context.SMSQuotas.Add(smsQuota);
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Quota already exists";
                    response.Result = smsQuota;
                }
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "SMS Quota is Added";
                    response.Result = smsQuota;
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
        public SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(SMSChannelDto smsChannel)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = _context.SMSQuotas.FirstOrDefault(q => q.SMSChannelID == smsChannel.ID);
                if (smsQuota != null)
                {
                    smsQuota.TotalQuota = smsChannel.IsRestrictedByQuota ? smsChannel.TotalQuota : -1;
                    smsQuota.MonthlyQuota = smsChannel.IsRestrictedByQuota ? smsChannel.MonthlyQuota : -1;
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Quota does not exist for provided channel ID : {smsChannel.ID}";
                    response.Result = smsQuota;
                }
                if (_context.SaveChanges() > 0)
                {
                    response.Status = true;
                    response.Message = "SMS Quota is Updated";
                    response.Result = smsQuota;
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Quota is not Updated";
                    response.Result = smsQuota;
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
        public SMSResponseDto<string> DeleteSMSQuota(string channelID)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                var smsQuota = _context.SMSQuotas.Where(q => q.SMSChannelID.ToLower().Equals(channelID.ToLower())).FirstOrDefault();
                if (smsQuota != null)
                {
                    _context.SMSQuotas.Remove(smsQuota);
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
                    response.Message = $"SMS Quota Data not found";
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
