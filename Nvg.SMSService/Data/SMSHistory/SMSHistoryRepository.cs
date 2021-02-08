using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
