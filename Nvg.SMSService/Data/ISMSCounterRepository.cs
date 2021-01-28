using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data
{
    public interface ISMSCounterRepository
    {
        string GetSMSCount(string tenantID, string facilityID);
        void UpdateSMSCounter(string tenantID, string facilityID);
    }
}
