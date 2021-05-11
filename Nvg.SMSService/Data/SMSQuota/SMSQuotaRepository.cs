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
                    var countInt = Convert.ToInt32(smsQuota.TotalConsumption); // TODO Implement encryption 
                    countInt += 1;
                    smsQuota.TotalConsumption = countInt;
                    _context.SMSQuotas.Update(smsQuota);
                }
                else
                {
                    smsQuota = new SMSQuotaTable()
                    {
                        SMSChannelID = channelID,
                        MonthlyQuota = 100,
                        MonthylConsumption = 1,
                        TotalConsumption = 1
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
    }
}
