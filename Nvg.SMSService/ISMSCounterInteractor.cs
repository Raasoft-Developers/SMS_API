using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService
{
    public interface ISMSCounterInteractor
    {
        string GetSMSCounter(string tenantID, string facilityID);
        void UpdateSMSCounter(string tenantID, string facilityID);
    }
}
