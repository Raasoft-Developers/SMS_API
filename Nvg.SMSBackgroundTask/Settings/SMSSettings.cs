using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSBackgroundTask.Settings
{
    public class SMSSettings
    {
        public bool EnableSMS { get; set; }
        public string SMSGatewayProvider { get; set; }
        public int MonthlySMSQuota { get; set; }
    }
}
