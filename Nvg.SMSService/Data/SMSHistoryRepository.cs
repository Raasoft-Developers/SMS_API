using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data
{
    public class SMSHistoryRepository : ISMSHistoryRepository
    {
        private readonly SMSDbContext _context;

        public SMSHistoryRepository(SMSDbContext context)
        {
            _context = context;
        }

        public SMSHistoryTable Add(SMSHistoryTable sms)
        {
            sms = _context.SMSHistory.Add(sms).Entity;
            _context.SaveChanges();
            return sms;
        }

    }
}
