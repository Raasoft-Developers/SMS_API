using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.SMSService.Data
{
    public class SMSCounterRepository : ISMSCounterRepository
    {
        private readonly SMSDbContext _context;

        public SMSCounterRepository(SMSDbContext context)
        {
            _context = context;
        }

        public string GetSMSCount(string tenantID, string facilityID)
        {
            var smsCounterObj = _context.SMSCounter.FirstOrDefault(sc => sc.TenantID == tenantID || sc.FacilityID == facilityID);
            if (smsCounterObj != null)
                return smsCounterObj.Count;
            return null;
        }

        public void UpdateSMSCounter(string tenantID, string facilityID)
        {
            var smsCounterObj = _context.SMSCounter.FirstOrDefault(sc => sc.TenantID == tenantID && sc.FacilityID == facilityID);
            if (smsCounterObj != null)
            {
                var countInt = Convert.ToInt32(smsCounterObj.Count); // TODO Implement encryption 
                countInt += 1;
                smsCounterObj.Count = countInt.ToString();
                _context.SMSCounter.Update(smsCounterObj);
            }
            else
            {
                smsCounterObj = new SMSCounterTable()
                {
                    TenantID = tenantID,
                    FacilityID = facilityID,
                    Count = "1"
                };
                _context.SMSCounter.Add(smsCounterObj);
            }
            _context.SaveChanges();
        }
    }
}
