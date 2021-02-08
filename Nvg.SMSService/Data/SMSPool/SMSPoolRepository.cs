using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.SMSService.Data.SMSPool
{
    public class SMSPoolRepository : ISMSPoolRepository
    {
        private readonly SMSDbContext _context;

        public SMSPoolRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSResponseDto<SMSPoolTable> AddSMSPool(SMSPoolTable smsPoolInput)
        {
            var response = new SMSResponseDto<SMSPoolTable>();
            try
            {
                var isPoolExist = _context.SMSPools.Any(sp => sp.Name.ToLower().Equals(smsPoolInput.Name.ToLower()));
                if (!isPoolExist)
                {
                    smsPoolInput.ID = Guid.NewGuid().ToString();
                    _context.SMSPools.Add(smsPoolInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = smsPoolInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = smsPoolInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS pool already exists";
                    response.Result = smsPoolInput;
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

        public SMSResponseDto<SMSPoolTable> GetSMSPoolByName(string poolName)
        {
            var response = new SMSResponseDto<SMSPoolTable>();
            try
            {
                var smsPool = _context.SMSPools.FirstOrDefault(sp => sp.Name.ToLower().Equals(poolName.ToLower()));
                if(smsPool != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved SMS pool data for {poolName}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"SMS pool data for {poolName} is not available";
                }
                response.Result = smsPool;
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
