using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;

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
                                select new SMSQuotaTable { 
                                ID = q.ID,
                                MonthlyQuota = q.MonthlyQuota,
                                MonthlyConsumption = q.MonthlyConsumption,
                                TotalQuota = q.TotalQuota,
                                SMSChannelID = q.SMSChannelID,
                                SMSChannelKey = c.Key,
                                CurrentMonth = q.CurrentMonth,
                                TotalConsumption = q.TotalConsumption
                                }).FirstOrDefault();
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

        public SMSResponseDto<List<SMSQuotaTable>> GetSMSQuotaList(string channelID)
        {
            var response = new SMSResponseDto<List<SMSQuotaTable>>();
            try
            {
                var smsQuota = (from q in _context.SMSQuotas
                                join c in _context.SMSChannels on q.SMSChannelID equals c.ID
                                where q.SMSChannelID.Equals(channelID)
                                select new SMSQuotaTable
                                {
                                    ID = q.ID,
                                    MonthlyQuota = q.MonthlyQuota,
                                    MonthlyConsumption = q.MonthlyConsumption,
                                    TotalQuota = q.TotalQuota,
                                    SMSChannelID = q.SMSChannelID,
                                    SMSChannelKey = c.Key,
                                    CurrentMonth = q.CurrentMonth,
                                    TotalConsumption = q.TotalConsumption
                                }).ToList();
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

        public SMSResponseDto<SMSQuotaTable> IncrementSMSQuota(string channelID)
        {
            var response = new SMSResponseDto<SMSQuotaTable>();
            try
            {
                var smsQuota = _context.SMSQuotas.FirstOrDefault(q => q.SMSChannelID == channelID);
                if (smsQuota != null)
                {
                    var totalCountInt = Convert.ToInt32(smsQuota.TotalConsumption); // TODO Implement encryption 
                    var monthCountInt = Convert.ToInt32(smsQuota.MonthlyConsumption); // TODO Implement encryption 
                    totalCountInt += 1; 
                    monthCountInt += 1;
                    smsQuota.TotalConsumption = totalCountInt;
                    smsQuota.MonthlyConsumption = monthCountInt;
                    _context.SMSQuotas.Update(smsQuota);
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS Quota does not exist for Channel ID :{channelID}";
                    response.Result = smsQuota;
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
                    response.Message = $"Updated Channel Quota Current Month.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Failed to Update Channel Quota Current Month.";
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
                        MonthlyQuota = smsChannel.MonthlyQuota ,
                        MonthlyConsumption = 0,
                        TotalConsumption = 0,
                        TotalQuota =  smsChannel.TotalQuota,
                        CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                    };
                    _context.SMSQuotas.Add(smsQuota);

                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "SMS Quota is Added";
                        response.Result = smsQuota;
                    }
                    else
                    {
                        response.Status = true;
                        response.Message = "SMS Quota is not added";
                        response.Result = smsQuota;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Quota already exists";
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
                    if(smsChannel.IsRestrictedByQuota)
                    {
                        smsQuota.TotalQuota = smsChannel.TotalQuota;
                        smsQuota.MonthlyQuota = smsChannel.MonthlyQuota;
                        smsQuota.CurrentMonth = DateTime.Now.ToString("MMM").ToUpper();
                    }
                    else
                    {
                        _context.SMSQuotas.Remove(smsQuota);
                    }
                }
                else
                {
                    if (smsChannel.IsRestrictedByQuota)
                    {
                        smsQuota = new SMSQuotaTable()
                        {
                            SMSChannelID = smsChannel.ID,
                            MonthlyQuota = smsChannel.MonthlyQuota,
                            MonthlyConsumption = 0,
                            TotalConsumption = 0,
                            TotalQuota = smsChannel.TotalQuota,
                            CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                        };
                        _context.SMSQuotas.Add(smsQuota);
                    }
                }
                if (_context.SaveChanges() > 0)
                {
                    response.Status = true;
                    response.Message = "SMS Quota has been Updated.";
                    response.Result = smsQuota;
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Quota has not been Updated.";
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
